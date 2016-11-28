using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogSystem.Models;
using BlogSystem.ViewModels;

namespace BlogSystem.Controllers
{
    public class PostController : BaseController
    {
        // GET: Post
        public ActionResult View(int id)
        {
            Post post = this.Context.Posts.Where(p => p.Id == id).FirstOrDefault();

            PostViewModel postViewModel = null;

            if (post != null)
            {
                postViewModel = new PostViewModel()
                {
                    Id = post.Id,
                    Title = post.Title,
                    Content = post.Content,
                    DateTime = post.DateCreated,
                    Username = post.Author.UserName,
                    Comments = post.Comments
                };
            }

            return View(postViewModel);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(PostShortViewModel postShortViewModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Create");
            }

            Post post = new Post()
            {
                Title = postShortViewModel.Title,
                Content = postShortViewModel.Content,
                DateCreated = DateTime.Now,
                Author = this.Context.Users.FirstOrDefault( u => u.UserName == HttpContext.User.Identity.Name)
            };

            this.Context.Posts.Add(post);
            this.Context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Edit(PostShortViewModel postShortViewModel, int id)
        {

            return View("Create");
        }

        [HttpPost]
        public PartialViewResult Comment(PostViewModel postViewModel, int id)
        {
            if (!ModelState.IsValid)
            {
                //return RedirectToAction("View", new { id = id });
                return null;
            }

            Post post = this.Context.Posts.FirstOrDefault(p => p.Id == id);

            Comment comment = new Comment()
            {
                Text = postViewModel.Text,
                CommentTime = DateTime.Now,
                Post = post
            };

            this.Context.Comments.Add(comment);
            this.Context.SaveChanges();

            CommentViewModel commentViewModel = null;

            commentViewModel = new CommentViewModel()
            {
                Id = comment.Id,
                Text = comment.Text
            };

            //return RedirectToAction("View", new { id =  id});
            return PartialView("_Comment", commentViewModel);
        }

        [HttpGet]
        public PartialViewResult Comment(Comment comment)
        {
            CommentViewModel commentViewModel = null;

            if (comment != null)
            {
                commentViewModel = new CommentViewModel()
                {
                    Id = comment.Id,
                    Text = comment.Text
                };
            }

            return PartialView("_Comment", commentViewModel);
        }

        [HttpGet]
        public ActionResult CommentEdit()
        {

            return View();
        }
    }
}