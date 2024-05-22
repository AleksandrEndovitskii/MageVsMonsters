using System;
using MageVsMonsters.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MageVsMonsters.JsonObjects
{
    [Serializable]
    public class CreatureDefinitionJsonObject
    {
        public int MaxHealth { get; set; }
        public int Damage { get; set; }
        public int Defense { get; set; }
        public float MovementSpeed { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public Rarity Rarity { get; set; }
    }
}
