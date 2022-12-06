using JobApplication.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobApplication.Service.OtpService
{
    public interface IOtpService
    {
        Task<OtpMaster> AddOtpAsync(OtpMaster otp);
        Task<OtpMaster> Validate(int otp);
        Task<bool> IsOtpUnique(int otp);
    }
}
