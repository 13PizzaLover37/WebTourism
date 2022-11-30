using Microsoft.AspNetCore.Components;
using WebTourism.CollectDataBaseInfo;

namespace WebTourism.Pages
{
    public partial class Advertisement
    {
        [Inject]
        private IServiceScopeFactory serviceScopeFactory { get; set; }

        private List<Models.Posts> listOrders = new List<Models.Posts>();
        protected override async void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                WorkWithDatabase workWithDatabase = new WorkWithDatabase(serviceScopeFactory);
                listOrders = await workWithDatabase.GetActivePostsAsync();
                StateHasChanged();
            }
        }
    }
}