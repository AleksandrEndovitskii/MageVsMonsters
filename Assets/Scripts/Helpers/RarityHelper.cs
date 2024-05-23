using System.Collections.Generic;
using System.Linq;
using MageVsMonsters.Models;
using UnityEngine;

namespace MageVsMonsters.Helpers
{
    public static class RarityHelper
    {
        private static Dictionary<Rarity, Color> _rarityColors = new Dictionary<Rarity, Color>
        {
            [Rarity.Trash] = Color.gray,
            [Rarity.Common] = Color.white,
            [Rarity.Uncommon] = Color.green,
            [Rarity.Rare] = Color.blue,
            [Rarity.Epic] = new Color(0.8f, 0.4f, 1f), // Purple
            [Rarity.Legendary] = Color.yellow,
            [Rarity.Mythical] = new Color(1f, 0.5f, 0.2f) // Orange
        };
        private static Dictionary<Rarity, float> _rarityDropChances = new Dictionary<Rarity, float>
        {
            [Rarity.Trash] = 100f,
            [Rarity.Common] = 50f,
            [Rarity.Uncommon] = 20f,
            [Rarity.Rare] = 10f,
            [Rarity.Epic] = 5f,
            [Rarity.Legendary] = 2f,
            [Rarity.Mythical] = 1f
        };

        static RarityHelper()
        {
            _rarityDropChances = _rarityDropChances.OrderBy(x =>
                x.Value).ToDictionary(x => x.Key, x => x.Value);
        }

        public static Rarity GetRarity(float dropChance)
        {
            foreach (var rarityDropChance in _rarityDropChances)
            {
                if (dropChance <= rarityDropChance.Value)
                {
                    return rarityDropChance.Key;
                }
            }

            return Rarity.Trash;
        }
    }
}
