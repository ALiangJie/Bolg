using Bolg.Models;
using Bolg.Models.Comments;
using Bolg.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolg.Data.Repository
{
    public class Repository : IRepository
    {

        private AppDbContext _ctx;

        public Repository(AppDbContext ctx)
        {
            _ctx = ctx;
        }


        public Post GetPost(int id)
        {
            return _ctx.Posts
                .Include(p => p.MainComments)
                .ThenInclude(mc => mc.SubComments)
                .FirstOrDefault(p => p.Id == id);


        }
        public List<Post> GetAllPosts()
        {
            return _ctx.Posts.ToList();
        }

        public IndexViewModle GetAllPosts(int pageNumber,string category)
        {
            Func<Post, bool> InCategory = (post) => { return post.Category.ToLower().Equals(category.ToLower()); };
            //InCategory(a)=2;
            //InCategory(b)=10;
            //var a = 2;
            //F#,Clojure,Haskell
            int pageSize = 5;
            int skipAmount = pageSize * (pageNumber - 1);


            var query = _ctx.Posts.AsQueryable();
                

            if (!string.IsNullOrEmpty(category))
                query = query.Where(x => InCategory(x));
            int postsCount = query.Count();

            return new IndexViewModle
            {
                PageNumber = pageNumber,
                PageCount = (int)Math.Ceiling((double)postsCount / pageSize),
                NextPage = postsCount > skipAmount + pageSize,
                Category = category,
                Posts = query.Skip(skipAmount).Take(pageSize).ToList()
            };
        }

        public void AddPost(Post post)
        {
            _ctx.Posts.Add(post);
            
        }
        public void UpdatePost(Post post)
        {
            _ctx.Posts.Update(post);
        }
        public void RemovePost(int id)
        {
            _ctx.Posts.Remove(GetPost(id));
        }

        public async Task<bool> SaveChangesAsybc()
        {
            if(await _ctx.SaveChangesAsync()>0)
            {
                return true;
            }
            return false;
        }

        public void AddSubComment(SubComment comment)
        {
            _ctx.SubComments.Add(comment);
        }
    }
}
