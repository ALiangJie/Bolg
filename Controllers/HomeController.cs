using Bolg.Data;
using Bolg.Data.FileManager;
using Bolg.Data.Repository;
using Bolg.Models;
using Bolg.Models.Comments;
using Bolg.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bolg.Controllers
{
    public class HomeController : Controller
    {
        private IRepository _repo;
        private IFileManager _fileManager;

        public HomeController(
            IRepository repo,
            IFileManager fileManager)
        {
            _repo = repo;
            _fileManager = fileManager;

        }

        public IActionResult Index(int pageNumber, string category)
        {
            if (pageNumber < 1)
            {
                return RedirectToAction("Index", new { pageNumber = 1, category });
            }

            //var vm = new IndexViewModle
            //{
            //    PageNumber = pageNumber,
            //    Posts = string.IsNullOrEmpty(category) ?
            //        _repo.GetAllPosts(pageNumber) :
            //        _repo.GetAllPosts(category)
            //};

            var vm = _repo.GetAllPosts(pageNumber,category);


            return View(vm);
        }



        //public IActionResult Index(string category)
        //{
        //    var posts = string.IsNullOrEmpty(category) ? _repo.GetAllPosts() : _repo.GetAllPosts(category);
        //    return View(posts);
        //}

        public IActionResult Post(int id) =>
            View(_repo.GetPost(id));

        //public IActionResult Post(int id)
        //{
        //    var post = _repo.GetPost(id);

        //    return View(post);
        //}

        [HttpGet("/Image/{image}")]
        [ResponseCache(CacheProfileName ="Monthly")]
        public IActionResult Image(string image) =>
            new FileStreamResult(
                _fileManager.ImageStream(image), 
                $"image/{image.Substring(image.LastIndexOf('.') + 1)}");

        //[HttpGet("/Image/{image}")]
        //public IActionResult Image(string image)
        //{
        //    var mine = image.Substring(image.LastIndexOf('.') + 1);
        //    return new FileStreamResult(_fileManager.ImageStream(image), $"image/{mine}");
        //}

        [HttpPost]
        public async Task<IActionResult> Comment(CommentViewModel vm)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("post", new { id = vm.PostId });

            var post = _repo.GetPost(vm.PostId);
            if (vm.MainCommentId==0)
            {
                post.MainComments = post.MainComments ?? new List<MainComment>();

                post.MainComments.Add(new MainComment
                {
                    Message = vm.Message,
                    Created = DateTime.Now,
                });

                _repo.UpdatePost(post);
            }
            else
            {
                var comment = new SubComment
                {
                    MainCommentId = vm.MainCommentId,
                    Message = vm.Message,
                    Created = DateTime.Now,
                };
                _repo.AddSubComment(comment);
            }


            await _repo.SaveChangesAsybc();

            return RedirectToAction("post", new { id = vm.PostId });
        }

    }
}
