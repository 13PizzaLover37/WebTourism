using Microsoft.EntityFrameworkCore;
using WebTourism.Context;

namespace WebTourism.CollectDataBaseInfo
{
    public class WorkWithDatabase
    {
        private IServiceScopeFactory serviceScope { get; set; } = null;
        public WorkWithDatabase(IServiceScopeFactory webHostEnvironment)
        {
            this.serviceScope = webHostEnvironment;
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

        public async Task<List<Models.Posts>> GetActivePostsAsync()
        {
            using (IServiceScope scope = serviceScope.CreateScope())
            {
                WebTourismDBContext context = scope.ServiceProvider.GetRequiredService<WebTourismDBContext>();
                List<Models.Posts> activePosts = await context.Posts.Where(elem => elem.IsActive == true.ToString()).ToListAsync();

                return activePosts;
            }
        }
    }
}
