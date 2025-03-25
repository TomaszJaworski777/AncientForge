using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AncientForge.Selection
{
	public class HighlightObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
	{
		private List<IHighlightEffect> _highlightEffects;

		private void Awake( )
		{
			_highlightEffects = GetComponents<IHighlightEffect>( ).ToList( );
		}

		private void OnDisable( )
		{
			OnPointerExit( null );
		}
		
		public void OnPointerEnter( PointerEventData eventData )
		{
			foreach ( var selector in _highlightEffects ) {
				selector.Activate( );
			}
		}

		public void OnPointerExit( PointerEventData eventData )
		{
			foreach ( var selector in _highlightEffects ) {
				selector.Deactivate( );
			}
		}
	}
}