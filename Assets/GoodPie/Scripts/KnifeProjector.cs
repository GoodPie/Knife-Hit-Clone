using GoodPie.Scripts.Controllers;
using UnityEngine;

namespace GoodPie.Scripts
{
	public class KnifeProjector : MonoBehaviour
	{

		[Tooltip("Time before the knives respawn")]
		public const float RespawnTimeout = 0.1f;
		private float _respawnTimer = 0.0f;
	
		// Flag that initiates the timer
		private bool _knifeThrown = false;
		private bool _canThrowKnife = true;
	
		// Currently spawned knife
		public GameObject CurrentKnife;

		public GameController GameController;

		private void Start()
		{
			SpawnKnife();
		}

		private void SpawnKnife()
		{
			CurrentKnife = Instantiate(GameController.KnifeInUse);
			CurrentKnife.transform.position = transform.position;
			CurrentKnife.transform.parent = transform;
		}

		public void KnifeLanded(bool landedOnCircle)
		{
			
			if (landedOnCircle)
			{
				// Let the game controller know we have used a knife
				GameController.LaunchedKnife();
			}
			else
			{
				GameController.RestartGame();
			}
		}

		private void Update()
		{
			if (GameController.HasKnives() == false) 
				return;
			
			var triggerDown = GetTriggerDown();

			if (triggerDown)
			{
				if (!_knifeThrown)
				{
					// Knife can spawn so throw knife and update variables
					var knifeController = CurrentKnife.GetComponent<KnifeController>();
					knifeController.Launch(this);
					
					// Set flag so knifes can't be thrown constantly
					_knifeThrown = true;
				}
			} 
		
			if (_knifeThrown)
			{
				UpdateKnifeTimer();
			}
		}

		private bool GetTriggerDown()
		{
			bool triggerDown;
			
			// Handle input for both touch and mouse (for testing)
			if (Input.touchCount > 0)
			{
				// We only need to handle touch for one finger
				var touchPhase = Input.GetTouch(0).phase;
				triggerDown = touchPhase == TouchPhase.Began;
			}
			else
			{
				// Handle input for Souse
				triggerDown = Input.GetMouseButtonDown(0);
			}

			return triggerDown;
		}

		/// <summary>
		/// Updates the timer and then spawns a new knife when the timer is reached
		/// </summary>
		private void UpdateKnifeTimer()
		{
			// Update timer so that we don't immediately spawn new knifes
			_respawnTimer += Time.deltaTime;

			// Timer has run out so we are safe to spawn new knife
			if (_respawnTimer >= RespawnTimeout)
			{
				// Reset the timer to zero for next knife
				_respawnTimer = 0.0f;
				_knifeThrown = false;

				// Create the next knife if we have any left
				if (GameController.CurrentKnives > 0)
				{
					SpawnKnife();
				}
				
				
			}
		}
	}
}
