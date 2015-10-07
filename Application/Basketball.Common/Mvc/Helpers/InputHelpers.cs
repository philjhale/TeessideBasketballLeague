using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;


namespace Basketball.Common.Mvc.Helpers
{
    /// <summary>
    /// InputExtensions
    /// 
    /// Include CheckBoxList taken from http://blogs.msdn.com/miah/archive/2008/11/10/checkboxlist-helper-for-mvc.aspx
    /// </summary>
    public static class InputExtensions
    {
        #region CheckBoxList
        public static string CheckBoxList(this HtmlHelper htmlHelper, string name, List<CheckBoxListInfo> listInfo)
        {
            return htmlHelper.CheckBoxList(name, listInfo,
                ((IDictionary<string, object>)null));
        }

        public static string CheckBoxList(this HtmlHelper htmlHelper, string name, List<CheckBoxListInfo> listInfo,
           object htmlAttributes)
        {
            return htmlHelper.CheckBoxList(name, listInfo,
                ((IDictionary<string, object>)new RouteValueDictionary(htmlAttributes)));
        }

        public static string CheckBoxList(this HtmlHelper htmlHelper, string name, List<CheckBoxListInfo> listInfo,
            IDictionary<string, object> htmlAttributes)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentException("The argument must have a value", "name");

            if (listInfo == null)
                throw new ArgumentNullException("listInfo");

            if (listInfo.Count < 1)
                throw new ArgumentException("The list must contain at least one value", "listInfo");

            StringBuilder sb = new StringBuilder();

            foreach (CheckBoxListInfo info in listInfo)
            {
                TagBuilder builder = new TagBuilder("input");

                if (info.IsChecked) builder.MergeAttribute("checked", "checked");
                builder.MergeAttributes<string, object>(htmlAttributes);
                builder.MergeAttribute("type", "checkbox");
                builder.MergeAttribute("value", info.Value);
                builder.MergeAttribute("name", name);
                builder.InnerHtml = info.DisplayText;
                sb.Append(builder.ToString(TagRenderMode.Normal));
                sb.Append("<br />");

            }

            return sb.ToString();

        }
        #endregion

        /// <summary>
        /// Outputs a drop down list populated with Yes/No display values and Y/N submit values
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="name"></param>
        /// <param name="selectedValue"></param>
        /// <param name="headerDisplayValue"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString DropDownListYesNo(this HtmlHelper htmlHelper,
            string name,
            string selectedValue,
            string headerDisplayValue,
            string headerSubmitValue,
            object htmlAttributes)
        {
            IList<SelectListItem> yesNoSelectList = new List<SelectListItem>();

            if (headerDisplayValue != null && headerSubmitValue != null)
            {
                yesNoSelectList.Add(
                    new SelectListItem()
                    {
                        Text = headerDisplayValue,
                        Value = headerSubmitValue,
                        Selected = selectedValue == headerSubmitValue ? true : false
                    }
                );
            }

            yesNoSelectList.Add(
                new SelectListItem()
                {
                    Text = "No",
                    Value = "N",
                    Selected = selectedValue == "N" ? true : false
                }
            );

            yesNoSelectList.Add(
                new SelectListItem()
                {
                    Text = "Yes",
                    Value = "Y",
                    Selected = selectedValue == "Y" ? true : false
                }
            );

            return htmlHelper.DropDownList(name, yesNoSelectList, htmlAttributes);
        }


        /// <summary>
        /// Outputs a drop down list populated with Yes/No display values and Y/N submit values
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="name"></param>
        /// <param name="selectedValue"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString DropDownListYesNo(this HtmlHelper htmlHelper, string name, string selectedValue, object htmlAttributes)
        {
            return htmlHelper.DropDownListYesNo(name, selectedValue, null, null, htmlAttributes);
        }

        /// <summary>
        /// Outputs a drop down list populated with Yes/No display values and Y/N submit values
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="name"></param>
        /// <param name="selectedValue"></param>
        /// <returns></returns>
        public static MvcHtmlString DropDownListYesNo(this HtmlHelper htmlHelper, string name, string selectedValue)
        {
            return htmlHelper.DropDownListYesNo(name, selectedValue, null, null, null);
        }
    }
}
