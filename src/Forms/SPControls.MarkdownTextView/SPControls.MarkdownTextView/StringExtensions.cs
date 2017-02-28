using SPControls.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SPControls
{
    public static class StringExtensions
    {
        #region MARKDOWN STYLES
        private const string ORIGINAL_PATTERN_BEGIN = "<code>";
        private const string ORIGINAL_PATTERN_END = "</code>";
        private const string PARSED_PATTERN_BEGIN = "<font color=\"#888888\" face=\"monospace\"><tt>";
        private const string PARSED_PATTERN_END = "</tt></font>";
        public const string MARKDOWN_STYLES_DARK = @"<style>
p,h1,h2,h3,h4,h5,h6,blockquote,pre{
	font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif;
	font-weight: 300;
    color: #212121;
	font-style: normal;
	font-variant: normal;
    padding:0px;
    margin:0px;
line-height:0;
}
h1 {
	font-size: 2em;
}
h2 {
    font-size: 1.5em;
}
h3 {
	font-size: 1.17em;
}
h4{
    font-size: 14px;
}
h5{
    font-size: 0.83em;
}
h6{
    font-size: 0.67em;
}
p {
	font-size: 16px;
}
blockquote {
    color: #9e9e9e;
    border-left: 2px solid #e0e0e0;
    padding-left: 16px;
}
pre {
	font-size: 13px;
},
code{
    background-color: #e0e0e0;
    overflow-x: scroll;
}
</style>";
        public const string MARKDOWN_STYLES_LIGHT = @"<style>
p,h1,h2,h3,h4,h5,h6,blockquote,pre{
	font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif;
	font-weight: 300;
    color: #FFFFFF;
	font-style: normal;
	font-variant: normal;
    padding:0px;
    margin:0px;
line-height:0;
}
h1 {
	font-size: 2em;
}
h2 {
    font-size: 1.5em;
}
h3 {
	font-size: 1.17em;
}
h4{
    font-size: 14px;
}
h5{
    font-size: 0.83em;
}
h6{
    font-size: 0.67em;
}
p {
	font-size: 16px;
}
blockquote {
    color: #EEEEEE;
    border-left: 2px solid #e0e0e0;
    padding-left: 16px;
}
pre {
	font-size: 13px;
},
code{
    background-color: #e0e0e0;
    overflow-x: scroll;
    color: #212121;
}
</style>";


#endregion

        public static string GetHtmlFromMarkdown(this string markdownText, bool isLight = false)
        {
            var markdownOptions = new MarkdownOptions
            {
                AutoHyperlink = true,
                AutoNewlines = false,
                EncodeProblemUrlCharacters = false,
                LinkEmails = true,
                StrictBoldItalic = true
            };
            var markdown = new Markdown(markdownOptions);
            var htmlContent = markdown.Transform(markdownText);
            var regex = new Regex("\n");
            htmlContent = regex.Replace(htmlContent, "<br/>");

            //if (isLight)
            //{
            //    htmlContent = MARKDOWN_STYLES_LIGHT + htmlContent;
            //}
            //else
            //{
            //    htmlContent = MARKDOWN_STYLES_DARK + htmlContent;
            //}

            var html = string.Format("<html><body>{0}</body></html>", htmlContent);
            var regex2 = new Regex("\r");
            html = regex.Replace(html, string.Empty);
            html = regex2.Replace(html, string.Empty);
            return html;
        }

        /// <summary>
        /// Wrap html with a full html tag
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string HtmlWrapped(this string html)
        {
            if (!html.StartsWith("<html>") || !html.EndsWith("</html>"))
            {
                html = $"<html><body>{html}</body></html>";
            }
            return html;
        }

        /// <summary>
        /// Parses html with code or pre tags and gives them proper
        /// styled spans so that Android can parse it properly
        /// </summary>
        /// <param name="htmlText">The html string</param>
        /// <returns>The html string with parsed code tags</returns>
        public static string ParseCodeTags(this string htmlText)
        {
            if (htmlText.IndexOf(ORIGINAL_PATTERN_BEGIN) < 0) return htmlText;
            var regex = new Regex(ORIGINAL_PATTERN_BEGIN);
            var regex2 = new Regex(ORIGINAL_PATTERN_END);

            htmlText = regex.Replace(htmlText, PARSED_PATTERN_BEGIN);
            htmlText = regex2.Replace(htmlText, PARSED_PATTERN_END);
            htmlText = htmlText.TrimLines();
            return htmlText;
        }

        public static string StripTags(this string html, bool stripBreaks)
        {
            if (stripBreaks)
            {
                html = html.ReplaceBreaksWithSpace();
            }
            else
            {
                html = html.ReplaceBreaks();
            }
            Regex regHtml = new Regex("<[^>]*>");
            var strippedString = regHtml.Replace(html, "");
            strippedString = strippedString.TrimLines();
            return strippedString;
        }
        public static bool EqualsIgnoreCase(this string text, string text2)
        {
            return text.Equals(text2, StringComparison.CurrentCultureIgnoreCase);
        }

        public static string ReplaceBreaks(this string html)
        {
            var regex = new Regex("<br/>");
            html = regex.Replace(html, "\n");
            return html;
        }

        public static string ReplaceBreaksWithSpace(this string html)
        {
            var regex = new Regex("<br/>");
            html = regex.Replace(html, " ");
            return html;
        }

        public static string TrimLines(this string originalString)
        {
            originalString = originalString.Trim('\n');
            return originalString;
        }
    }
}
