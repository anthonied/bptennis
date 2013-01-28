﻿using BPTennis.MVC.Models;
using BPTennis.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BPTennis.MVC.Controllers
{
    public class CommentController : Controller
    {
        //
        // GET: /Comment/

        public ActionResult Index()
        {
            var comments = new List<CommentsModel>();
            var commentRepository = new CommentRepository();

            var domainComments = commentRepository.GetAllComments();

            domainComments.ForEach(domainComment => comments.Add(new CommentsModel { Name = domainComment.Name, Comment = domainComment.Comment }));

            return View("Index");
        }

        public ActionResult AddComment(string name, string comment)
        {
            var commentRepository = new CommentRepository();

            commentRepository.AddComment(name, comment);

            return RedirectToAction("Index");
        }

        public ActionResult Comments()
        {
            var commentModel = new CommentsModel();
            var commentRepository = new CommentRepository();
            commentModel.CommentList = commentRepository.GetAllComments();
            return View(commentModel);
        }
        //
        // GET: /Comment/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Comment/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Comment/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Comment/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Comment/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Comment/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Comment/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
