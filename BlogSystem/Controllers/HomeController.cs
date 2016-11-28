using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogSystem.Data;
using BlogSystem.ViewModels;

namespace BlogSystem.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            ICollection<PostShortViewModel> posts = this.Context.Posts.OrderByDescending(p => p.DateCreated).Select(p => new PostShortViewModel()
            {
                Id = p.Id,
                Title = p.Title,
                Content = p.Content,
                DateTime = p.DateCreated,
                Username = p.Author.UserName
            }).ToList();

            return View(posts);
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