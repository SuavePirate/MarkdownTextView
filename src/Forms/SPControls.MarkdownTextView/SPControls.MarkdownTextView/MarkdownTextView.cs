using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SPControls.Forms
{
    public class MarkdownTextView : Label
    {
        public static BindableProperty MarkdownProperty = BindableProperty.Create("Markdown", typeof(string), typeof(MarkdownTextView));
        public string Markdown
        {
            get { return (string)GetValue(MarkdownProperty); }
            set { SetValue(MarkdownProperty, value); }
        }
    }
}
