using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using GoodPie.Scripts.Models;
using Random = UnityEngine.Random;

namespace GoodPie.Scripts.Controllers
{
	public class DataController : MonoBehaviour
	{
		
		/// Note:
		/// Collections (in this case, wrapper around arrays) were used so that we can just use the standard JSONUtility
		/// tool included with Unity to read in arrays from JSON. Annoying to handle it otherwise.

		[Serializable]
		private class CircleCollection
		{
			public Models.Circle[] Circles;
		}


		[Serializable]
		private class KnifeCollection
		{
			public Knife[] Knives;
		}

		// The main types of data we are going to load for a our game is the circles and the knives
		private static readonly string CircleDataFile = "/data/circles.json";
		private static readonly string KnifeDataFile = "/data/knives.json";
		private static readonly string PlayerSaveFile = "/data/save.json";
		public List<Models.Circle> StandardCircles = new List<Models.Circle>();
		public List<Models.Circle> BossCircles = new List<Models.Circle>();
		public Knife[] Knives;
		public PlayerSave PlayerSave;

		private void Start()
		{
			DontDestroyOnLoad(gameObject);

			// Load all the data required
			var allCircles = ReadJSONFromFile<CircleCollection>(CircleDataFile).Circles;
			SeperateBossAndStandardCircles(allCircles);
			Knives = ReadJSONFromFile<KnifeCollection>(KnifeDataFile).Knives;
			PlayerSave = ReadJSONFromFile<PlayerSave>(PlayerSaveFile);
			if (PlayerSave == null)
			{
				PlayerSave = new PlayerSave();
			}
		}

		private void SeperateBossAndStandardCircles(Models.Circle[] allCircles)
		{
			foreach (Models.Circle circle in allCircles)
			{
				if (circle.IsBoss)
				{
					BossCircles.Add(circle);
				}
				else
				{
					StandardCircles.Add(circle);
				}
			}
		}

		public Models.Circle PickRandomStandardCircle(int difficulty)
		{
			List<Models.Circle> circlesWithDifficulty = new List<Models.Circle>();
			foreach (var circle in StandardCircles)
			{
				if (circle.Diffuculty == difficulty)
				{
					circlesWithDifficulty.Add(circle);
				}
			}

			if (circlesWithDifficulty.Count == 0 && difficulty > 0)
			{
				PickRandomStandardCircle(difficulty - 1);
			}

			if (circlesWithDifficulty.Count == 0 && difficulty - 1 == 0)
			{
				throw new InvalidDataException("Failed to find any circles with any difficulty");
			}

			int chosenIndex = Random.Range(0, circlesWithDifficulty.Count);
			return circlesWithDifficulty[chosenIndex];
		}

		public Models.Circle GetStandardCircleByName(string name)
		{
			Models.Circle foundCircle = null;
			foreach (var circle in StandardCircles)
			{
				if (circle.Name.Equals(name) && circle.IsBoss == false) 
				{
					foundCircle = circle;
					break;
				}
			}

			return foundCircle;
		}

		public Models.Circle PickRandomBossCircle()
		{
			var choice = Random.Range(0, BossCircles.Count);
			return BossCircles[choice];
		}

		public Knife FindKnifeByName(string name)
		{
			Knife foundKnife = null;
			foreach (var knife in Knives)
			{
				if (knife.Name.Equals(name))
				{
					foundKnife = knife;
					break;
				}
			}

			return foundKnife;
		}
		
		public Knife FindKnifeByID(int id)
		{
			Knife foundKnife = null;
			foreach (var knife in Knives)
			{
				if (knife.ID == id)
				{
					foundKnife = knife;
					break;
				}
			}

			return foundKnife;
		}

		/// <summary>
		/// Reads JSON to new object from a readonly location (streaming assets)
		/// </summary>
		/// <param name="dataPath"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		private static T ReadJSONFromFile<T>(string dataPath)
		{
			var actualDataPath = Application.streamingAssetsPath + dataPath;
			if (File.Exists(actualDataPath))
			{
				string dataAsText = File.ReadAllText(actualDataPath);
				var collection = JsonUtility.FromJson<T>(dataAsText);
				return collection;
			}

			return default(T);
		}

		public class InvalidDataException : Exception
		{
			public InvalidDataException()
				: base() {}
			
			public InvalidDataException(string message)
				: base(message) {}
		}
	
	}
}
