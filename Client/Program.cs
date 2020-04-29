using Grpc.Net.Client;
using System;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // The port number(5001) must match the port of the gRPC server.
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");


            var clientAPI = new MyService.MyServiceClient(channel);
            while (true)
            {
                var lists = clientAPI.GetAllPosts(new RequestById() { Id = "1" });
                for (var i = 0; i < lists.Posts.Count; i++)
                {
                    Console.WriteLine("Domain" + lists.Posts[i].Domain);
                    Console.WriteLine("Description" + lists.Posts[i].Description);
                    Console.WriteLine("Date" + lists.Posts[i].Date);
                }
                Console.WriteLine("enter a post");
                var newPost = new RequestPost();
                Console.WriteLine("Enter a description");
                newPost.Description = Console.ReadLine();
                Console.WriteLine("Enter a domain");
                newPost.Domain = Console.ReadLine();
                Console.WriteLine("Date will be" + DateTime.Now.ToString());
                newPost.Date = DateTime.Now.ToString();
                clientAPI.AddPost(newPost);


            }
        }
    }
}