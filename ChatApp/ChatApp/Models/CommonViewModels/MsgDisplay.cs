using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatApp.Models.CommonViewModels
{
    public class MsgDisplay
    {
        public int MsgID { get; set; }
        public string Text { get; set; }
        public string Time { get; set; }
        public string User_Username { get; set; }
        public string User_Profile { get; set; }


    }
}