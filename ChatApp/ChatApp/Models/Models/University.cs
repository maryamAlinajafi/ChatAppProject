
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model
{
  public class University
    {
        public University()
        {
            Classes = new HashSet<Class>();

        }
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.ErrorMessages), ErrorMessageResourceName = "RequireError")]
        [Display(Name = "Title", ResourceType = typeof(ChatApp.App_GlobalResources.UniversityRF))]
        public string UniversityTitle { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.ErrorMessages), ErrorMessageResourceName = "RequireError")]
        [Display(Name = "UniversityCountry", ResourceType = typeof(ChatApp.App_GlobalResources.UniversityRF))]
        public string UniversityCountry { get; set; }



        //Relation:

        //To tbl: Class
        public virtual ICollection<Class> Classes { get; set; }
    }
}
