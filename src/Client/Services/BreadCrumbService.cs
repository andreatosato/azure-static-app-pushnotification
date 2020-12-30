namespace Blazoring.PWA.Client.Services
{
	public class Routes
	{
		public static string Home() => "/";

		public static string EventList() => $"/{BreadCrum.EventList}";
		public static string EventEdit(string id) => $"/{BreadCrum.EventEdit}/{id}";
		public static string EventCreate() => $"/{BreadCrum.EventCreate}";

		public static string CommunityList() => $"/{BreadCrum.CommunityList}";
		public static string CommunityEdit(string shortname) => $"/{BreadCrum.CommunityEdit}/{shortname}";
		public static string CommunityCreate() => $"/{BreadCrum.CommunityCreate}";

		public static string EventReports() => $"/{BreadCrum.EventReport}";
	}

	public class BreadCrum
	{
		public const string Home = "";
		public const string EventList = "event";
		public const string EventEdit = "event/edit/";
		public const string EventCreate = "event/create";
		public const string EventReport = "event/reports";

		public const string CommunityList = "community";
		public const string CommunityEdit = "community/edit";
		public const string CommunityCreate = "community/create";
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
