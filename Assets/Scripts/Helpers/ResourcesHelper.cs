using Newtonsoft.Json;
using UnityEngine;

namespace MageVsMonsters.Helpers
{
    public static class ResourcesHelper
    {
        public static T LoadFromJsonInResources<T>(string fileName)
        {
            var textAsset = Resources.Load<TextAsset>(fileName);

            if (textAsset == null)
            {
                Debug.LogError($"{nameof(ResourcesHelper)}.{ReflectionHelper.GetCallerMemberName()}" +
                                         $"\n{nameof(fileName)} == {fileName}" +
                                         $"\n{nameof(textAsset)} == {textAsset}");

                return default;
            }

            var content = GetContent<T>(textAsset);

            return content;
        }

        private static T GetContent<T>(TextAsset textAsset)
        {
            T content;
            var json = textAsset.text;
            if (typeof(T) == typeof(string))
            {
                content = (T)(object)json;
                return content;
            }
            content = JsonConvert.DeserializeObject<T>(json);

            Debug.Log($"{nameof(ResourcesHelper)}.{ReflectionHelper.GetCallerMemberName()}" +
                            $"\n{nameof(textAsset)}.{nameof(textAsset.name)} == {textAsset.name}" +
                            $"\n{nameof(content)} == {content}");

            return content;
        }
    }
}
