using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Model
{
    public class Class

    {
        public Class()
        {
            Users = new HashSet<User>();
            Messages = new HashSet<Message>();
            Resourses = new HashSet<Resource>();
            Projects = new HashSet<Project>();


        }
        [Required]
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.ErrorMessages), ErrorMessageResourceName = "RequireError")]
        [MinLength(3, ErrorMessageResourceType = typeof(Resources.ErrorMessages), ErrorMessageResourceName = "minLenght")]
        [MaxLength(25, ErrorMessageResourceType = typeof(Resources.ErrorMessages), ErrorMessageResourceName = "maxLenght")]
        [Display(Name = "Title", ResourceType = typeof(ChatApp.App_GlobalResources.ClassRF))]
        public string Title { get; set; }

        [Display(Name = "MemberCount", ResourceType = typeof(ChatApp.App_GlobalResources.ClassRF))]

        public int MemberCount { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.ErrorMessages), ErrorMessageResourceName = "semesterRequire")]
        [Range(1, int.MaxValue, ErrorMessageResourceType = typeof(Resources.ErrorMessages), ErrorMessageResourceName = "semesterRenge")]
        [Display(Name = "Semester", ResourceType = typeof(ChatApp.App_GlobalResources.ClassRF))]
        public ChatApp.Models.Enums.SemmesterEnum Semester { get; set; }


        [Required(ErrorMessageResourceType = typeof(Resources.ErrorMessages), ErrorMessageResourceName = "RequireError")]
        [MinLength(8, ErrorMessageResourceType = typeof(Resources.ErrorMessages), ErrorMessageResourceName = "minLenght")]
        [MaxLength(15, ErrorMessageResourceType = typeof(Resources.ErrorMessages), ErrorMessageResourceName = "maxLenght")]
        [Index(IsUnique = true)]
        [Display(Name = "AccessCode", ResourceType = typeof(ChatApp.App_GlobalResources.ClassRF))]

        public string AccessCode { get; set; }



        [Display(Name = "AdminInfo", ResourceType = typeof(ChatApp.App_GlobalResources.ClassRF))]
        public string AdminInfo { get; set; }





        [Display(Name = "ProfileImage", ResourceType = typeof(ChatApp.App_GlobalResources.ClassRF))]
        public string ProfileImage { get; set; }




        //Relation:

        //To tbl : User
        public virtual ICollection<User> Users { get; set; }


        //To tbl: Message
        public virtual ICollection<Message> Messages { get; set; }


        //TO tbl:University

        public virtual University University { get; set; }
        [Display(Name = "UniversityId", ResourceType = typeof(ChatApp.App_GlobalResources.ClassRF))]
        [Required(ErrorMessageResourceType = typeof(Resources.ErrorMessages), ErrorMessageResourceName = "RequireError")]

        public virtual int UniversityId { get; set; }

        //To tbl : Resourses

        public virtual ICollection<Resource> Resourses { get; set; }


        //To tbl: project

        public virtual ICollection<Project> Projects { get; set; }


    }
}
