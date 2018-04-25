using System;
using UnityEngine;

namespace GoodPie.Scripts.Models
{
    [Serializable]
    public class Knife
    {
        public int ID;
        public string Name;
        public string SpriteLocation;
        public int Rarity;

        private GameObject _defaultKnife;
    
        public Knife() {}

        public Knife(int id, string name, string sprite, int rarity)
        {
            ID = id;
            Name = name;
            SpriteLocation = sprite;
            Rarity = rarity;
        }

        public void Initialize(GameObject knife)
        {
            _defaultKnife = knife;
            var sprite = Resources.Load(SpriteLocation) as Sprite;
            knife.GetComponent<SpriteRenderer>().sprite = sprite;
        }
    
    }
}
