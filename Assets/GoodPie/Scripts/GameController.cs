using GoodPie.Scripts.Models;
using UnityEngine;
using UnityEngine.UI;

namespace GoodPie.Scripts
{
	public class GameController : MonoBehaviour
	{
		[Tooltip("The max amount of knives the player can have")]
		public int MaxKnives = 10;	// Allow for changes in the future
		
		[Tooltip("The amount of knives the player current has left")]
		public int CurrentKnives = 10;

		[Tooltip("The players current score")]
		public int CurrentScore = 0;
		
		[Tooltip("The current level the player is on")]
		public int CurrentStage = 0;

		[Tooltip("The currently used knife. May change for cosmetic purposes")]
		public GameObject KnifeInUse;

		[Tooltip("Manages the data required for the game")]
		public DataController DataController;



	}
}
