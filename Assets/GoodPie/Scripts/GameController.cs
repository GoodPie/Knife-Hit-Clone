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
		
		public int CurrentStage = 0;

		public GameObject KnifeInUse;

	}
}
