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
        public string Department { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(200)]
        public string Password { get; set; }
    }
}