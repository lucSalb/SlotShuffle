using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Slots_Shuffle.CustomRenders
{
    public class BTNGamePiece : ImageButton
    {
        public event Action<string> TouchDown; 
        public event Action<string> TouchUp; 
        public void InvokeTouch(string data) {
            if(data!=null)
            {
                switch (data)
                {
                    case "Touch Down":
                        //TouchDown.Invoke(data);
                        break;
                    case "Touch Up":
                        break;
                }
            }
          
            Console.WriteLine(data);
            //MotionEventActions.Cancel
        }
    }
}
