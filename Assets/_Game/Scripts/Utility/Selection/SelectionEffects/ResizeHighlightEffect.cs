using UnityEngine;


namespace AncientForge.Selection
{
	public class ResizeHighlightEffect : MonoBehaviour, IHighlightEffect
	{
		[SerializeField] private RectTransform target;
		[SerializeField] private float         sizeMultiplier;

		private Vector3 _baseSize;

		private void Awake( )
		{
			_baseSize = target.localScale;
		}

		public void Activate( )
		{
			target.localScale = _baseSize * sizeMultiplier;
		}

		public void Deactivate( )
		{
			target.localScale = _baseSize;
		}
	}
}