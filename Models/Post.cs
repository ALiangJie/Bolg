using System;

namespace Bolg.Models
{
    public class Post
    {
        public String Title { get; set; } = "";
        public String Body { get; set; } = "";

        public DateTime Created { get; set; } = DateTime.Now;
    }
}
