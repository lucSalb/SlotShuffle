using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Service.Controls;
using Android.Views;
using Android.Widget;
using Slots_Shuffle.CustomRenders;
using Slots_Shuffle.Droid.CustomRenders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(BTNGamePiece), typeof(BTNGamePieceRender))]
namespace Slots_Shuffle.Droid.CustomRenders
{
    public class BTNGamePieceRender : ImageButtonRenderer
    {
        public BTNGamePieceRender(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ImageButton> e)
        {
            base.OnElementChanged(e);
            if (this != null)
            {
                this.Touch += Control_Touch;
            }
        }

        private void Control_Touch(object sender, TouchEventArgs e)
        {
            switch(e.Event.Action & MotionEventActions.Mask)
            {
                case MotionEventActions.Down:
                    ((BTNGamePiece)Element).InvokeTouch("Down");
                    break;
                case MotionEventActions.Move:
                    ((BTNGamePiece)Element).InvokeTouch("Move");
                    break;
                case MotionEventActions.Outside:
                    ((BTNGamePiece)Element).InvokeTouch("Outside");
                    break;
            }
            e.Handled = false;
        }
    }
}