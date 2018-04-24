using UnityEngine;

namespace GoodPie.Scripts.Circle
{
	public class CircleRotation : MonoBehaviour
	{
		[Tooltip("How fast the circle will rotate")]
		public float TargetRotationSpeed = 250f;
		
		[Tooltip("How fast the circle is currently rotating")]
		public float CurrentRotationSpeed;

		[Tooltip("The max rotation speed of the circle. May change depending on difficulty of circle")]
		public float MaxRotationSpeed = 350f;
		
		[Tooltip("How fast the circle is transitioning from CurrentRotationSpeed to MaxRotationSpeed")]
		public float TransitionSpeed = 0.5f;

		private void Start()
		{
			ChangeTargetSpeed();
			CurrentRotationSpeed = TargetRotationSpeed;
		}

		// Update is called once per frame
		void Update ()
		{

			// Decide if we want to change the target rotation speed
			if (Random.Range(0, 1000) < 5)
			{
				ChangeTargetSpeed();
			}

			// Determine how fast we transition form current rotation speed to target rotation speed
			if (CurrentRotationSpeed < TargetRotationSpeed)
			{
				CurrentRotationSpeed += TransitionSpeed;
			}
			else if (CurrentRotationSpeed > TargetRotationSpeed)
			{
				CurrentRotationSpeed -= TransitionSpeed;
			}
			
			
			// Rotate on its axis
			transform.Rotate(Vector3.forward * Time.deltaTime * CurrentRotationSpeed);

			
		}

		/// <summary>
		/// This is just a test function that provides the basic idea of how the circle works (random rotation)
		/// </summary>
		private void ChangeTargetSpeed()
		{
			// Choose a new rotation and a transition speed to match 
			// Don't want transition speed to be to slow (in case -MaxRotationSpeed to MaxRotationSpeed)
			// but don't want it to be too fast
			TargetRotationSpeed = Random.Range(-MaxRotationSpeed, MaxRotationSpeed);
			float speedDiff = Mathf.Abs(CurrentRotationSpeed - TargetRotationSpeed);
			TransitionSpeed = 0.1f * (speedDiff / 20f);
		}

		}
}
