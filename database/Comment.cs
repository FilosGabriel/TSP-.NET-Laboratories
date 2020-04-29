using System;
using System.Collections.Generic;
using System.Text;

namespace database
{
  public  class Comment
    {
        public int CommentId { get; set; }
        public string Text { get; set; }
        public int PostPostId { get; set; }
        public Post post;
    }
}
