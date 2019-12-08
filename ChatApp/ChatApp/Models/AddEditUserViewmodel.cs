using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model

{
    public class AddEditUserViewmodel
    {
        public Model.User UserViewModel { get; set; }
        public string ImagePath { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
    }
}