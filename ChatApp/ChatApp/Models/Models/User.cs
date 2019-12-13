using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class User
    {

        public User()
        {
            Classes = new HashSet<Class>();
            Messages = new HashSet<Message>();

        }
        [Key]
        public System.Guid ID { get; set; }

        [Required(ErrorMessageResourceType =typeof(Resources.ErrorMessages),ErrorMessageResourceName = "RequireError")]
        [Display(Name = "Firstname", ResourceType = typeof(ChatApp.App_GlobalResources.Users))]
        [MinLength(3,ErrorMessageResourceType =typeof(Resources.ErrorMessages),ErrorMessageResourceName = "minLenght")]
        [MaxLength(15, ErrorMessageResourceType = typeof(Resources.ErrorMessages), ErrorMessageResourceName = "maxLenght")]
        public string Firstname { get; set; }


        [Required(ErrorMessageResourceType = typeof(Resources.ErrorMessages), ErrorMessageResourceName = "RequireError")]
        [Display(Name = "Lastname", ResourceType = typeof(ChatApp.App_GlobalResources.Users))]
        [MinLength(3, ErrorMessageResourceType = typeof(Resources.ErrorMessages), ErrorMessageResourceName = "minLenght")]
        [ MaxLength(15, ErrorMessageResourceType = typeof(Resources.ErrorMessages), ErrorMessageResourceName = "maxLenght")]
        public string Lastname { get; set; }


        [Required(ErrorMessageResourceType = typeof(Resources.ErrorMessages), ErrorMessageResourceName = "RequireError")]
        [Display(Name = "Username", ResourceType = typeof(ChatApp.App_GlobalResources.Users))]
        [MinLength(8, ErrorMessageResourceType = typeof(Resources.ErrorMessages), ErrorMessageResourceName = "minLenght")]
        [ MaxLength(15, ErrorMessageResourceType = typeof(Resources.ErrorMessages), ErrorMessageResourceName = "maxLenght")]
        public string Username { get; set; }



        [Required(ErrorMessageResourceType = typeof(Resources.ErrorMessages), ErrorMessageResourceName = "RequireError")]
        [Display(Name = "Password", ResourceType = typeof(ChatApp.App_GlobalResources.Users))]
        [MinLength(8, ErrorMessageResourceType = typeof(Resources.ErrorMessages), ErrorMessageResourceName = "minLenght")]
        public string Password { get; set; }





        [Required(ErrorMessageResourceType = typeof(Resources.ErrorMessages), ErrorMessageResourceName = "RequireError")]
        [ScaffoldColumn(false)]
        [Display(Name = "Confirmation", ResourceType = typeof(ChatApp.App_GlobalResources.Users))]
        [Compare("Password",ErrorMessageResourceType =typeof(Resources.ErrorMessages),ErrorMessageResourceName = "CompereError")]
        public string Confirmation { get; set; }


        [Display(Name = "ProfileImage", ResourceType = typeof(ChatApp.App_GlobalResources.Users))]
        public string ProfileImage { get; set; }



        public bool? Status { get; set; }



        [Display(Name = "EmailAddress", ResourceType = typeof(ChatApp.App_GlobalResources.Users))]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessageResourceType = typeof(Resources.ErrorMessages), ErrorMessageResourceName = "InvalidEmail")]
        public string  EmailAddress{ get; set; }



        [Display(Name = "PhoneNumber", ResourceType = typeof(ChatApp.App_GlobalResources.Users))]
        public string PhoneNumber { get; set; }





        //Relations:
        //To tbl : Role
        public virtual Role Role { get; set; }

        [Display(Name = "RoleId", ResourceType = typeof(ChatApp.App_GlobalResources.Users))]
        [Required(ErrorMessageResourceType = typeof(Resources.ErrorMessages), ErrorMessageResourceName = "RequireError")]
        public virtual int  RoleId { get; set; }

        //To tbl : Message
        public virtual ICollection<Message> Messages  { get; set; }


        //To tbl : Class
        public virtual ICollection<Class> Classes { get; set; }
    }
}


