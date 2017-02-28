using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using SPControls.MarkdownTextView.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(SPControls.Forms.MarkdownTextView), typeof(MarkdownTextViewRenderer))]
namespace SPControls.MarkdownTextView.iOS
{
    public class MarkdownTextViewRenderer : LabelRenderer
    {
        public SPControls.Forms.MarkdownTextView TextView
        {
            get
            {
                return (SPControls.Forms.MarkdownTextView)Element;
            }
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            if(!string.IsNullOrEmpty(TextView.Markdown))
            {
                Control.AttributedText = TextUtil.GetAttributedStringFromHtml(TextView.Markdown.GetHtmlFromMarkdown(true));
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if(e.PropertyName == "Markdown" && !string.IsNullOrEmpty(TextView.Markdown))
            {
                Control.AttributedText = TextUtil.GetAttributedStringFromHtml(TextView.Markdown.GetHtmlFromMarkdown(true));
            }
        }
    }

}