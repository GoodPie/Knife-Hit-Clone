using System;

namespace GoodPie.Scripts.Models
{
    [Serializable]
    public class Level
    {
        private static readonly int DefaultKnives = 6;
        private static readonly int DefaultStages = 4;
        private static readonly string DefaultBoss = "";

        public int MaxKnives;
        public int Stages;
        public string Boss;

        public Level(int maxKnives, int stages, string boss)
        {
            MaxKnives = maxKnives;
            Stages = stages;
            Boss = boss;
        }

        private static Level GenerateLevel(int difficulty, string boss)
        {
            var knives = DefaultKnives + (difficulty % 5);
            var stages = DefaultStages + (difficulty % 10);
            return new Level(knives, stages, boss);
        }

    }
}