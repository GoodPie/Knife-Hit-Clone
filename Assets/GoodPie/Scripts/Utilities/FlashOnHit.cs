using UnityEngine;

namespace GoodPie.Scripts.Utilities
{
	public class FlashOnHit : MonoBehaviour
	{

		[Tooltip("Color to flash")]
		public Color FlashColor;
		private Color _originalColor; // Keep a reference to the original sprite color so we can change back
		
		[Tooltip("How long the flash will last")]
		public float FlashTime = 0.5f;
		private float _flashTimer = 0.0f;
		
		private bool _isFlashing = false;
		
		// Keep a reference to the renderer as it's faster than getting the component each flash
		private SpriteRenderer _renderer;

		private void Start()
		{
			_renderer = GetComponent<SpriteRenderer>();
			_originalColor = _renderer.color; 
		}

		/// <summary>
		/// Initializes the flash 
		/// </summary>
		public void BeginFlash()
		{
			// Reset the timer here so it can flash for a long time on multiple hits
			_flashTimer = 0.0f;
			_isFlashing = true;
			_renderer.color = FlashColor;
		}

		private void Update()
		{
			if (_isFlashing)
			{
				_flashTimer += Time.deltaTime;
				
				if (_flashTimer > FlashTime)
				{
					_isFlashing = false;
					
					// Return renderer to original color
					_renderer.color = _originalColor;
					
				}
			}
		}
	}
}
