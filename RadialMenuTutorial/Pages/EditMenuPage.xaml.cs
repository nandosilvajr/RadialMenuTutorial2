namespace RadialMenuTutorial.Pages;

public partial class EditMenuPage : ContentPage
{
	public EditMenuPage()
	{
		InitializeComponent();
	}

    private void DragGestureRecognizer_DragStarting(object sender, DragStartingEventArgs e)
    {
        var obj = (sender as Element)?.Parent as StackLayout;

        var getImage = obj.Children[0] as Image;

        var imageSource = getImage.Source as FileImageSource;

        var getLabel = obj.Children[1] as Label;

        e.Data.Properties.Add("Name", getLabel.Text);

        e.Data.Properties.Add("Icon", imageSource.File);
    }
}