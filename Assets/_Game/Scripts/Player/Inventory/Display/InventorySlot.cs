﻿using AncientForge.Selection;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AncientForge.Inventory
{
	public class InventorySlot : MonoBehaviour, IPointerClickHandler
	{
		[SerializeField] private Image    icon;
		[SerializeField] private TMP_Text quantityNumber;
		
		private InventoryDisplay       _inventoryDisplay;
		private int                    _slotIndex;

		private TooltipHighlightEffect _tooltipHighlight;

		public void Initialize( InventoryDisplay inventoryDisplay, int index )
		{
			_tooltipHighlight = GetComponent<TooltipHighlightEffect>( );
			
			_inventoryDisplay = inventoryDisplay;
			_slotIndex        = index;

			ResetSlotUI( );
		}

		public void OnPointerClick( PointerEventData eventData )
		{
			_inventoryDisplay.OnInventorySlotPressed( _slotIndex );
		}

		public void UpdateDisplay( InventoryItemStack itemStack )
		{
			if ( itemStack.IsEmpty ) {
				ResetSlotUI( );
				return;
			}

			icon.sprite = itemStack.Item.ItemConfig.icon;
			icon.color  = new( 1, 1, 1, 1 );

			quantityNumber.text = itemStack.Quantity > 1 ? itemStack.Quantity.ToString( ) : string.Empty;

			_tooltipHighlight?.SetItemConfig( itemStack.Item.ItemConfig );
		}

		private void ResetSlotUI( )
		{
			icon.sprite         = null;
			icon.color          = new( 0, 0, 0, 0 );
			quantityNumber.text = string.Empty;

			_tooltipHighlight?.SetItemConfig( null );
		}

		protected void OnValidate( )
		{
			if ( GetComponent<IHighlightEffect>( ) == null )
				Debug.LogWarning( $"InventorySlot script on object {gameObject} require ISelectionEffect script!" );
		}
	}
}