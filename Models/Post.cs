using Bolg.Models.Comments;
using System;
using System.Collections.Generic;

namespace Bolg.Models
{
    public class Post
    {
        public int Id { get; set; }

        public string Title { get; set; } = "";
        public string Body { get; set; } = "";
        public string Image { get; set; } = "";

        public string Description { get; set; } = "";
        public string Tag { get; set; } = "";
        public string Category { get; set; } = "";

        public DateTime Created { get; set; } = DateTime.Now;

        public List<MainComment> MainComments { get; set; }
    }
}
