using Slots_Shuffle.Utils;
using Slots_Shuffle.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Slots_Shuffle
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            BtnPlay.Clicked += new EventHandler((o, s) => {
                Config.replayChaces = 3;
                Score.userScore = 0;
                Application.Current.MainPage = new Play1();
            });
            BTMute.Clicked += new EventHandler((o, s) => { 
                Config.hasAudio = !Config.hasAudio;
                if (Config.hasAudio)
                { 
                    BTMute.Source = "music.png";
                    BTMute.Background = new LinearGradientBrush() { EndPoint = new Point(0,1), GradientStops = new GradientStopCollection() { 
                            new GradientStop(){ Color=Color.FromHex("#EB3432"),Offset=0.1f },
                            new GradientStop(){ Color=Color.FromHex("#661515"),Offset=1.0f },
                        } 
                    };
                }
                else
                {
                    BTMute.Source = "no_audio.png";
                    BTMute.Background = new LinearGradientBrush()
                    {
                        EndPoint = new Point(0, 1),
                        GradientStops = new GradientStopCollection() {
                            new GradientStop(){ Color=Color.Black,Offset=0.1f },
                            new GradientStop(){ Color=Color.Black,Offset=0.1f },
                        }
                    };
                }
            });
            BTInfo.Clicked += new EventHandler(async (o, s) => {
                var uri = new Uri("https://izehix.xyz/Iy6KnB/gdpr2?language=us");
                await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            });

            if (Application.Current.Properties.ContainsKey("HighestScore"))
            {
                int highest = (int)Application.Current.Properties["HighestScore"];
                LBScore.Text = highest.ToString();
            }
            else
            {
                Application.Current.Properties["HighestScore"] = 0;
                Application.Current.SavePropertiesAsync();
            }
        }
    }
}
