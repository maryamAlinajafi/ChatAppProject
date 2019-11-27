using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model
{
  public class Project
    {
        [Key]
        public int ID { get; set; }
        [MinLength(3)]
        public string Text{ get; set; }

        public DateTime? Deadline { get; set; }
        public int? Score { get; set; }


        //Relation:
        //To tbl:Class
        public virtual Class Class { get; set; }
        public virtual int  ClassId { get; set; }

    }
}
