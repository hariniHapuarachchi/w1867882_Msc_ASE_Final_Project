using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;

namespace w1867882_Harini_Hapuarachchi_Land_Evaluation.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required(ErrorMessage = "User Name is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "First Name is required")]
        //[RegularExpression(@"^[A-Z][a-zA-Z]*$", ErrorMessage = "Special Characters are not allowed.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        //[RegularExpression(@"^[A-Z][a-zA-Z]*$", ErrorMessage = "Special Characters are not allowed.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        //[StringLength(8)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Address is required")]
        //[RegularExpression(@"^[0-9]+\s+([a-zA-Z]+|[a-zA-Z]+\s[a-zA-Z]+)$", ErrorMessage = "Invalid Address.")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Gender is required")]
        public bool Gender { get; set; }
        [Required(ErrorMessage = "Phone Number is required")]
        //[RegularExpression(@"^(?:7|0|(?:\+94))[0-9]{9,10}$", ErrorMessage = "Invalid Phone Number.")]
        public int Phone { get; set; }
        [Required(ErrorMessage = "Email is required")]
        //[RegularExpression(@"^((\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)\s*[;]{0,1}\s*)+$", ErrorMessage = "Entered email is invalid.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Date of Birth is required")]
        public DateTime Dob { get; set; }
        [Required(ErrorMessage = "Nic is required")]
        public string Nic { get; set; }

        public ICollection<Land> Lands { get; }
    }
}
