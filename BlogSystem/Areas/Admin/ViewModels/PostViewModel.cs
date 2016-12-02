using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogSystem.Areas.Admin.ViewModels
{
    public class PostViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Username { get; set; }

        public DateTime DateTime { get; set; }
    }
}