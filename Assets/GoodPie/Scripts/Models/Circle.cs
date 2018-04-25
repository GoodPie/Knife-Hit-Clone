using System;
using GoodPie.Scripts.Circle;
using UnityEngine;

namespace GoodPie.Scripts.Models
{
	[Serializable]
	public class Circle 
	{
		public string Name = "Default";
		public string SpriteLocation = "Cicles/Default";
		public float MaxRotationSpeed = 250;
		public int Diffuculty = 1;
		
		public bool IsBoss = false;
		public int BossKnifeDrop = -1;
		public string BossSpecialComponent;
		
		[NonSerialized]
		public GameObject DefaultCircle;

		public Circle()
		{
		}

		public Circle(string name, string sprite, float maxRotationSpeed, int diffuculty)
		{
			Name = name;
			SpriteLocation = sprite;
			MaxRotationSpeed = maxRotationSpeed;
			Diffuculty = diffuculty;
			IsBoss = false;
			BossKnifeDrop = -1;
			BossSpecialComponent = null;
		}
		
		public Circle(string name, string sprite, float maxRotationSpeed, int difficulty, int knifeDrop, string bossSpecial)
		{
			Name = name;
			SpriteLocation = sprite;
			MaxRotationSpeed = maxRotationSpeed;
			Diffuculty = difficulty;
			IsBoss = true;
			BossKnifeDrop = knifeDrop;
			BossSpecialComponent = bossSpecial;
		}


		public void Initialize(GameObject circle)
		{
			DefaultCircle = circle;
			var sprite = Resources.Load<Sprite>(SpriteLocation);
			circle.GetComponent<SpriteRenderer>().sprite = sprite;
			circle.GetComponent<CircleRotation>().MaxRotationSpeed = MaxRotationSpeed;
		}

		public int Drop()
		{
			return BossKnifeDrop;
		}
		
	}
}
