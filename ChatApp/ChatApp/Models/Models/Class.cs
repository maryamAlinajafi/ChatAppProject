using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model
{
  public  class Class
    {
        [Required]
        [Key]
        public int ID { get; set; }

        [MinLength(3),MaxLength(80)]
        public string Title { get; set; }

        public int MemberCount { get; set; }
        public string  Semester { get; set; }

        [Required(ErrorMessage ="پر کردن این فیلد اجباریست")]
        [MinLength(8), MaxLength(15)]
        [Index(IsUnique =true)]
        public string  AccessCode { get; set; }
        public int AdminInfo { get; set; }
        public string ProfileImage { get; set; }




        //Relation:

            //To tbl : User
        public virtual ICollection<User> Users { get; set; }


        //To tbl: Message
        public virtual ICollection<Message> Messages { get; set; }


        //TO tbl:University

        public virtual University University  { get; set; }
        public virtual int  UniversityId { get; set; }

        //To tbl : Resourses

        public virtual ICollection<Resource> Resourses { get; set; }


        //To tbl: project

        public virtual ICollection<Project> Projects { get; set; }


    }


}
