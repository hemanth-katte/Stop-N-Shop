using Microsoft.AspNetCore.Mvc;
using Stop_nShop.DTOs.RequestDTOs;
using Stop_nShop.Service.ServiceInterface;

namespace Stop_nShop.Utilities.Email
{


    public class Email
    {
        private readonly IUserService userService;

        //send email to seller 
        [HttpPost("sendEmail")]
        public string SendEmail([FromBody] SendEmailDto emailDto)
        {
            var response = userService.SendEmail(emailDto);

            return "Email sent";
        }
    }
}
