﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laborator6
{
    public partial class Comment
    {
        public bool AddComment()
        {
            using (ModelPostCommentContainerContainer ctx = new ModelPostCommentContainerContainer())
            {
                bool bResult = false;
                if (this == null || this.PostPostId == 0)
                    return bResult;
                if (this.CommentId == 0)
                {
                    ctx.Entry<Comment>(this).State = EntityState.Added;
                    Post p = ctx.Post.Find(this.PostPostId);
                    ctx.Entry<Post>(p).State = EntityState.Unchanged;
                    ctx.SaveChanges();
                    bResult = true;
                }

                return bResult;
            }
        }

        public Comment UpdateComment(Comment newComment)
        {
            using (ModelPostCommentContainerContainer ctx = new ModelPostCommentContainerContainer())
            {
                Comment oldComment = ctx.Comments.Find(newComment.CommentId);
                // Deoarece parametrul este un Comment ar trebui verificata fiecare
                // proprietate din newComment daca are valoare atribuita si
                // daca valoarea este diferita de cea din bd.
                // Acest lucru il fac numai la modificarea asocierii.
                if (newComment.Text != null)
                    oldComment.Text = newComment.Text;
                if ((oldComment.PostPostId != newComment.PostPostId)
                    && (newComment.PostPostId != 0))
                {
                    oldComment.PostPostId = newComment.PostPostId;
                }

                ctx.SaveChanges();
                return oldComment;
            }
        }

        public Comment GetCommentById(int id)
        {
            using (ModelPostCommentContainerContainer ctx = new ModelPostCommentContainerContainer())
            {
                var items = from c in ctx.Comments where (c.CommentId == id) select c;
                return items.Include(p => p.Post).SingleOrDefault();
            }
        }
    }
}