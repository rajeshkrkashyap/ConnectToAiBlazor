namespace ConnectToAi.MobileApp;

public partial class AudioPayer : ContentView
{
	public AudioPayer()
	{
		InitializeComponent();
	}
    private void PlayButton_Click(object sender, EventArgs e)
    {
        this.framePlay.IsVisible = false;
        this.framePause.IsVisible = true;
    }
    private void PauseButton_Click(object sender, EventArgs e)
    {
        this.framePlay.IsVisible = true;
        this.framePause.IsVisible = false;
    }
}