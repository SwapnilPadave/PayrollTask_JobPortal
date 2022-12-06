using JobApplication.Database.Infrastructure;
using JobApplication.Database.Repositories;
using JobApplication.Model;
using JobApplication.Model.Models;
using JobApplication.Service.EmailService;
using JobApplication.Service.OtpService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobApplication.Service.AccountService
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private static Random _random = new Random();
        private readonly IOtpService _otpService;
        private readonly IEmailService _emailService;
        private readonly IUserRepository _userRepository;
        public AccountService(IAccountRepository accountRepository, IOtpService otpService, IEmailService emailService, IUserRepository userRepository)
        {
            _accountRepository = accountRepository;
            _otpService = otpService;
            _emailService = emailService;
            _userRepository = userRepository;
        }
        public async Task<bool> ForgotPassword(string email)
        {
            StringBuilder body = new StringBuilder();

            var user = await GetUserByMail(email);
            if (user != null)
            {
            regenerate:
                var otp = Convert.ToInt32(GenerateRandomNo());
                var isUnique = await _otpService.IsOtpUnique(otp);
                if (!isUnique)
                    goto regenerate;
                var to = user.Email;
                var sub = "OTP";
                var emailBody = body;
                body.Append("<h3>Job Portal</h3>");
                body.AppendLine($"<h4 style='font-size:1.1em'>Hi, {user.Name}</h4>");
                body.AppendLine("<h5>For Reseting your password, OTP is valid for 10 minutes</h5>");
                body.AppendLine($"<h2 style='background: #00466a;margin: 0 auto;width: max-content;padding: 0 10px;color: #fff;border-radius: 4px;'>{otp}</h2>");

                var userOtp = new OtpMaster();
                userOtp.Otp = Convert.ToInt32(otp);
                userOtp.GenerateBy = user.Id;
                userOtp.CreateDate = DateTime.Now;
                userOtp.expiry = DateTime.Now.AddMinutes(10);
                await _otpService.AddOtpAsync(userOtp);
                await _emailService.SendEmailAsync(to, body, sub, "", "");
                return true;
            }
            return false;
        }
        public async Task<UserMaster> ResetPassword(int otp, string newPassword, string confirmPassword)
        {

            var details = await ValidateOtp(otp);
            var updateUser = new UserMaster();
            if (details != null)
            {
                updateUser = await _userRepository.GetByIdAsync(details.GenerateBy);
                if (newPassword == confirmPassword)
                {
                    updateUser.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
                    await _userRepository.UpdateAsync(updateUser);
                    return updateUser;
                }
            }

            return null;
        }

        public async Task<UserMaster> GetUserByMail(string email)
        {
            var user = await _accountRepository.GetDefault(x => x.Email == email);
            if (user != null)
            {
                return user;
            }
            else
            {
                return null;
            }

        }

        private static string GenerateRandomNo()
        {
            return _random.Next(0, 999999).ToString("D6");
        }

        private async Task<OtpMaster> ValidateOtp(int otp)
        {
            return await _otpService.Validate(otp);
        }

    }
}
