using JobApplication.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobApplication.Service.AccountService
{
    public interface IAccountService
    {
        Task<bool> ForgotPassword(string email);
        Task<UserMaster> ResetPassword(int otp, string newPassword, string confirmPassword);
    }
}
