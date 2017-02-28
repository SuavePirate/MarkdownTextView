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
using Android.Text.Style;
using Android.Text;

namespace SPControls
{
    /// <summary>
    /// Custom tag handler for parsing more html tags for android textviews
    /// This is a port/translation of https://github.com/sufficientlysecure/html-textview/blob/master/HtmlTextView/src/main/java/org/sufficientlysecure/htmltextview/HtmlTagHandler.java
    /// Which is considered the most robust handler
    /// </summary>
    public class HtmlTagHandler : Java.Lang.Object, Android.Text.Html.ITagHandler
    {
        /**
         * Keeps track of lists (ol, ul). On bottom of Stack is the outermost list
         * and on top of Stack is the most nested list
         */
        Stack<String> lists = new Stack<String>();
        /**
         * Tracks indexes of ordered lists so that after a nested list ends
         * we can continue with correct index of outer list
         */
        Stack<Int32> olNextIndex = new Stack<Int32>();
        /**
         * List indentation in pixels. Nested lists use multiple of this.
         */
        private static int indent = 10;
        private static int listItemIndent = indent * 2;
        private static BulletSpan bullet = new BulletSpan(indent);

        private class Ul : Java.Lang.Object
        {
        }

        private class Ol : Java.Lang.Object
        {
        }

        private class Code : Java.Lang.Object
        {
        }

        private class Center : Java.Lang.Object
        {
        }

        private class Strike : Java.Lang.Object
        {
        }


        public void HandleTag(Boolean opening, String tag, Android.Text.IEditable output, IXMLReader xmlReader)
        {
            if (opening)
            {
                // opening tag
                //          if (HtmlTextView.DEBUG) {
                //              Log.d(HtmlTextView.TAG, "opening, output: " + output.ToString());
                //          }
                //
                if (tag.ToLower() == "ul")
                {
                    lists.Push(tag);
                }
                else if (tag.EqualsIgnoreCase("ol"))
                {
                    lists.Push(tag);
                    olNextIndex.Push(1);
                }
                else if (tag.EqualsIgnoreCase("li"))
                {
                    if (output.Length() > 0 && output.CharAt(output.Length() - 1) != '\n')
                    {
                        output.Append("\n");
                    }
                    String parentList = lists.Peek();
                    if (parentList.EqualsIgnoreCase("ol"))
                    {
                        Start(output, new Ol());
                        output.Append(olNextIndex.Peek().ToString()).Append('.').Append(' ');
                        olNextIndex.Push(olNextIndex.Pop() + 1);
                    }
                    else if (parentList.EqualsIgnoreCase("ul"))
                    {
                        Start(output, new Ul());
                    }
                }
                else if (tag.EqualsIgnoreCase("code"))
                {
                    Start(output, new Code());
                }
                else if (tag.EqualsIgnoreCase("center"))
                {
                    Start(output, new Center());
                }
                else if (tag.EqualsIgnoreCase("s") || tag.EqualsIgnoreCase("strike"))
                {
                    Start(output, new Strike());
                }
            }
            else
            {
                // closing tag
                //          if (HtmlTextView.DEBUG) {
                //              Log.d(HtmlTextView.TAG, "closing, output: " + output.ToString());
                //          }
                //
                if (tag.EqualsIgnoreCase("ul"))
                {
                    lists.Pop();
                }
                else if (tag.EqualsIgnoreCase("ol"))
                {
                    lists.Pop();
                    olNextIndex.Pop();
                }
                else if (tag.EqualsIgnoreCase("li"))
                {
                    if (lists.Peek().EqualsIgnoreCase("ul"))
                    {
                        if (output.Length() > 0 && output.CharAt(output.Length() - 1) != '\n')
                        {
                            output.Append("\n");
                        }
                        // Nested BulletSpans increases distance between bullet and Text, so we must prevent it.
                        int bulletMargin = indent;
                        if (lists.Count > 1)
                        {
                            bulletMargin = indent - bullet.GetLeadingMargin(true);
                            if (lists.Count > 2)
                            {
                                // This get's more complicated when we add a LeadingMarginSpan into the same line:
                                // we have also counter it's effect to BulletSpan
                                bulletMargin -= (lists.Count - 2) * listItemIndent;
                            }
                        }
                        BulletSpan newBullet = new BulletSpan(bulletMargin);
                        End(output, typeof(Ul), false,
                            new LeadingMarginSpanStandard(listItemIndent * (lists.Count - 1)),
                            newBullet);
                    }
                    else if (lists.Peek().EqualsIgnoreCase("ol"))
                    {
                        if (output.Length() > 0 && output.CharAt(output.Length() - 1) != '\n')
                        {
                            output.Append("\n");
                        }
                        int numberMargin = listItemIndent * (lists.Count - 1);
                        if (lists.Count > 2)
                        {
                            // Same as in ordered lists: counter the effect of nested Spans
                            numberMargin -= (lists.Count - 2) * listItemIndent;
                        }
                        End(output, typeof(Ol), false, new LeadingMarginSpanStandard(numberMargin));
                    }
                }
                else if (tag.EqualsIgnoreCase("code"))
                {
                    End(output, typeof(Code), false, new TypefaceSpan("monospace"));
                }
                else if (tag.EqualsIgnoreCase("center"))
                {
                    End(output, typeof(Center), true, new AlignmentSpanStandard(Layout.Alignment.AlignCenter));
                }
                else if (tag.EqualsIgnoreCase("s") || tag.EqualsIgnoreCase("strike"))
                {
                    End(output, typeof(Strike), false, new StrikethroughSpan());
                }
            }
        }

        /**
         * Mark the opening tag by using private classes
         */
        private void Start(IEditable output, Java.Lang.Object mark)
        {
            int len = output.Length();
            output.SetSpan(mark, len, len, SpanTypes.MarkMark);

            //      if (HtmlTextView.DEBUG) {
            //          Log.d(HtmlTextView.TAG, "len: " + len);
            //      }
        }

        /**
         * Modified from {@link Android.Text.Html}
         */
        private void End(IEditable output, Type kind, Boolean paragraphStyle, params Java.Lang.Object[] replaces)
        {
            Java.Lang.Object obj = GetLast(output, kind);
            // start of the tag
            int where = output.GetSpanStart(obj);
            // end of the tag
            int len = output.Length();

            output.RemoveSpan(obj);

            if (where != len)
            {
                int thisLen = len;
                // paragraph styles like AlignmentSpan need to end with a new line!
                if (paragraphStyle)
                {
                    output.Append("\n");
                    thisLen++;
                }
                foreach (Java.Lang.Object replace in replaces)
                {
                    output.SetSpan(replace, where, thisLen, SpanTypes.ExclusiveExclusive);
                }

                //          if (HtmlTextView.DEBUG) {
                //              Log.d(HtmlTextView.TAG, "where: " + where);
                //              Log.d(HtmlTextView.TAG, "thisLen: " + thisLen);
                //          }
            }
        }

        /**
         * Get last marked position of a specific tag kind (private class)
         */
        private static Java.Lang.Object GetLast(IEditable Text, Type kind)
        {
            Java.Lang.Object[] objs = Text.GetSpans(0, Text.Length(), Java.Lang.Class.FromType(kind)); // TODO: LOl will this work?
            if (objs.Length == 0)
            {
                return null;
            }
            else
            {
                for (int i = objs.Length; i > 0; i--)
                {
                    if (Text.GetSpanFlags(objs[i - 1]) == SpanTypes.MarkMark)
                    {
                        return objs[i - 1];
                    }
                }
                return null;
            }
        }

    }
}