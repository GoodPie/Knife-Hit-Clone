using GoodPie.Scripts.Utilities;
using UnityEngine;

namespace GoodPie.Scripts
{
	public class KnifeController : MonoBehaviour
	{
	
		[Tooltip("The force applied to knife when it's thrown")]
		public float ThrowForce = -10f;

		// Keep a reference to RigidBody as we will a lot of rigid body features
		private Rigidbody2D _rigidBody;

		private bool _hasLaunched = false;

		private KnifeProjector _launcher;


		private void Awake()
		{
			_rigidBody = GetComponent<Rigidbody2D>();
		}


		public void Launch(KnifeProjector launcher)
		{
			_hasLaunched = true;
			_launcher = launcher;
		}

		private void FixedUpdate()
		{
			// Apply force if we have launched the knife
			if (_hasLaunched)
			{
				_rigidBody.AddForce(Vector2.up * ThrowForce, ForceMode2D.Impulse);
				_hasLaunched = false;
			}
		}

		private void OnCollisionEnter2D(Collision2D other)
		{
			if (other.transform.CompareTag("Circle"))
			{
				_hasLaunched = false;
			
				// Make object stick
				_rigidBody.isKinematic = true;
				_rigidBody.collisionDetectionMode = CollisionDetectionMode2D.Discrete;

				transform.parent = other.transform;

				other.transform.GetComponent<FlashOnHit>().BeginFlash();

				_rigidBody.Sleep();
				_launcher.KnifeLanded(true);
			} 
			else if (other.transform.CompareTag("Knife"))
			{
				_hasLaunched = false;
				_launcher.KnifeLanded(false);
			}
		
		}
	}
}
