using Bolg.Models;
using Bolg.Models.Comments;
using Bolg.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolg.Data.Repository
{
    public interface IRepository
    {
        Post GetPost(int id);
        List<Post> GetAllPosts();
        IndexViewModle GetAllPosts(int pageNumber, string Category);
        void AddPost(Post post);
        void UpdatePost(Post post);
        void RemovePost(int id);


        void AddSubComment(SubComment comment);

        Task<bool> SaveChangesAsybc();
    }
}
