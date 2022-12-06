using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using WebTourism.CollectDataBaseInfo;

namespace WebTourism.Pages
{
    public partial class AddAdvertisement
    {
        [Inject]
        private WorkWithDatabase? workWithDatabase { get; set; }

        [CascadingParameter]
        private Task<AuthenticationState> authenticationStateTask { get; set; }
        private Models.Posts post { get; set; } = new Models.Posts();
        private Guid userID = Guid.NewGuid();


        //IReadOnlyList<IBrowserFile> selectedFiles;

        public async void SavePost()
        {
            var user = await authenticationStateTask;
            userID = Guid.Parse(user.User.Claims.FirstOrDefault().Value);

            post.CreatorId = userID;
            workWithDatabase.CreateNewPostAsync(post);
        }

        //void HandleSelection(InputFileChangeEventArgs eventArgs)
        //{
        //    var file = eventArgs.File;
        //}
        //<div class="row">
        //        <div class="col-md-2">
        //            <label>Photo:</label>
        //        </div>
        //        <div class="col-md-10">
        //            <InputFile OnChange = "HandleSelection" />
        //        </ div >
            //</ div >
    }
}