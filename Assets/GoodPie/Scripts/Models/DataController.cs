using System;
using System.IO;
using System.Net;
using UnityEngine;

namespace GoodPie.Scripts.Models
{
	public class DataController : MonoBehaviour
	{

		[Serializable]
		public class CircleCollection
		{
			public Circle[] Circles;
		}
		
		private static readonly string CircleDataFile = "/data/circles.json";
		public Circle[] Circles;

		private void Start()
		{
			DontDestroyOnLoad(gameObject);

			Circles = ReadDataToCollection<CircleCollection>(CircleDataFile).Circles;
			
		}

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
		
	
	}
}
