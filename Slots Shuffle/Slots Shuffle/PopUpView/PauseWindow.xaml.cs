using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Slots_Shuffle.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Slots_Shuffle.PopUpView
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PauseWindow : PopupPage
	{
		public PauseWindow ()
		{
			InitializeComponent ();

			LBScore.Text = "Score: " + Score.userScore.ToString();
			BTResume.Clicked += new EventHandler(async (o, s) => { await PopupNavigation.Instance.PopAsync(); });
			BTMenu.Clicked += new EventHandler(async (o, s) => { await PopupNavigation.Instance.PopAsync(); Application.Current.MainPage = new MainPage(); });
		}
	}
}