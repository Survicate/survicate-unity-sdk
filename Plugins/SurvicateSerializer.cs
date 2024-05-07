using System.Text;
using System.Collections.Generic;

namespace Plugins.Survicate
{
    public class SurvicateSerializer
    {
        public static string serializeDictionary(Dictionary<string, string> dictionary)
        {
            if (dictionary.Count == 0)
            {
                return "";
            }

            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{");
            foreach (var kvp in dictionary)
            {
                jsonBuilder.Append($"\"{kvp.Key}\":\"{kvp.Value}\",");
            }
            jsonBuilder.Length--;
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
    }
}
