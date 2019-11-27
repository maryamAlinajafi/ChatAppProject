using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class User
    {

        public User()
        {
           
        }
        [Key]
        public System.Guid ID { get; set; }


        [MinLength(3), MaxLength(15)]
        public string Firstname { get; set; }

        [MinLength(3), MaxLength(15)]

        public string Lastname { get; set; }


        [Required]
        [MinLength(8), MaxLength(15)]
        public string Username { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }

        [ScaffoldColumn(false)]
        [Compare("Password",ErrorMessage ="Password and confirm not same! try again!")]
        
        public string Confirmation { get; set; }
        public string ProfileImage { get; set; }
        public bool? Status { get; set; }

        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "آدرس ایمیل وارد شده اشتباه است")]
        public string  EmailAddress{ get; set; }
        public string PhoneNumber { get; set; }





        //Relations:
        //To tbl : Role
        public virtual Role Role { get; set; }
        public virtual int  RoleId { get; set; }

        //To tbl : Message
        public virtual ICollection<Message> Messages  { get; set; }


        //To tbl : Class
        public virtual ICollection<Class> Classes { get; set; }
    }
}


