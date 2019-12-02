using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model
{
    public class Role
    {
        public Role()
        {
            Users = new HashSet<User>();

        }
        [Key]
        public int ID { get; set; }
        [Required]
        public string RoleName { get; set; }

        //Relation:
        //To tbl : User
        public virtual ICollection<User> Users { get; set; }


    }
}
