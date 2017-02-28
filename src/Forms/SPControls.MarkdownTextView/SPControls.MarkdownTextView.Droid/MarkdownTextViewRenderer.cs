using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using SPControls;
using Xamarin.Forms;
using System.ComponentModel;
using SPControls.Forms;

[assembly: ExportRenderer(typeof(SPControls.Forms.MarkdownTextView), typeof(MarkdownTextViewRenderer))]
namespace SPControls
{
    public class MarkdownTextViewRenderer : LabelRenderer
    {
        public MarkdownTextView TextView
        {
            get
            {
                return (MarkdownTextView)Element;
            }
        }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if(e.PropertyName == "Markdown" && !string.IsNullOrEmpty(TextView.Markdown))
            {
                Control.TextFormatted = TextUtil.GetFormattedHtml(TextView.Markdown.GetHtmlFromMarkdown(true));
            }
        }
    }
}