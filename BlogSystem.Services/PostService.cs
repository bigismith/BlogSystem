using BlogSystem.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogSystem.Models;
using BlogSystem.Data;

namespace BlogSystem.Services
{
    public class PostService : BaseService<Post>, IPostService
    {
        public PostService(IBlogSystemData data)
            :base(data)
        {
        }

        public override IQueryable<Post> GetAll()
        {
            return base.GetAll().OrderByDescending(p => p.DateCreated);
        }

        public override void Add(Post entity)
        {
            entity.DateCreated = DateTime.Now;
            base.Add(entity);
            base.SaveChanges();
        }

        public IQueryable<Post> GetByUserId(object id)
        {
            return base.GetAll().OrderByDescending(p => p.DateCreated).Where(p => p.Author.Id == id);
        }
    }
}
