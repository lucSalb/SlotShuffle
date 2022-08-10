using Rg.Plugins.Popup.Extensions;
using Slots_Shuffle.CustomRenders;
using Slots_Shuffle.Models;
using Slots_Shuffle.PopUpView;
using Slots_Shuffle.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Slots_Shuffle.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Play1 : ContentPage
    {
        //RANDOM NUMBER GENERATOR
        private readonly Random _random = new Random();
        //LIST OF TYPE OF PIECES
        static List<GamePieces> elements = new List<GamePieces>()
        {
            new GamePieces(){value=1, Piece="clover.png" },
            new GamePieces(){value=2, Piece="heart.png" },
            new GamePieces(){value=3, Piece="horseshoe.png" },
            new GamePieces(){value=4, Piece="lime.png" },
            new GamePieces(){value=5, Piece="cherries.png" },
       }; 
        //ASSIGNS A RANDOM GENERATED PIECE TO THE GRID
        GamePieces[,] tableContent = new GamePieces[3,5]; 
        //GAME MAP STORES WEIGHT IN EACH POSITION 
        int[,] gameMoveMatrix = new int[3, 5]; 
        //NUMBER OF CLEAN AREA AVAILABLE TO PRESS       
        int highest;
        public Play1()
        {
            InitializeComponent();
            PopulateGameTable();
            setClicks();
            LBreplayChances.Text = "x" + Config.replayChaces.ToString();
            highest = (int)Application.Current.Properties["HighestScore"];
            LBScore.Text = Score.userScore.ToString();
        }

        private void setClicks()
        {
            BTReplay.Clicked += new EventHandler((o,e)=> {
                if (Config.replayChaces > 0) 
                {
                    if(Config.hasAudio)AudioPlayer.Play("scroll_sound.mp3");
                    Config.replayChaces--;
                    LBreplayChances.Text = "x" + Config.replayChaces.ToString();
                    ClearTable();
                    PopulateGameTable();
                }
                else
                {
                    BTReplay.IsEnabled = false;
                    Application.Current.MainPage = new GameOver();
                }
            });
            BTPause.Clicked += new EventHandler(async (o, e) => {                
                await Navigation.PushPopupAsync(new PauseWindow());
            });   
        }

        private void ClearTable()
        {
            gameScreen.Children.Clear();
        }
        //https://social.msdn.microsoft.com/Forums/en-US/8f58b509-dad5-4e9f-867c-cfd28062c11d/buttonreleased-not-triggered-if-you-move-the-finger-a-little-while-the-button-is-still-pressed?forum=xamarinforms
        private void PopulateGameTable()
        {
            Image backgroundImage = new Image() { Source = "tabel_background.png" };

            Image col1 = new Image() { Source = "col1.png" };
            Image col2 = new Image() { Source = "col2.png" };
            Image col3 = new Image() { Source = "col2.png" };
            Image col4 = new Image() { Source = "col2.png" };
            Image col5 = new Image() { Source = "col3.png" };

            gameScreen.Children.Add(col1, 0, 0);
            Grid.SetRowSpan(col1, 3);

            gameScreen.Children.Add(col2, 1, 0);
            Grid.SetRowSpan(col2, 3);

            gameScreen.Children.Add(col3, 2, 0);
            Grid.SetRowSpan(col3, 3);
            
            gameScreen.Children.Add(col4, 3, 0);
            Grid.SetRowSpan(col4, 3);

            gameScreen.Children.Add(col5, 4, 0);
            Grid.SetRowSpan(col5, 3);

            //gameScreen.Children.Add(backgroundImage,0,0);
            //Grid.SetColumnSpan(backgroundImage, 5);
            //Grid.SetRowSpan(backgroundImage, 3);

            for (int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 5;j++)
                {
                    var aux = RandomNumber(0, elements.Count - 1);
                    
                    tableContent[i,j] = elements[aux];
                    gameMoveMatrix[i, j] = tableContent[i, j].value;

                    var auxButton = new ImageButton() { Source = tableContent[i, j].Piece, HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, Scale = 0.7, BackgroundColor = Color.Transparent };
                    auxButton.Pressed += new EventHandler((o, e) => { Element_clicked(i, j, auxButton); });
                    gameScreen.Children.Add(auxButton,j,i);
                }
            }
         }

        List<SelectedGamePieces> selectedGamePieces = new List<SelectedGamePieces>();
        private void Element_clicked(int i, int j, ImageButton BTNClicked)
        {
            if (selectedGamePieces.Count >= 5)
                return;

            bool allowedMove = false;
            int pieceWeight = 0;

            i = Grid.GetRow(BTNClicked);
            j = Grid.GetColumn(BTNClicked);

            pieceWeight = gameMoveMatrix[i, j];

            if (selectedGamePieces.Count > 0)
            {
                if (selectedGamePieces[selectedGamePieces.Count - 1].Position.X == i && (selectedGamePieces[selectedGamePieces.Count - 1].Position.Y == j - 1 || selectedGamePieces[selectedGamePieces.Count - 1].Position.Y == j + 1) ||
                   (selectedGamePieces[selectedGamePieces.Count - 1].Position.X == i - 1 || selectedGamePieces[selectedGamePieces.Count - 1].Position.X == i + 1) && (selectedGamePieces[selectedGamePieces.Count - 1].Position.Y == j - 1 ||
                   (selectedGamePieces[selectedGamePieces.Count - 1].Position.X == i + 1 && selectedGamePieces[selectedGamePieces.Count - 1].Position.Y == j) ||
                   (selectedGamePieces[selectedGamePieces.Count - 1].Position.X == i - 1  && selectedGamePieces[selectedGamePieces.Count - 1].Position.Y == j) ||
                    selectedGamePieces[selectedGamePieces.Count - 1].Position.Y == j + 1))
                {
                    if (pieceWeight == selectedGamePieces[selectedGamePieces.Count - 1].value)
                        allowedMove = true;
                    else
                        allowedMove = false;
                }
                else
                {
                    if (selectedGamePieces[selectedGamePieces.Count - 1].Position.X == i && selectedGamePieces[selectedGamePieces.Count - 1].Position.Y + 1 < j)
                    {
                        for (int aux = j; aux >= selectedGamePieces[selectedGamePieces.Count - 1].Position.Y; aux--)
                        {
                            if (gameMoveMatrix[i, aux] == pieceWeight)
                                allowedMove = true;
                            else
                                allowedMove = false;
                        }
                    }
                    else if (selectedGamePieces[selectedGamePieces.Count - 1].Position.X == i && selectedGamePieces[selectedGamePieces.Count - 1].Position.Y - 1 > j)
                    {
                        for (int aux = (int)selectedGamePieces[selectedGamePieces.Count - 1].Position.Y; aux <= j; aux++)
                        {
                            if (gameMoveMatrix[i, aux] == pieceWeight)
                                allowedMove = true;
                            else
                                allowedMove = false;
                        }
                    }
                }
            }
            else
            {
                allowedMove = true;
            }

            if (allowedMove)
            {

                selectedGamePieces.Add(new SelectedGamePieces() {
                    Position = new Point(i, j),
                    Piece = BTNClicked,
                    value = pieceWeight,
                });

                //if (hasSameColElement(selectedGamePieces))
                //{
                //    foreach (SelectedGamePieces sp in selectedGamePieces)
                //    {
                //        sp.Piece.BorderWidth = 0;
                //        sp.Piece.CornerRadius = 0;
                //        sp.Piece.Opacity = 1;
                //        sp.Piece.BorderColor = Color.Transparent;
                //    }
                //    selectedGamePieces = new List<SelectedGamePieces>();
                //    return;
                //}
                BTNClicked.BorderWidth = 3;
                BTNClicked.CornerRadius = 5;
                BTNClicked.Opacity = 0.85;
                BTNClicked.BorderColor = Color.Red;

                if (selectedGamePieces.Count == 3)
                {
                    Device.StartTimer(new TimeSpan(0,0,2), () =>
                    {
                        if (selectedGamePieces.Count >= 3)
                        {
                            AudioPlayer.Play("path_matched.mp3");
                            switch (selectedGamePieces.Count)
                            {
                                case 3:
                                    Score.userScore += 1000;
                                    break;
                                case 4:
                                    Score.userScore += 1500;
                                    break;
                                case 5:
                                    Score.userScore += 2500;
                                    break;
                            }
                            LBScore.Text = Score.userScore.ToString();
                            if (Score.userScore > highest)
                            {
                                Application.Current.Properties["HighestScore"] = Score.userScore;
                                highest = Score.userScore;
                                Application.Current.SavePropertiesAsync();
                            }

                            foreach (SelectedGamePieces game in selectedGamePieces)
                            {
                                ReplacePiece(game);
                            }
                            selectedGamePieces = new List<SelectedGamePieces>();
                        }
                        return false;
                    });
                }
            }
            else
            {
                foreach(SelectedGamePieces sp in selectedGamePieces)
                {
                    sp.Piece.BorderWidth = 0;
                    sp.Piece.CornerRadius = 0;
                    sp.Piece.Opacity = 1;
                    sp.Piece.BorderColor = Color.Transparent;
                }
                selectedGamePieces = new List<SelectedGamePieces>();
            }

        }

        private int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }

        bool hasSameColElement(List<SelectedGamePieces> element_list)
        {
            foreach (SelectedGamePieces game in element_list)
            {
                var found = element_list.Where(e => e.Position.Y == game.Position.Y);
                if (found.Count() > 1) return true;
            }
            return false;
        }

        void ReplacePiece(SelectedGamePieces game)
        {

            game.Piece.BorderWidth = 0;
            game.Piece.CornerRadius = 0;
            game.Piece.Opacity = 1;
            game.Piece.BorderColor = Color.Transparent;

            gameScreen.Children.Remove(game.Piece);

            var aux = RandomNumber(0, elements.Count - 1);

            tableContent[(int)game.Position.X, (int)game.Position.Y] = elements[aux];
            gameMoveMatrix[(int)game.Position.X, (int)game.Position.Y] = tableContent[(int)game.Position.X, (int)game.Position.Y].value;

            var auxButton = new ImageButton() { Source = tableContent[(int)game.Position.X, (int)game.Position.Y].Piece, HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, Scale = 0.7, BackgroundColor = Color.Transparent };
            auxButton.Pressed += new EventHandler((o, e) => { Element_clicked((int)game.Position.X, (int)game.Position.Y, auxButton); });
            gameScreen.Children.Add(auxButton, (int)game.Position.Y, (int)game.Position.X);
        }

        //POPUP CLICKS
        public static void GoToMenu()
        {

        }
    }
 
}
//if (selectedGamePieces[selectedGamePieces.Count - 1].Position.X == i && selectedGamePieces[selectedGamePieces.Count - 1].Position.Y + 1 <= j)
//{
//    for (int aux = j; aux >= selectedGamePieces[selectedGamePieces.Count - 1].Position.Y; aux--)
//    {
//        if (gameMoveMatrix[i, aux] == pieceWeight)
//            allowedMove = true;
//        else
//        {
//            allowedMove = false;
//            break;
//        }
//    }
//}
//else if (selectedGamePieces[selectedGamePieces.Count - 1].Position.X == i && selectedGamePieces[selectedGamePieces.Count - 1].Position.Y - 1 >= j)
//{
//    for (int aux = (int)selectedGamePieces[selectedGamePieces.Count - 1].Position.Y; aux >= j; aux--)
//    {
//        if (gameMoveMatrix[i, aux] == pieceWeight)
//            allowedMove = true;
//        else
//        {
//            allowedMove = false;
//            break;
//        }
//    }
//}
//else if (selectedGamePieces[selectedGamePieces.Count - 1].Position.X > i && selectedGamePieces[selectedGamePieces.Count - 1].Position.Y - 1 > j)
//{
//    for (int aux = (int)selectedGamePieces[selectedGamePieces.Count - 1].Position.Y; aux >= j; aux--)
//    {
//        if (gameMoveMatrix[i, aux] == pieceWeight)
//            allowedMove = true;
//        else
//        {
//            allowedMove = false;
//            break;
//        }
//    }
//}