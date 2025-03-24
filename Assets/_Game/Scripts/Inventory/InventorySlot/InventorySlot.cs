using System.Collections.Generic;
using System.Linq;
using AncientForge.Selectors;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AncientForge.Inventory
{
	public class InventorySlot : Button
	{
		private InventoryBase   _inventoryBase;
		private List<ISelectionEffect> _selectionEffects;

		public bool HasItem => false;
		public bool IsFull  => true;

		public void Initialize( InventoryBase inventoryBase )
		{
			_selectionEffects = GetComponents<ISelectionEffect>( ).ToList( );
			_inventoryBase = inventoryBase;
		}

		public override void OnPointerEnter( PointerEventData eventData )
		{
			base.OnPointerEnter( eventData );

			foreach ( var selector in _selectionEffects ) {
				selector.Select( );
			}
		}

		public override void OnPointerExit( PointerEventData eventData )
		{
			base.OnPointerExit( eventData );

			foreach ( var selector in _selectionEffects ) {
				selector.Deselect( );
			}
		}

		protected override void OnValidate( )
		{
			base.OnValidate( );

			if ( GetComponent<ISelectionEffect>( ) == null )
				Debug.LogWarning( $"InventorySlot script on object {gameObject} require ISelectionEffect script!" );
		}
	}
}