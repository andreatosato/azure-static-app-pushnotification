﻿using Blazorise;
using Blazorise.Sidebar;

namespace Blazoring.PWA.Client.Stores
{
	public static class AppStore
	{
		public static Sidebar Sidebar { get; set; } = new Sidebar();
		public static Theme Tema { get; set; } = new Theme()
		{
			ColorOptions = new ThemeColorOptions
			{
				Primary = "#3700B3",
				Secondary = "#018786",
			},
			BackgroundOptions = new ThemeBackgroundOptions
			{
				Primary = "#3700B3",
			},
			SidebarOptions = new ThemeSidebarOptions
			{
				Color = "#FFFFFF",
				BackgroundColor = "#3700B3"
			},
		};
	}
}
