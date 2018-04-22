using UnityEngine;

namespace GoodPie.Scripts
{
	public class CircleRotation : MonoBehaviour
	{
		[Tooltip("How fast the circle will rotate")]
		public float RotationSpeed = 250f;

		public bool MovingRight = false;

		// Update is called once per frame
		void Update ()
		{
			// Allow for adjusting the direction in the future
			var actualRotationSpeed = RotationSpeed;
			if (MovingRight)
				actualRotationSpeed = -RotationSpeed;
			
			// Rotate on its axis
			transform.Rotate(Vector3.forward * Time.deltaTime * actualRotationSpeed);

			
		}
	}
}
