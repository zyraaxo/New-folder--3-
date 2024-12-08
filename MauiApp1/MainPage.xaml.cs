using Microsoft.Maui.Storage;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MauiApp1
{
	public partial class MainPage : ContentPage
	{
		public ObservableCollection<FileItem> Files { get; set; }

		public ICommand FileTappedCommand { get; }

		public MainPage()
		{
			InitializeComponent();

			Files = new ObservableCollection<FileItem>();
			FileTappedCommand = new Command<string>(OnFileTapped);

			BindingContext = this;
		}

		private async void OnAddFilesClicked(object sender, EventArgs e)
		{
			try
			{
				var result = await FilePicker.PickAsync();

				if (result != null)
				{
					if (result.FileName.EndsWith(".png", StringComparison.OrdinalIgnoreCase) || result.FileName.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase))
					{
						Files.Add(new FileItem { FileName = result.FileName, FilePath = result.FullPath });

					}
					else
					{
						await DisplayAlert("Invalid File", "Only PNG files are supported.", "OK");
					}
				}
				else
				{
					await DisplayAlert("No File Selected", "The user canceled the file pick.", "OK");
				}
			}
			catch (Exception ex)
			{
				await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
			}
		}

		private void OnFileTapped(string filePath)
		{
			if (filePath.EndsWith(".png", StringComparison.OrdinalIgnoreCase) || filePath.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase))
			{
				SelectedImage.Source = ImageSource.FromFile(filePath);
				SelectedImage.IsVisible = true;
			}
		}
		private async void OnImageButtonClicked(object sender, EventArgs e)
		{
			await DisplayAlert("Button Clicked", "You clicked the button next to the image!", "OK");
		}
	}

	public class FileItem
	{
		public required string FileName { get; set; }
		public required string FilePath { get; set; }
	}
}
