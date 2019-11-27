using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model
{
  public class University
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string UniversityTitle { get; set; }
        [Required]
        public string UniversityCountry { get; set; }



        //Relation:

        //To tbl: Class
        public virtual ICollection<Class> Classes { get; set; }
    }
}
