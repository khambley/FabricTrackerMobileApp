using System;
using FabricTrackerMobileApp.CustomControls;
using FabricTrackerMobileApp.iOS.CustomControls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BorderedDatePicker), typeof(CustomBorderedDatePicker))]
namespace FabricTrackerMobileApp.iOS.CustomControls
{
    public class CustomBorderedDatePicker : DatePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
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

