using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using WebTourism.CollectDataBaseInfo;

namespace WebTourism.Pages
{
    public partial class OwnAdvertisement
    {
        [Inject]
        private WorkWithDatabase? _workWithDatabase { get; set; }

        [CascadingParameter]
        private Task<AuthenticationState> authenticationStateTask { get; set; }

        private Guid userID = Guid.NewGuid();

        private List<Models.Posts> Posts = new List<Models.Posts>();
        protected override async Task OnInitializedAsync()
        {
            var user = await authenticationStateTask;
            userID = Guid.Parse(user.User.Claims.FirstOrDefault().Value);
            Posts = await _workWithDatabase.GetPostsByCreatorIDAsync(userID);
        }

        private async void DeleteRecord(Guid guid)
        {
            if (await _workWithDatabase.DeletePostByIDAsync(guid))
            {
                Posts = await _workWithDatabase.GetPostsByCreatorIDAsync(userID);
                StateHasChanged();
            }
        }
    }
}