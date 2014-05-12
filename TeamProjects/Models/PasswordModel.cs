using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeamProjects.Models
{
    public class PasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [StringLength(200, MinimumLength = 6)]
        [Display(Name = "Password: ")]
        public string Password { get; set; }
    }
}