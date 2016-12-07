using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogSystem.Common.Caching;
using BlogSystem.Data;
using BlogSystem.Services.Contracts;
using BlogSystem.ViewModels;
using AutoMapper.QueryableExtensions;

namespace BlogSystem.Controllers
{
    public class HomeController : BaseController
    {
        private IPostService postService;
        private ICacheService cache;

        public HomeController(IPostService postService, ICacheService cache)
        {
            this.postService = postService;
            this.cache = cache;
        }

        public ActionResult Index()
        {
            //ICollection<PostShortViewModel> posts = this.Context.Posts.OrderByDescending(p => p.DateCreated).Select(p => new PostShortViewModel()
            //ICollection<PostShortViewModel> posts = this.Data.Posts.All().OrderByDescending(p => p.DateCreated).Select(p => new PostShortViewModel()
            /*
            ICollection<PostShortViewModel> posts = this.postService.GetAll().OrderByDescending(p => p.DateCreated).Select(p => new PostShortViewModel()
            {
                Id = p.Id,
                Title = p.Title,
                Content = p.Content,
                DateTime = p.DateCreated,
                Username = p.Author.UserName
            }).ToList();
            */

            var posts = this.cache.Get("homepagePosts", GetHomepagePosts, 5 * 60);

            return View(posts);
        }

        private ICollection<PostShortViewModel> GetHomepagePosts()
        {
            //ICollection<PostShortViewModel> posts = this.postService.GetAll().OrderByDescending(p => p.DateCreated).Select(p => new PostShortViewModel()
            //{
            //    Id = p.Id,
            //    Title = p.Title,
            //    Content = p.Content,
            //    DateTime = p.DateCreated,
            //    Username = p.Author.UserName
            //}).ToList();

            ICollection<PostShortViewModel> posts = this.postService.GetAll().Take(7).OrderByDescending(p => p.DateCreated).ProjectTo<PostShortViewModel>().ToList();

            return posts;
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}