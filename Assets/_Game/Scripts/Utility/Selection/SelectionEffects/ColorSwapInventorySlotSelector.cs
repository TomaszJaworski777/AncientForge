using UnityEngine;
using UnityEngine.UI;

namespace AncientForge.Selection
{
	public class ColorSwapSelectionEffect : MonoBehaviour, ISelectionEffect
	{
		[SerializeField] private Image target;
		[SerializeField] private Color selectedColor;

		private Color _baseColor;

		private void Awake( )
		{
			_baseColor = target.color;
		}

		public void Select( )
		{
			target.color = selectedColor;
		}

		public void Deselect( )
		{
			target.color = _baseColor;
		}
	}
}