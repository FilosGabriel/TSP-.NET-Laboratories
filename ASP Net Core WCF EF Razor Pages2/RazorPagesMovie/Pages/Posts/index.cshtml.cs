using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ServiceReferencePostComment;

namespace RazorPagesMovie
{
    public class indexModel : PageModel
    {
        PostCommentClient pcc = new PostCommentClient();

        public indexModel()
        {
            Posts = new List<Post>();
        }
        public async Task OnGetAsync()
        {
            List<Post> posts = new List<Post>();
            posts = await pcc.GetPostsAsync();


            foreach (var item in posts)
            {
                // Trebuia folosit AutoMapper. Transform Post in PostDTO
                Post pd = new Post();
                pd.Description = item.Description;
                pd.PostId = item.PostId;
                pd.Domain = item.Domain;
                pd.Date = item.Date;
                if (pd.Comments == null)
                    pd.Comments = new List<Comment>();
                foreach (var cc in item.Comments)
                {
                    Comment cdto = new Comment();
                    cdto.PostPostId = cc.PostPostId;
                    cdto.Text = cc.Text;
                    pd.Comments.Add(cdto);
                }
                Posts.Add(pd);
            }

        }
        public List<Post> Posts { get; set; }
    }
}