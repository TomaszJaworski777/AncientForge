using UnityEngine;
using UnityEngine.UI;

namespace AncientForge.Selection
{
	public class ColorSwapHighlightEffect : MonoBehaviour, IHighlightEffect
	{
		[SerializeField] private Image target;
		[SerializeField] private Color selectedColor;

		private Color _baseColor;

		private void Awake( )
		{
			_baseColor = target.color;
		}

		public void Activate( )
		{
			target.color = selectedColor;
		}

		public void Deactivate( )
		{
			target.color = _baseColor;
		}
	}
}