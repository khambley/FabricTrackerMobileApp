using System;
using FabricTrackerMobileApp.CustomControls;
using FabricTrackerMobileApp.iOS.CustomControls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BorderedPicker), typeof(CustomBorderedPicker))]
namespace FabricTrackerMobileApp.iOS.CustomControls
{
    public class CustomBorderedPicker : PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            if(Control != null)
            {
                Control.BackgroundColor = UIKit.UIColor.FromCGColor(Color.Transparent.ToCGColor());
                Control.BorderStyle = UIKit.UITextBorderStyle.Line;
                Control.Layer.CornerRadius = 5;
                Control.Layer.BorderWidth = 3;
                Control.Layer.BorderColor = Color.DarkGray.ToCGColor();
                
            }
        }
    }
}

