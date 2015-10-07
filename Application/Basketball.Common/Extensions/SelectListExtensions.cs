using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Basketball.Common.Extensions
{
    public static class SelectListExtensions
    {
        /// <summary>
        /// Converts a List<T> into a List<SelectListItem>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="text"></param>
        /// <param name="value"></param>
        /// <param name="defaultOption"></param>
        /// <param name="defaultValue"></param>
        /// <returns>List<SelectListItem></returns>
        public static List<SelectListItem> ToSelectList<T>(
            this IEnumerable<T> enumerable,
            Func<T, string> text,
            Func<T, string> value,
            string selectedValue)
        {
            var items = enumerable.Select(f => new SelectListItem()
            {
                Text = text(f),
                Value = value(f),
                Selected = (selectedValue != null && selectedValue == value(f)) ? true : false
            }).ToList();

            return items;
        }

        public static List<SelectListItem> ToSelectListWithHeader<T>(
            this IEnumerable<T> enumerable,
            Func<T, string> text,
            Func<T, string> value,
            string selectedValue,
            string defaultOption,
            string defaultValue)
        {
            var items = enumerable.Select(f => new SelectListItem()
            {
                Text = text(f),
                Value = value(f),
                Selected = (selectedValue != null && selectedValue == value(f)) ? true : false
            }).ToList();

            items = items.OrderBy(i => i.Text).ToList();

            items.Insert(0, new SelectListItem()
            {
                Text = defaultOption,
                Value = defaultValue
            });

            return items; 
        }

        /// <summary>
        /// Converts a List<T> into a List<SelectListItem>. Default value is set to -1
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="text"></param>
        /// <param name="value"></param>
        /// <param name="defaultOption"></param>
        /// <returns></returns>
        public static List<SelectListItem> ToSelectListWithHeader<T>(
            this IEnumerable<T> enumerable,
            Func<T, string> text,
            Func<T, string> value,
            string selectedValue,
            string defaultOption)
        {
            return ToSelectListWithHeader(enumerable, text, value, selectedValue, defaultOption, "");
        }

        /// <summary>
        /// Converts a List<T> into a List<SelectListItem> with the 
        /// first element as "Please select..."
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="text"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static List<SelectListItem> ToSelectListWithHeader<T>(
            this IEnumerable<T> enumerable,
            Func<T, string> text,
            Func<T, string> value,
            string selectedValue)
        {
            return ToSelectListWithHeader(enumerable, text, value, selectedValue, "Please select...");
        }


    }
}
