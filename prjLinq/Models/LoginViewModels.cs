using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GroupViewModelSample.Models
{
    public class LoginViewModels
    {
        [Required(ErrorMessage = "信箱不可以空白!")]
        [EmailAddress(ErrorMessage = "E-Mail格式錯誤!")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}