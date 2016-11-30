using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogSystem.Data;
using BlogSystem.Models;
using BlogSystem.Services.Contracts;

namespace BlogSystem.Services
{
    public class CommentService : BaseService<Comment>, ICommentService
    {
        public CommentService(IBlogSystemData data) : base(data)
        {
        }

        public override void Add(Comment entity)
        {
            entity.CommentTime = DateTime.Now;
            base.Add(entity);
            base.SaveChanges();
        }
    }
}
