using Blazoring.PWA.Client.Services;
using Blazoring.PWA.Client.Stores;
using Blazoring.PWA.Shared;
using Blazorise.Snackbar;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Blazoring.PWA.Client.Shared
{
    public partial class MainLayout : LayoutComponentBase
    {
        [Inject]
        public ISnackbarService SnackbarService { get; set; }
        [Inject]
        public IJSRuntime JSRuntime { get; set; }
        [Inject]
        public HttpClient client { get; set; }

        protected override void OnInitialized()
        {
#if DEBUG
            Thread.Sleep(10000);
#endif
            SnackbarService.OnShow += OnShow;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            if(firstRender)
                await RequestNotificationSubscriptionAsync();
        }

        public void OnShow(string message, SnackbarType type)
        {
            SnackbarType = type;
            SnackbarMessage = message;
            snackbarRef.Show();
        }

        Snackbar snackbarRef;
        SnackbarType SnackbarType;
        string SnackbarMessage;

#pragma warning disable IDE0052 // Remove unread private members
        string menuIcon = "menu";
#pragma warning restore IDE0052 // Remove unread private members
        void ToggleSidebar()
        {
            if (AppStore.Sidebar.Visible)
                menuIcon = "menu";
            else
                menuIcon = "menu_open";
            AppStore.Sidebar.Toggle();
        }

        #region [Push]
        private async Task RequestNotificationSubscriptionAsync()
        {
            var subscription = await JSRuntime.InvokeAsync<NotificationSubscription>("blazoring.requestSubscription");
            if (subscription != null)
            {
                await SubscribeToNotifications(subscription);
            }
        }

        public async Task SubscribeToNotifications(NotificationSubscription subscription)
        {
            var response = await client.PostAsJsonAsync("NotificationSubscribe", subscription);
            response.EnsureSuccessStatusCode();
        }
        #endregion

        public void Dispose()
        {
            SnackbarService.OnShow -= OnShow;
        }
    }
}