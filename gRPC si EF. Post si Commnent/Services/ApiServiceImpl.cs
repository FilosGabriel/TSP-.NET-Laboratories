using database;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gRPC2
{

    public class ApiServiceImpl : MyService.MyServiceBase
    {
        private readonly ILogger<ApiServiceImpl> _logger;
        public ApiServiceImpl(ILogger<ApiServiceImpl> logger)
        {
            _logger = logger;
        }
        public override Task<Response> AddPost(RequestPost request, ServerCallContext context)
        {
            API.AddPost(new Post() { Date = DateTime.Parse(request.Date), Description = request.Description, Domain = request.Domain });
            return Task.FromResult(new Response
            {
                Message = "Completed "
            });
        }

        public override Task<ResponsePost> UpdatePost(RequestPost request, ServerCallContext context)
        {
            var post = API.UpdatePost(new Post() { Date = DateTime.Parse(request.Date), Description = request.Description, Domain = request.Domain });
            return Task.FromResult(new ResponsePost { Post = new RequestPost() { Date = request.Date, Description = request.Description, Domain = request.Domain } });
        }

        public override Task<Response> DeletePost(RequestById request, ServerCallContext context)
        {
            API.DeletePost(int.Parse(request.Id));
            return Task.FromResult(new Response() { Message = "Completed" });
        }

        public override Task<ResponsePost> GetPostById(RequestById request, ServerCallContext context)
        {
            var dates = API.GetPostById(int.Parse(request.Id));
            return Task.FromResult(new ResponsePost()
            {
                Post = new RequestPost()
                {
                    Date = dates.Date.ToString(),
                    Description = dates.Description,
                    Domain = dates.Domain

                }
            });
        }

        public override Task<ResponseListPost> GetAllPosts(RequestById request, ServerCallContext context)
        {
            _logger.LogInformation("reach");
            var listOfSomething = API.GetAllPosts();
            var response = new ResponseListPost();
            listOfSomething.ForEach(i => response.Posts.Add(new RequestPost() { Date = i.Date.ToString(), Description = i.Description, Domain = i.Domain }));
            return Task.FromResult(response);
        }

        public override Task<ResponseComment> AddComment(RequestComment request, ServerCallContext context)
        {
            API.AddComment(new Comment() { Text = request.Text });
            return Task.FromResult(new ResponseComment() { Comment = new RequestComment() { Text = request.Text } });
        }


        public override Task<ResponseComment> GetCommentById(RequestById request, ServerCallContext context)
        {
            var a = API.GetCommentById(int.Parse(request.Id));
            return Task.FromResult(new ResponseComment() { Comment = new RequestComment() { Text = a.Text } });
        }


    }
}
