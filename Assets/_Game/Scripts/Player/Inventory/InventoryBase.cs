using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AncientForge.Inventory
{
	[RequireComponent( typeof( InventoryDisplay ) )]
	public class InventoryBase : MonoBehaviour
	{
		[SerializeField] private InventorySettingsConfig inventorySettings;

		private InventoryDisplay _display;
		private InventoryContent _items;

		public InventoryDisplay.Events DisplayEvents => _display?.DisplayEvents;

		public Action<InventoryItem> OnItemAdded   { get; set; }
		public Action<InventoryItem> OnItemRemoved { get; set; }

		public List<InventoryItemStack> Items => _items.Items.Where( itemStack => !itemStack.IsEmpty ).ToList();

		private void Awake( )
		{
			//Separated display script allows for easy modification in the scenario where you want to use the inventory without display
			//(f.ex. enemy unit inventory)
			_display = GetComponent<InventoryDisplay>( );
			_display.Initialize( this );

			_items = new( _display.SlotCount, inventorySettings.allowStacking );

			InitializeStartResources( );
		}

		private void InitializeStartResources( )
		{
			foreach ( var startResource in inventorySettings.startingResources ) {
				//We add +1 to y in Random.Range because by default Random.Range second bound is exclusive, and we want it to be inclusive
				var resourceCount = startResource.quantityRange.x == startResource.quantityRange.y
					? startResource.quantityRange.x
					: Random.Range( startResource.quantityRange.x, startResource.quantityRange.y + 1 );

				for ( var i = 0; i < resourceCount; i++ ) {
					if ( !TryAdd( startResource.itemConfig ) )
						break;
				}
			}
		}
		
		public int GetIndexOfStack( InventoryItemStack itemStack ) => _items.Items.IndexOf( itemStack );

		public InventoryItem GetItem( int slotIndex ) => _items.GetItem( slotIndex );

		public InventoryItemStack GetItemStack( int slotIndex ) => _items.GetItemStack( slotIndex );

		public bool TryAdd( InventoryItemConfig itemConfig ) => TryAdd( new InventoryItem( itemConfig ) );

		public bool TryAdd( InventoryItem item )
		{
			if ( !_items.TryAddItem( item, out var slotIndex ) )
				return false;

			OnItemAdded?.Invoke( item );
			_display.UpdateDisplay( slotIndex, _items.GetItemStack( slotIndex ) );
			return true;
		}
		
		public bool TryRemove( int slotIndex, out InventoryItem item )
		{
			if ( !_items.TryTakeItem( slotIndex, out item ) )
				return false;

			OnItemRemoved?.Invoke( item );
			_display.UpdateDisplay( slotIndex, _items.GetItemStack( slotIndex ) );
			return true;
		}
	}
}