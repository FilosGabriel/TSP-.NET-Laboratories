using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace database
{

    public static class API
    {
        public static void AddPost(Post post)
        {
            using DatabaseContext ctx = new DatabaseContext();
            ctx.Posts.Add(post);
            ctx.SaveChanges();
        }
        public static Post UpdatePost(Post newPost)
        {
            using DatabaseContext ctx = new DatabaseContext();
            Post oldPost = ctx.Posts.Find(newPost.PostId);
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
        public static void DeletePost(int id)
        {
            using DatabaseContext ctx = new DatabaseContext();
            var entity = ctx.Posts
                .Where(b => b.PostId == id)
                .FirstOrDefault();
            ctx.Posts.Remove(entity);
        }

        public static Post GetPostById(int id)
        {
            using DatabaseContext ctx = new DatabaseContext();
            return ctx.Posts.Where(i => i.PostId == id).FirstOrDefault();
        }

        public static List<Post> GetAllPosts()
        {
            using DatabaseContext ctx = new DatabaseContext();
            return ctx.Posts.Include(e => e.comments).ToList<Post>(); 
        }

        public static void AddComment(Comment comment)
        {
            using DatabaseContext ctx = new DatabaseContext();
            ctx.Comments.Add(comment);
            ctx.SaveChanges();
        }

        public static Comment UpdateComment(Comment newComment)
        {
            using DatabaseContext ctx = new DatabaseContext();
            Comment oldComment = ctx.Comments.Find(newComment.CommentId);
            if (newComment.Text != null)
                oldComment.Text = newComment.Text;

            if ((oldComment.PostPostId != newComment.PostPostId) && (newComment.PostPostId != 0))
            {
                oldComment.PostPostId = newComment.PostPostId;
            }
            ctx.SaveChanges();
            return oldComment;
        }
        public static Comment GetCommentById(int id)
        {
            using DatabaseContext ctx = new DatabaseContext();
            return ctx.Comments.Where(e => e.CommentId == id).FirstOrDefault();
        }
    }
}