using System.Collections;
using System.Collections.Generic;
using GoodPie.Scripts;
using GoodPie.Scripts.Circle;
using GoodPie.Scripts.Utilities;
using UnityEngine;

public class KnifeController : MonoBehaviour
{
	
	[Tooltip("The force applied to knife when it's thrown")]
	public float ThrowForce = -10f;

	// Keep a reference to RigidBody as we will a lot of rigid body features
	private Rigidbody2D _rigidBody;

	private bool _hasLaunched = false;

	public GameObject Circle;

	private void Awake()
	{
		_rigidBody = GetComponent<Rigidbody2D>();
	}

	private void Start()
	{
		Circle = GameObject.FindWithTag("Circle");
	}

	public void Launch()
	{
		_hasLaunched = true;
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
			
			Circle.GetComponent<FlashOnHit>().BeginFlash();

			_rigidBody.Sleep();
		} 
		else if (other.transform.CompareTag("Knife"))
		{
			_hasLaunched = false;
		}
		
	}
}
