﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laborator6
{
    public partial class Post
    {
        public bool AddPost()
        {
            using (ModelPostCommentContainerContainer ctx = new ModelPostCommentContainerContainer())
            {
                bool bResult = false;
                if (this.PostId == 0)
                {
                    var it = ctx.Entry<Post>(this).State = EntityState.Added;
                    ctx.SaveChanges();
                    bResult = true;
                }

                return bResult;
            }
        }

        public Post UpdatePost(Post newPost)
        {
            using (ModelPostCommentContainerContainer ctx = new ModelPostCommentContainerContainer())
            {
                Post oldPost = ctx.Post.Find(newPost.PostId);
                if (oldPost == null) // nu exista in bd
                {
                    return null;
                }

                oldPost.Description = newPost.Description;
                oldPost.Domain = newPost.Domain;
                oldPost.Date = newPost.Date;
                ctx.SaveChanges();
                return oldPost;
            }
        }

        public int DeletePost(int id)
        {
            using (ModelPostCommentContainerContainer ctx = new ModelPostCommentContainerContainer())
            {
                return ctx.Database.ExecuteSqlCommand("Delete From Post where postid =@p0", id);
            }
        }

        public Post GetPostById(int id)
        {
            using (ModelPostCommentContainerContainer ctx = new ModelPostCommentContainerContainer())
            {
                var items = from p in ctx.Post where (p.PostId == id) select p;
                if (items != null)
                    return items.Include(c => c.Comments).SingleOrDefault();
                return null; // trebuie verificat in apelant
            }
        }

        public List<Post> GetAllPosts()
        {
            using (ModelPostCommentContainerContainer ctx = new ModelPostCommentContainerContainer())
            {
                return ctx.Post.Include("Comments").ToList<Post>();
                // Obligatoriu de verificat in apelant rezultatul primit.
            }
        }
    }
}
