using System;

namespace GoodPie.Scripts.Models
{
    [Serializable]
    public class PlayerSave
    {
        public int[] UnlockedKnives;
        public int CurrentlyUsedKnife;
        public int HighScore;
        public int BossesDefeated;
        public int HighestLevel;

        public PlayerSave()
        {
            UnlockedKnives = new[] {0};
            CurrentlyUsedKnife = UnlockedKnives[0];
            HighScore = 0;
            BossesDefeated = 0;
            HighestLevel = 0;
        }

        public PlayerSave(int[] unlockedKnives, int currentlyUsedKnife, int highScore, int bossesDefeated, int highestLevel)
        {
            UnlockedKnives = unlockedKnives;
            CurrentlyUsedKnife = currentlyUsedKnife;
            HighScore = highScore;
            BossesDefeated = bossesDefeated;
            HighestLevel = highestLevel;
        }

    }
}