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
		private List<ISelectionEffect> _selectionEffects;

		public void Initialize( InventoryDisplay inventoryDisplay, int index )
		{
			_selectionEffects = GetComponents<ISelectionEffect>( ).ToList( );

			onClick.AddListener( ( ) => inventoryDisplay.OnInventorySlotPressed( index ) );
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

		public void UpdateUI( InventoryItemStack itemStack )
		{
			
		}
		
		protected override void OnValidate( )
		{
			base.OnValidate( );

			if ( GetComponent<ISelectionEffect>( ) == null )
				Debug.LogWarning( $"InventorySlot script on object {gameObject} require ISelectionEffect script!" );
		}
	}
}