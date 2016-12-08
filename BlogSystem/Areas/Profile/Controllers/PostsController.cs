using AutoMapper.QueryableExtensions;
using BlogSystem.Areas.Profile.ViewModels;
using BlogSystem.Models;
using BlogSystem.Services.Contracts;
using BlogSystem.ViewModels;
using Microsoft.AspNet.Identity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogSystem.Areas.Profile.Controllers
{
    public class PostsController : ProfileBaseController
    {
        private IPostService postService;
        private IUsersService usersService;

        public PostsController(IPostService postService, IUsersService usersService)
        {
            this.postService = postService;
            this.usersService = usersService;
        }

        // GET: Profile/Posts
        public ActionResult Index(int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            IQueryable<PostShortViewModel> posts = this.postService.GetByUserId(HttpContext.User.Identity.GetUserId()).ProjectTo<PostShortViewModel>();//.ToList();

            return View(posts.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
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

            return RedirectToAction("View", "Post", new { area = "", id = post.Id });
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Post post = this.postService.Find(id);

            PostEditViewModel postEditViewModel = null;

            postEditViewModel = AutoMapper.Mapper.Map<Post, PostEditViewModel>(post);

            return View(postEditViewModel);
        }

        [HttpPost]
        public ActionResult Edit(PostEditViewModel postEditViewModel, int id)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Edit");
            }

            Post post = this.postService.Find(id);

            post = AutoMapper.Mapper.Map<PostEditViewModel, Post>(postEditViewModel, post);

            this.postService.Update(post);

            return RedirectToAction("View", "Post", new { area = "", id = id });
        }
    }
}