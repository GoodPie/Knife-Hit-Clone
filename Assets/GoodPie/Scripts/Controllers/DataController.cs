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
		public Models.Circle[] Circles;
		public Knife[] Knives;

		private void Start()
		{
			DontDestroyOnLoad(gameObject);

			// Load all the data required
			Circles = ReadDataToCollection<CircleCollection>(CircleDataFile).Circles;
			Knives = ReadDataToCollection<KnifeCollection>(KnifeDataFile).Knives;
		}

		public Models.Circle PickRandomCircle(int difficulty)
		{
			List<Models.Circle> circlesWithDifficulty = new List<Models.Circle>();
			foreach (var circle in Circles)
			{
				if (circle.Diffuculty == difficulty)
				{
					circlesWithDifficulty.Add(circle);
				}
			}

			if (circlesWithDifficulty.Count == 0 && difficulty > 0)
			{
				PickRandomCircle(difficulty - 1);
			}

			if (difficulty - 1 == 0)
			{
				throw new InvalidDataException("Failed to find any circles with any difficulty");
			}

			int chosenIndex = Random.Range(0, circlesWithDifficulty.Count - 1);
			return circlesWithDifficulty[chosenIndex];
		}

		public Models.Circle FindCircleByName(string name)
		{
			Models.Circle foundCircle = null;
			foreach (var circle in Circles)
			{
				if (circle.Name.Equals(name))
				{
					foundCircle = circle;
					break;
				}
			}

			return foundCircle;
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
		/// Generic function to handle reading in data from JSON (in array format) into a collection which in this case
		/// is just a wrapper for an array
		/// </summary>
		/// <param name="dataPath">Data path, within the streaming assets directory</param>
		/// <typeparam name="T">Collection type</typeparam>
		/// <returns>Collection type</returns>
		private static T ReadDataToCollection<T>(string dataPath)
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
