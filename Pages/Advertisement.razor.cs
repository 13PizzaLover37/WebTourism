using Microsoft.AspNetCore.Components;
using WebTourism.CollectDataBaseInfo;

namespace WebTourism.Pages
{
    public partial class Advertisement
    {
        [Inject]
        private WorkWithDatabase _workWithDatabase { get; set; }

        private List<Models.Posts> listOrders = new List<Models.Posts>();
        protected override async Task OnInitializedAsync()
        {
            listOrders = await _workWithDatabase.GetActivePostsAsync();
            base.OnInitialized();
        }
    }
}