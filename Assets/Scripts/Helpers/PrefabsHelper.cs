using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MageVsMonsters.Helpers
{
    public static class PrefabsHelper
    {
        public static T LoadPrefab<T>(string prefabPath)
            where T : Object
        {
            var prefab = Resources.Load<T>(prefabPath);

            return prefab;
        }
        public static List<T> LoadPrefabs<T>(string prefabsPath)
            where T : Object
        {
            var prefabs = Resources.LoadAll<T>(prefabsPath).ToList();

            return prefabs;
        }
        public static Dictionary<string,T> LoadPrefabsAsDictionary<T>(string prefabsPath)
            where T : Object
        {
            var prefabs = Resources.LoadAll<T>(prefabsPath).ToDictionary(x => x.name);

            return prefabs;
        }
    }
}
