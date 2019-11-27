using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model
{
   public class Resource
    {
        [Key]
        public int ID { get; set; }

        public string File { get; set; }




        //Resourses:
        //To tbl: Class
        public virtual Class Class { get; set; }
        public virtual int  ClassId { get; set; }
    }
}
