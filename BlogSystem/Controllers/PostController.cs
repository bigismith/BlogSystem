using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogSystem.Models;
using BlogSystem.Services.Contracts;
using BlogSystem.ViewModels;

namespace BlogSystem.Controllers
{
    public class PostController : BaseController
    {
        private IPostService postService;
        private IUsersService usersService;
        private ICommentService commentService;

        public PostController(IPostService postService, IUsersService usersService, ICommentService commentService)
        {
            this.postService = postService;
            this.usersService = usersService;
            this.commentService = commentService;
        }
        
        // GET: Post
        public ActionResult View(int id)
        {
            Post post = postService.Find(id);

            PostViewModel postViewModel = null;

            if (post != null)
            {
                //postViewModel = new PostViewModel()
                //{
                //    Id = post.Id,
                //    Title = post.Title,
                //    Content = post.Content,
                //    DateTime = post.DateCreated,
                //    Username = post.Author.UserName,
                //    Comments = post.Comments
                //};

                postViewModel = AutoMapper.Mapper.Map<Post, PostViewModel>(post);
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

            //Post post = new Post()
            //{
            //    Title = postShortViewModel.Title,
            //    Content = postShortViewModel.Content,
            //    //DateCreated = DateTime.Now,
            //    //Author = this.Context.Users.FirstOrDefault( u => u.UserName == HttpContext.User.Identity.Name)
            //    Author = usersService.GetAll().FirstOrDefault(u => u.UserName == HttpContext.User.Identity.Name)
            //};

            Post post = null;

            post = AutoMapper.Mapper.Map<PostShortViewModel, Post>(postShortViewModel);
            post.Author = usersService.GetAll().FirstOrDefault(u => u.UserName == HttpContext.User.Identity.Name);

            this.postService.Add(post);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit(int id)
        {
            Post post = this.postService.Find(id);

            PostEditViewModel postEditViewModel = null;

            postEditViewModel = AutoMapper.Mapper.Map<Post, PostEditViewModel>(post);

            return View(postEditViewModel);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(PostEditViewModel postEditViewModel, int id)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Edit");
            }

            Post post = this.postService.Find(id);

            post = AutoMapper.Mapper.Map<PostEditViewModel, Post>(postEditViewModel, post);

            this.postService.Update(post);

            return RedirectToAction("View", new { id = id });
        }

        [HttpPost]
        public PartialViewResult Comment(PostViewModel postViewModel, int id)
        {
            if (!ModelState.IsValid)
            {
                //return RedirectToAction("View", new { id = id });
                return null;
            }

            Post post = this.postService.Find(id);

            Comment comment = new Comment()
            {
                Text = postViewModel.Text,
                //CommentTime = DateTime.Now,
                Post = post
            };

            this.commentService.Add(comment);

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
                //commentViewModel = new CommentViewModel()
                //{
                //    Id = comment.Id,
                //    Text = comment.Text
                //};

                commentViewModel = AutoMapper.Mapper.Map<Comment, CommentViewModel>(comment);
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