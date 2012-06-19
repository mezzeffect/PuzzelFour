using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace PuzzleFourGlossary.Web.Helper
{
    public static class MvcHelper
    {

        public static string GetErrorMessage(this ModelStateDictionary modelState, string key)
        {
            var state = modelState[key];
            if (state == null) return string.Empty;

            var firstError = state.Errors.FirstOrDefault();
            return firstError == null ? string.Empty : firstError.ErrorMessage;
        }

        public static List<string> GetErros(this ModelStateDictionary modelState)
        {
            return (from vModelState in modelState.Values from error in vModelState.Errors select error.ErrorMessage).ToList();
        }

        public static string EncodeJson(object o)
        {
            var ser = new Newtonsoft.Json.JsonSerializer();
            using (var tw = new StringWriter())
            {

                ser.Serialize(tw, o);
                var encodeJson = tw.ToString();
                var cleanedString = encodeJson
                    .Replace("\\r\\n", " ")
                    .Replace("\r\n", " ")
                    .Replace("\\r", " ")
                    .Replace("\r", " ")
                    .Replace("\\n", " ")
                    .Replace("\n", " ")
                    .Replace("\\", "");
                    //this should skip it in the json parsing
                foreach (var match in Regex.Matches(cleanedString, @"<[\w\s\.]*@[\w\s\.]*>"))
                {
                    cleanedString = cleanedString.Replace(match.ToString(),match.ToString().Replace("<","<;"));
                }

                // removed inner quotes
                foreach (Match match in Regex.Matches(cleanedString, ":\"(.*?)\"(?>(?>,\")|})"))
                {

                    if (match.Groups[1].ToString().Contains('"'))
                    {
                        var newVal = match.Groups[1].ToString().Replace("\"", " ");
                        var oldVal = match.Groups[1].ToString();
                        var replace = match.ToString();
                        var replaceNew = replace.Replace(oldVal, newVal);
                        cleanedString = cleanedString.Replace(replace, replaceNew);
                    }
                }
                return cleanedString;
            }
        }

        public static string JavaScriptStringEncode(object o)
        {
            var stringObject = EncodeJson(o);
            return JavaScriptStringEncode(stringObject);
        }

        public static string JavaScriptStringEncode(string message)
        {
            if (String.IsNullOrEmpty(message))
            {
                return message;
            }

            StringBuilder builder = new StringBuilder();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.Serialize(message, builder);
            return builder.ToString(1, builder.Length - 2); // remove first + last quote
        }

        public static T To<T>(this IConvertible obj)
        {
            Type t = typeof(T);
            Type u = Nullable.GetUnderlyingType(t);

            if (u != null)
            {
                if (obj == null)
                    return default(T);

                return (T)Convert.ChangeType(obj, u);
            }
            return (T)Convert.ChangeType(obj, t);
        }
    }
 }