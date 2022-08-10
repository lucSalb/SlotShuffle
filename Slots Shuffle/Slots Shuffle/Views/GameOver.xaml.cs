using Slots_Shuffle.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Slots_Shuffle.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GameOver : ContentPage
	{
		public GameOver ()
		{
			InitializeComponent ();

			AudioPlayer.Play("game_over.mp3");

			LBScore.Text = Score.userScore.ToString();
			LBBestScore.Text = "Best Score: " + Application.Current.Properties["HighestScore"].ToString();

			BTAgain.Clicked += new EventHandler((o, e) => {
				Config.replayChaces = 3;
				Score.userScore = 0;
				Application.Current.MainPage = new Play1();
			});
			BTMenu.Clicked+=new EventHandler((o, e) =>{
				Application.Current.MainPage = new MainPage();
			});
		}
	}
}