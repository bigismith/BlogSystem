using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogSystem.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Diagnostics;

namespace BlogSystem.Data
{
    public class BlogSystemDbContext : IdentityDbContext<ApplicationUser>
    {
        public BlogSystemDbContext() : base("BlogSystemConnection")
        {
            Database.Log = sql => Debug.Write(sql);
        }

        public static BlogSystemDbContext Create()
        {
            return new BlogSystemDbContext();
        }

        public IDbSet<Post> Posts { get; set; }

        public IDbSet<Comment> Comments { get; set; }
    }
}
