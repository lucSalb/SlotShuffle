using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Slots_Shuffle.Models
{
    class GamePieces
    {
        public int value { get; set; }
        public string Piece { get; set; }
    }

    class SelectedGamePieces
    {
        public int value { get; set; }

        public Point Position { get; set; }


        public ImageButton Piece { get; set; }
    }
}
