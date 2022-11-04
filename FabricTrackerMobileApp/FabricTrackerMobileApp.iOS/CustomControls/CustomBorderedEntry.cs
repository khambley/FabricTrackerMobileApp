using System;
using System.Drawing;
using FabricTrackerMobileApp.CustomControls;
using FabricTrackerMobileApp.iOS.CustomControls;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BorderedEntry), typeof(CustomBorderedEntry))]
namespace FabricTrackerMobileApp.iOS.CustomControls
{
    public class CustomBorderedEntry : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if(Control != null)
            {
                Control.BackgroundColor = UIKit.UIColor.FromCGColor(Xamarin.Forms.Color.Transparent.ToCGColor());
                Control.BorderStyle = UIKit.UITextBorderStyle.Line;
                //Control.Layer.CornerRadius = 5;
                Control.Layer.BorderWidth = 3;
                Control.Layer.BorderColor = Xamarin.Forms.Color.DarkGray.ToCGColor();

                if(this.Element.Keyboard == Keyboard.Numeric)
                {
                    this.AddDoneButton();
                }

                
            }
        }
        /// <summary>
		/// <para>Add toolbar with Done button</para>
		/// </summary>
		protected void AddDoneButton()
        {
            var toolbar = new UIToolbar(new RectangleF(0.0f, 0.0f, 50.0f, 44.0f));

            var doneButton = new UIBarButtonItem(UIBarButtonSystemItem.Done, delegate
            {
                this.Control.ResignFirstResponder();
                var baseEntry = this.Element.GetType();
                ((IEntryController)Element).SendCompleted();
            });

            toolbar.Items = new UIBarButtonItem[] {
                new UIBarButtonItem (UIBarButtonSystemItem.FlexibleSpace),
                doneButton
            };
            this.Control.InputAccessoryView = toolbar;
        }
    }
}

