using System;
using System.Collections.Generic;
using System.Text;

namespace database
{
    public class Post
    {

        public int PostId { get; set; }
        public String Description { get; set; }
        public String Domain { get; set; }
        public DateTime Date { get; set; }
        public List<Comment> comments { get; set; }
    }
}
