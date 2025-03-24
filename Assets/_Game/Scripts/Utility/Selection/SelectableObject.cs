using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AncientForge.Selection
{
	public class SelectableObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
	{
		private List<ISelectionEffect> _selectionEffects;

		private void Awake( )
		{
			_selectionEffects = GetComponents<ISelectionEffect>( ).ToList( );
		}

		public void OnPointerEnter( PointerEventData eventData )
		{
			foreach ( var selector in _selectionEffects ) {
				selector.Select( );
			}
		}

		public void OnPointerExit( PointerEventData eventData )
		{
			foreach ( var selector in _selectionEffects ) {
				selector.Deselect( );
			}
		}
	}
}