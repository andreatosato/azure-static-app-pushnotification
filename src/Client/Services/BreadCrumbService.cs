namespace Blazoring.PWA.Client.Services
{
	public class Routes
	{
		public static string Home() => "/";

		public static string UsersList() => $"/{BreadCrum.UsersList}";
		public static string UserEdit(int id) => $"/{BreadCrum.UserEdit}/{id}";
		public static string UserCreate() => $"/{BreadCrum.UserCreate}";

		public static string UsersGrid() => $"/{BreadCrum.UsersGrid}";
		public static string UsersGridEdit(int id) => $"/{BreadCrum.UsersGrid}/{id}";
		public static string Notifications() => $"/{BreadCrum.Notifications}";
	}

	public class BreadCrum
	{
		public const string Home = "";
		public const string UsersList = "users";
		public const string UserEdit = "users/edit";
		public const string UserCreate = "users/create";
		public const string UsersGrid = "usersgrid";
		public const string UsersGridEdit = "usersgrid/edit";
		public const string Notifications = "notifications";
	}

	public class BreadCrumLink
	{
		public BreadCrumLink(string link, string description, bool active = false)
		{
			Link = link;
			Description = description;
			Active = active;
		}
		public string Link { get; }
		public string Description { get; }
		public bool Active { get; }
	}
}
