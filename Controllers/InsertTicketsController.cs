using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using WebTourism.CollectDataBaseInfo;

namespace WebTourism.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class InsertTicketsController : ControllerBase
    {
        // thanks Blazor, we can inject our class that manage db connection and 
        // another necessary staff with db
        [Inject]
        WorkWithDatabase? workWithDatabase { get; set; }
        public IServiceScopeFactory _serviceScopeFactory { get; set; }

        public InsertTicketsController(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            workWithDatabase = new WorkWithDatabase(_serviceScopeFactory);
        }

        [HttpGet]
        public async Task<List<Models.Posts>> GetFuncAsync()
        {
            List<Models.Posts> posts = await workWithDatabase?.GetActivePostsAsync();
            return posts;
        }

        [HttpGet("{guid}")]
        public async Task<List<Models.Posts>> GetFuncAsync(Guid guid)
        {
            List<Models.Posts> posts = await workWithDatabase?.GetPostsByCreatorIDAsync(guid);
            return posts;
        }

        [HttpPost]
        public async Task<IActionResult> ActionPost([FromBody] object jsonPost)
        {
            bool isPosted = await workWithDatabase.PostElementAsync(jsonPost.ToString());
            if (!isPosted) return BadRequest("Something wrong with your data. Ciao");
            return Ok();
        }

        [HttpPut("{guid}")]
        public async Task<IActionResult> ActionPut([FromBody] string posts, Guid guid)
        {
            bool isPosted = await workWithDatabase.PutPostByIdAsync(guid, posts);
            if (isPosted) return Ok();
            return BadRequest("Something wrong");
        }

        [HttpDelete("{postGuid}")]
        public async Task<IActionResult> ActionDelete(Guid postGuid)
        {
            bool isDeleted = await workWithDatabase.DeletePostByIDAsync(postGuid);
            if (isDeleted)
            {
                return Ok();
            }
            return BadRequest("Probably wrong guid");
        }
    }
}
