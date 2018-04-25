using System;
using System.Collections.Generic;

namespace GoodPie.Scripts.Models
{
    public static class Rarity
    {
        // Keep an array of rarities (was going to use dictionary with int key but kind of pointless)
        private static readonly string[] Rarities = {"Common", "Uncommon", "Rare", "Very Rare", "Legendary"};

        public static string GetRarityDesc(int rarity)
        {
            if (rarity > Rarities.Length - 1 || rarity < 0)
            {
                throw new InvalidRarityException("Rarity must be between 0 and " + (Rarities.Length - 1).ToString());
            }
            
            return Rarities[rarity];
        }

        public class InvalidRarityException : Exception
        {
            public InvalidRarityException(string message)
                :base(message) {}
        }
        
    }
}