using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model
{
    public class Message
    {
        [Key]
        [Required]
        public int ID { get; set; }


        [MinLength(1)]
        public string Text { get; set; }
        public int? SeenNumber  { get; set; }
        public DateTime? DateTime { get; set; }




        //Relation:
        //To tbl:User
        public virtual User User{ get; set; }
        public virtual string  UserId { get; set; }


        //To tbl:Class
        public virtual Class Classes{ get; set; }
        public virtual int  ClassId { get; set; }
    }
}
