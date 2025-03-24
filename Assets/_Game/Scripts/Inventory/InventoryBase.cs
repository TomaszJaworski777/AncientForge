using System;
using System.Collections.Generic;
using UnityEngine;

namespace AncientForge.Inventory
{
	[RequireComponent( typeof( InventoryDisplay ) )]
	public class InventoryBase : MonoBehaviour
	{
		[SerializeField] private InventorySettingsConfig inventorySettings;

		private InventoryDisplay _display;
		private InventoryContent _items;

		public InventoryDisplay.Events DisplayEvents => _display?.DisplayEvents;

		public List<InventoryItemStack> Items => _items.Items;

		private void Awake( )
		{
			//Separated display script allows for easy modification in the scenario where you want to use the inventory without display
			//(f.ex. enemy unit inventory)
			_display = GetComponent<InventoryDisplay>( );
			_display.Initialize( this );

			_items = new( _display.SlotCount );
		}

		public InventoryItem GetItem( int slotIndex ) => _items.GetItem( slotIndex );

		public InventoryItemStack GetItemStack( int slotIndex ) => _items.GetItemStack( slotIndex );

		public bool TryAdd( InventoryItemConfig itemConfig ) => TryAdd( new InventoryItem( itemConfig ) );

		public bool TryAdd( InventoryItem item )
		{
			if ( !_items.TryAddItem( item, out var slotIndex ) )
				return false;

			_display.UpdateUI( slotIndex, _items.GetItemStack( slotIndex ) );
			return true;
		}

		public bool TryRemove( int slotIndex )
		{
			if ( !_items.TryTakeItem( slotIndex, out _ ) )
				return false;

			_display.UpdateUI( slotIndex, _items.GetItemStack( slotIndex ) );
			return true;
		}
	}
}