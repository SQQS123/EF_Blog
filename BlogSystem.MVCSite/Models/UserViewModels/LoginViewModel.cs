﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogSystem.MVCSite.Models.UserViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "电子邮箱")]
        public string Email { get; set; }
        [Required]
        [StringLength(50,MinimumLength = 6)]
        [Display(Name = "密码")]
        [DataType(dataType:DataType.Password)]
        public string LoginPwd { get; set; }
        public bool RememberMe { get; set; }
    }
}