using Microsoft.Maui.Controls;

namespace MauiApp1
{
	public partial class AppShell : Shell
	{
		public AppShell()
		{
			InitializeComponent();

			// Set navigation bar title color explicitly
			Shell.SetTitleColor(this, Colors.White);
		}
	}
}
