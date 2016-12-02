using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BlogSystem.Data;
using BlogSystem.Models;
using BlogSystem.Controllers;
using BlogSystem.Services.Contracts;
using BlogSystem.Areas.Admin.ViewModels;
using AutoMapper.QueryableExtensions;

namespace BlogSystem.Areas.Admin.Controllers
{
    public class PostsController : BaseController
    {
        private IPostService postService;

        public PostsController(IPostService postService)
        {
            this.postService = postService;
        }

        // GET: Admin/Posts
        public ActionResult Index()
        {
            ICollection<PostViewModel> posts = this.postService.GetAll().ProjectTo<PostViewModel>().ToList();

            return View(posts);
        }

        // GET: Admin/Posts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = postService.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }

            PostViewModel postViewModel = AutoMapper.Mapper.Map<Post, PostViewModel>(post);

            return View(postViewModel);
        }

        // GET: Admin/Posts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Content,DateCreated")] PostViewModel postViewModel)
        {

            if (ModelState.IsValid)
            {
                Post post = AutoMapper.Mapper.Map<PostViewModel, Post>(postViewModel);
                postService.Add(post);
                return RedirectToAction("Index");
            }

            return View(postViewModel);
        }

        // GET: Admin/Posts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = postService.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }

            PostViewModel postViewModel = AutoMapper.Mapper.Map<Post, PostViewModel>(post);

            return View(postViewModel);
        }

        // POST: Admin/Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Content,DateCreated")] PostViewModel postViewModel)
        {
            Post post = AutoMapper.Mapper.Map<PostViewModel, Post>(postViewModel);

            if (ModelState.IsValid)
            {
                //db.Entry(post).State = EntityState.Modified;
                postService.Update(post);
                return RedirectToAction("Index");
            }
            return View(postViewModel);
        }

        // GET: Admin/Posts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = postService.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }

            PostViewModel postViewModel = AutoMapper.Mapper.Map<Post, PostViewModel>(post);

            return View(postViewModel);
        }

        // POST: Admin/Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = postService.Find(id);
            postService.Delete(post);
            return RedirectToAction("Index");
        }
    }
}
