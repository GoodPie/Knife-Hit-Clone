﻿using System;

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
        public Circle Boss;

        public Level(int maxKnives, int stages, Circle boss)
        {
            MaxKnives = maxKnives;
            Stages = stages;
            Boss = boss;
        }

        public static Level GenerateLevel(int difficulty, Circle boss)
        {
            int knives = DefaultKnives + (difficulty / 5);
            int stages = DefaultStages + (difficulty / 10);
            return new Level(knives, stages, boss);
        }

    }
}