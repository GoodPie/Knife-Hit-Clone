using GoodPie.Scripts;
using UnityEngine;

public class KnifeSpawn : MonoBehaviour
{

	[Tooltip("Time before the knives respawn")]
	public const float RespawnTimeout = 0.2f;
	private float _respawnTimer = 0.0f;
	
	// Flag that initiates the timer
	private bool _knifeThrown = false;
	private bool _canThrowKnife = true;
	
	// Currently spawned knife
	public GameObject CurrentKnife;
	public GameController GameController;

	private void Start()
	{
		// Create the first knife
		CurrentKnife = Instantiate(GameController.KnifeInUse);
	}

	private void Update()
	{

		// Flags for handling input default to false
		var triggerDown = false;
		var triggerUp = false;
		
		// Handle input for both touch and mouse (for testing)
		if (Input.touchCount > 0)
		{
			// We only need to handle touch for one finger
			var touchPhase = Input.GetTouch(0).phase;
			triggerDown = touchPhase == TouchPhase.Began;
			triggerUp = touchPhase == TouchPhase.Ended;
		}
		else
		{
			// Handle input for mouse
			triggerDown = Input.GetMouseButtonDown(0);
			triggerUp = Input.GetMouseButtonUp(0);
		}
		
		if (triggerDown)
		{
			if (_canThrowKnife)
			{
				// Knife can no longer be thrown until the touch has ended
				_canThrowKnife = false;
			
				if (!_knifeThrown)
				{
					// We have run out of knives so just ensure that we can't throw if we are out
					// This will be handled by GameController as well
					if (GameController.CurrentKnives <= 0) return;
					
					// Knife can spawn so throw knife and update variables
					var knifeController = CurrentKnife.GetComponent<KnifeController>();
					knifeController.Launch();
					GameController.CurrentKnives -= 1;
					
					// Set flag so knifes can't be thrown constantly
					_knifeThrown = true;
				}

			}
			
		} 
		else if (triggerUp)	
		{
			// Need to ensure that we can't have multiple touches triggering throwing knifes
			_canThrowKnife = true;
		}
		
		if (_knifeThrown)
		{
			UpdateKnifeTimer();
		}
	}

	/// <summary>
	/// Updates the timer and then spawns a new knife when the timer is reached
	/// </summary>
	private void UpdateKnifeTimer()
	{
		// Update timer so that we don't immediately spawn new knifes
		_respawnTimer += Time.deltaTime;

		// Timer has run out so we are safe to spawn new knife
		if (_respawnTimer > RespawnTimeout)
		{
			// Reset the timer to zero for next knife
			_respawnTimer = 0.0f;
			_knifeThrown = false;

			// Create the next knife if we have any left
			if (GameController.CurrentKnives <= 0) return;
			CurrentKnife = Instantiate(GameController.KnifeInUse);
		}
	}
}
