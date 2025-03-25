using TMPro;
using UnityEngine;

namespace AncientForge.Machines
{
	public class ForgeStatText : MonoBehaviour
	{
		[SerializeField] private Color increaseColor;
		[SerializeField] private Color decreaseColor;
		[SerializeField] private bool  lowerBetter;

		private TMP_Text _text;

		public string Text => _text.text;

		private void Awake( )
		{
			_text = GetComponent<TMP_Text>( );
		}

		public void DisplayStat( string format, int value, int expectedValue )
		{
			var color = value > expectedValue
				? ( lowerBetter ? decreaseColor : increaseColor )
				: ( value < expectedValue ? ( lowerBetter ? increaseColor : decreaseColor ) : _text.color );
			
			var htmlMark = ColorUtility.ToHtmlStringRGB( color );
			_text.text = string.Format( format, $"#{htmlMark}", value );
		}
	}
}