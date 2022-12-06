using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebTourism.Context;
using WebTourism.Models;

namespace WebTourism.CollectDataBaseInfo
{
    public class WorkWithDatabase
    {
        private IServiceScopeFactory serviceScope { get; set; } 
        public WorkWithDatabase(IServiceScopeFactory webHostEnvironment)
        {
            this.serviceScope = webHostEnvironment;
        }
        public async Task<List<Models.Posts>> GetActivePostsAsync()
        {
            using (IServiceScope scope = serviceScope.CreateScope())
            {
                WebTourismDBContext context = scope.ServiceProvider.GetRequiredService<WebTourismDBContext>();
                List<Models.Posts> activePosts = context.Posts.Where(elem => elem.IsActive == "1").ToList();

                return activePosts;
            }
        }

        public async Task CreateNewPostAsync(Posts post)
        {
            using (IServiceScope scope = serviceScope.CreateScope())
            {
                try
                {
                    WebTourismDBContext context = scope.ServiceProvider.GetRequiredService<WebTourismDBContext>();
                    post.Id = Guid.NewGuid();
                    post.IsActive = "1";
                    await context.Posts.AddAsync(post);
                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        public async Task<List<Models.Posts>> GetPostsByCreatorIDAsync(Guid creator)
        {
            using (IServiceScope scope = serviceScope.CreateScope())
            {
                WebTourismDBContext context = scope.ServiceProvider.GetRequiredService<WebTourismDBContext>();

                List<Models.Posts> requiredPosts = await context.Posts.Where(elem => elem.CreatorId == creator).ToListAsync();
                return requiredPosts;
            }
        }

        public async Task<bool> PostElementAsync(string jsonElement)
        {
            Posts posts = JsonConvert.DeserializeObject<Posts>(jsonElement);
            using (IServiceScope scope = serviceScope.CreateScope())
            {
                WebTourismDBContext context = scope.ServiceProvider.GetRequiredService<WebTourismDBContext>();
                posts.Id = posts.Id == null ? Guid.NewGuid() : posts.Id;
                posts.CreatorId ??= Guid.NewGuid();
                posts.IsActive = "1";
                context.Posts.Add(posts);
                await context.SaveChangesAsync();
            }
            return true;
        }

        public async Task<bool> PutPostByIdAsync(Guid guid, string jsonElement)
        {
            Posts postFromJson = JsonConvert.DeserializeObject<Posts>(jsonElement);

            using (IServiceScope scope = serviceScope.CreateScope())
            {
                WebTourismDBContext context = scope.ServiceProvider.GetRequiredService<WebTourismDBContext>();
                try
                {
                    Posts postForUpdate = await context.Posts.Where(elem => elem.Id == guid).FirstOrDefaultAsync();
                    postForUpdate.Text = postFromJson?.Text == null ? postForUpdate?.Text : postFromJson?.Text;
                    postForUpdate.Header = postFromJson?.Header == null ? postForUpdate?.Header : postFromJson?.Header;
                    return true;

                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public async Task<bool> DeletePostByIDAsync(Guid postGuid)
        {
            using (IServiceScope scope = serviceScope.CreateScope())
            {
                WebTourismDBContext context = scope.ServiceProvider.GetRequiredService<WebTourismDBContext>();

                Posts postForDelete = await context.Posts.Where(elem => elem.Id == postGuid).FirstOrDefaultAsync();
                if (postForDelete == null) return false;

                context.Posts.Remove(postForDelete);
                await context.SaveChangesAsync();
            }
            return true;
        }


    }
}
