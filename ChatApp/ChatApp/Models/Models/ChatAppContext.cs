using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Entity;

namespace Model
{
   public  class ChatAppContext : DbContext

    {
        public System.Data.Entity.DbSet<Class> Classes { get; set; }
        public System.Data.Entity.DbSet<Message> Messages { get; set; }
        public System.Data.Entity.DbSet<Project> Projects { get; set; }
        public System.Data.Entity.DbSet<Resource> Resources { get; set; }
        public System.Data.Entity.DbSet<Role> Roles { get; set; }
        public System.Data.Entity.DbSet<University> Universities { get; set; }
        public System.Data.Entity.DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
