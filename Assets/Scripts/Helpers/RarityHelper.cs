using System.Collections.Generic;
using System.Linq;
using MageVsMonsters.Models;

namespace MageVsMonsters.Helpers
{
    public static class RarityHelper
    {
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
