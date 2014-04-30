using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeamProjects.Models
{
    public class DepartmentModel
    {
        [Required]
        [DataType(DataType.Text)]
        [StringLength(4)]
        [Display(Name="Department Code: ")]
        public string Department { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(200, MinimumLength = 8)]
        [Display(Name = "Password: ")]
        public string Password { get; set; }
    }
}