using UnityEngine;
using UnityEngine.UI;

namespace AncientForge.WidthFill
{
	public class ImageWidthFill : MonoBehaviour
	{
		[SerializeField] private float minWidth;
		[SerializeField] private float maxWidth;

		private RectTransform _rectTransform;
		private Image         _image;

		[SerializeField]
		[Range( 0f, 1f )]
		private float fillAmount;

		public float FillAmount {
			get => fillAmount;
			set {
				fillAmount = value;

				if ( fillAmount == 0 ) {
					_image.enabled = false;
					return;
				}
				
				_image.enabled = true;
				_rectTransform.SetSizeWithCurrentAnchors( RectTransform.Axis.Horizontal,
					Mathf.Lerp( minWidth, maxWidth, fillAmount ) );
			}
		}

		private void Awake( )
		{
			_rectTransform = GetComponent<RectTransform>( );
			_image         = GetComponent<Image>( );

			//We set fill amount to itself, so all logic in setter can be executed
			FillAmount = fillAmount;
		}
	}
}