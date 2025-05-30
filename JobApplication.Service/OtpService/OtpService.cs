﻿using JobApplication.Database.Repositories;
using JobApplication.Model;
using System;
using System.Threading.Tasks;

namespace JobApplication.Service.OtpService
{
    public class OtpService : IOtpService
    {
        private readonly IOtpRepository _otpRepository;
        public OtpService(IOtpRepository otpRepository)
        {
            _otpRepository = otpRepository;
        }

        public async Task<OtpMaster> AddOtpAsync(OtpMaster otp)
        {
            var result = await _otpRepository.AddAsync(otp);
            return result;
        }

        public async Task<bool> IsOtpUnique(int otp)
        {
            var isUnique = await _otpRepository.GetDefault(x => x.Otp == otp);
            return isUnique == null ? true : false;
        }

        public async Task<OtpMaster> Validate(int otp)
        {
            var result = await _otpRepository.GetDefault(x => x.Otp == otp && x.expiry >= DateTime.Now);
            return result;
        }
    }
}
