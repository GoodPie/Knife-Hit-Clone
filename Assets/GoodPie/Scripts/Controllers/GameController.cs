using GoodPie.Scripts.Models;
using UnityEngine;

namespace GoodPie.Scripts.Controllers
{
	public class GameController : MonoBehaviour
	{
		[Tooltip("The max amount of knives the player can have")]
		public int MaxKnives = 10;	// Allow for changes in the future
		
		[Tooltip("The amount of knives the player current has left")]
		public int CurrentKnives = 10;

		[Tooltip("The players current score")]
		public int CurrentScore = 0;

		public int CurrentLevel = 1;
		
		[Tooltip("The current stage (sub level) the player is on")]
		public int CurrentStage = 1;

		[Tooltip("The currently used knife. May change for cosmetic purposes")]
		public GameObject KnifeInUse;

		[Tooltip("Manages the data required for the game")]
		public DataController DataController;

		public Level LevelDetails;

		public GameObject BaseCircle;
		public Models.Circle CircleDetails;
		public GameObject CurrentCircle;

		private void Start()
		{
			RestartGame();
		}

		private void Update()
		{
			if (CurrentKnives <= 0)
			{
				CompletedStage();
			}
		}

		public void RestartGame()
		{
			Destroy(CurrentCircle);

			CurrentStage = 1;
			CurrentLevel = 0;
			
			// Generate a new level
			LevelDetails = Level.GenerateLevel(0, DataController.PickRandomBossCircle());
			MaxKnives = LevelDetails.MaxKnives;
			CurrentKnives = LevelDetails.MaxKnives;
			ChooseNewCircle();
		}

		private void SetupNewLevel()
		{
			Destroy(CurrentCircle);
			
			LevelDetails = Level.GenerateLevel(GetDifficulty(), DataController.PickRandomBossCircle());
			MaxKnives = LevelDetails.MaxKnives;
			CurrentKnives = LevelDetails.MaxKnives;
			ChooseNewCircle();
		}

		private int GetDifficulty()
		{
			int difficulty = (CurrentStage / 5) + 1;
			return difficulty;
		}

		public void ChooseNewCircle()
		{
			int difficulty = (CurrentStage / 5) + 1;
			CircleDetails = DataController.PickRandomStandardCircle(difficulty);
			CircleDetails.Initialize(BaseCircle);
			CurrentCircle = Instantiate(CircleDetails.DefaultCircle);
		}

		public void LaunchedKnife()
		{
			CurrentKnives -= 1;
		}

		public bool HasKnives()
		{
			return CurrentKnives > 0;
		}
		

		public void CompletedStage()
		{
			CurrentStage += 1;
			if (CurrentStage > LevelDetails.Stages)
			{
				CurrentLevel += 1;
				CurrentStage = 0;
				SetupNewLevel();
			}

			if (CurrentStage >= LevelDetails.Stages - 1)
			{
				// TODO: Intiialize boss
			}

			CurrentKnives = LevelDetails.MaxKnives;
			Destroy(CurrentCircle);
			ChooseNewCircle();
		}

		public void HitCollidable()
		{
			RestartGame();
		}
	}
}
