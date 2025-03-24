using UnityEngine;


namespace AncientForge.Selectors
{
	public class ResizeSelectionEffect : MonoBehaviour, ISelectionEffect
	{
		[SerializeField] private RectTransform target;
		[SerializeField] private float         sizeMultiplier;

		private Vector3 _baseSize;

		private void Awake( )
		{
			_baseSize = target.localScale;
		}

		public void Select( )
		{
			target.localScale = _baseSize * sizeMultiplier;
		}

		public void Deselect( )
		{
			target.localScale = _baseSize;
		}
	}
}