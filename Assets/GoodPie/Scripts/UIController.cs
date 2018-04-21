using System.Collections;
using System.Collections.Generic;
using GoodPie.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	// Keep reference to GameController for easier access
	public GameController GameController;
	
	// References to all UI assets (not to many so we can do this relatively easily)
	public Text CurrentScore;
	public Text CurrentStage;
	public Text KnivesCounter;
	
	// Keep references to text
	private string _stageText = "Stage: ";

	private void Awake()
	{
		// Just for safe keeping
		if (!GameController)
		{
			// GameController script will be attached ot GameController asset
			GameController = gameObject.GetComponent<GameController>();
		}
	}

	private void Update()
	{
		// TODO: Have events called rather than calling constantly in update
		// For testing, this will do
		CurrentScore.text = GameController.CurrentScore.ToString();
		CurrentStage.text = _stageText + GameController.CurrentStage.ToString();
		KnivesCounter.text = GameController.CurrentKnives.ToString() + "/" + GameController.MaxKnives.ToString();
	}
}
