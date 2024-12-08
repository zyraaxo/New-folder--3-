using Microsoft.Maui.Storage;
using System.Collections.ObjectModel;
using System.Linq; // Add this
using System.Windows.Input;

namespace MauiApp1
{
	public partial class MainPage : ContentPage
	{
		public ObservableCollection<FileItem> Files { get; set; }

		public ICommand FileTappedCommand { get; }

		// Track the index of the current image being displayed
		private int currentImageIndex = -1;

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

						await DisplayAlert("File Added", $"File: {result.FileName} has been added.", "OK");
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
				// Set the selected image
				SelectedImage.Source = ImageSource.FromFile(filePath);
				SelectedImage.IsVisible = true;

				// Update the current image index
				currentImageIndex = Files.ToList().IndexOf(Files.FirstOrDefault(f => f.FilePath == filePath));

				// Make the 'Next Image' button visible
				AnotherActionButton.IsVisible = true;
			}
		}

		private void OnAnotherActionClicked(object sender, EventArgs e)
		{
			// Check if there are multiple files
			if (Files.Count > 1)
			{
				// Increment the image index (loop back to 0 if it's the last image)
				currentImageIndex = (currentImageIndex + 1) % Files.Count;

				// Display the next image in the collection
				var nextFilePath = Files[currentImageIndex].FilePath;
				SelectedImage.Source = ImageSource.FromFile(nextFilePath);
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
