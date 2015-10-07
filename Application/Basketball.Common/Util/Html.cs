using System;
using System.Text;
using HtmlAgilityPack;

namespace Basketball.Common.Util
{
    public class Html
    {
        /// <summary>
        /// Replaces break tags with newlines and paragraph tags with a newline above and below
        /// </summary>
        public static string ConvertToPlainText(string html)
        {
            if (html == null || html.Trim().Length == 0)
                return null;

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);

            StringBuilder content = new StringBuilder();
            foreach (var node in doc.DocumentNode.DescendantNodesAndSelf())
            {
                if (node.Name == "br")
                    content.Append(Environment.NewLine);

                if (!node.HasChildNodes)
                {
                    if(node.ParentNode.Name == "p")   
                        content.Append(Environment.NewLine + node.InnerText + Environment.NewLine);
                    else
                        content.Append(node.InnerText);
                }
            }
            return HtmlEntity.DeEntitize(content.ToString().Replace("&nbsp;", " ")); 
        }
    }
}
