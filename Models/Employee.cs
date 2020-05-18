using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GuidMethodWorking.Models
{
    public class Employee
    {

        [Key]
        public int EmployeeId { get; set; }
        [Required(ErrorMessage = "Enter Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "PhoneNumber")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Address")]
        public string Address { get; set; }



        //// this is to recognise the userId
        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}