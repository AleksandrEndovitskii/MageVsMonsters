using System.Collections.Generic;
using Newtonsoft.Json;

namespace MageVsMonsters.Helpers
{
    public static class DefinitionsHelper
    {
        public static List<T> GetDefinitions<T>(string definitionsFilePath)
        {
            var definitionsJson = ResourcesHelper.LoadFromJsonInResources<string>(definitionsFilePath);
            var definitionJsonObjects = JsonConvert.DeserializeObject<List<T>>(definitionsJson);

            return definitionJsonObjects;
        }
    }
}
