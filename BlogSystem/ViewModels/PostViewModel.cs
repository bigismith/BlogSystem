using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BlogSystem.Models;

namespace BlogSystem.ViewModels
{
    public class PostViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Username { get; set; }

        public DateTime DateTime { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public string Text { get; set; }
    }
}