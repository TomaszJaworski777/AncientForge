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

		public InventoryContent InventoryContent { get; private set; }

		public InventoryDisplay.Events DisplayEvents => _display?.DisplayEvents;
		
		public List<InventoryItemStack> Items => InventoryContent.ItemStacks.Where( itemStack => !itemStack.IsEmpty ).ToList();

		public void Initialize( )
		{
			//Separated display script allows for easy modification in the scenario where you want to use the inventory without display
			//(f.ex. enemy unit inventory)
			_display = GetComponent<InventoryDisplay>( );
			_display.Initialize( this );

			InventoryContent = new( _display.SlotCount, inventorySettings.allowStacking );

			InitializeStartResources( );
		}
		
		public void Initialize( InventoryContent inventoryContent )
		{
			_display = GetComponent<InventoryDisplay>( );
			_display.Initialize( this );

			InventoryContent = inventoryContent;

			for ( var slotIndex = 0; slotIndex < InventoryContent.ItemStacks.Count; slotIndex++ ) {
				_display.UpdateDisplay( slotIndex, InventoryContent.GetItemStack( slotIndex ) );
			}
		}

		private void InitializeStartResources( )
		{
			foreach ( var startResource in inventorySettings.startingResources ) {
				//We add +1 to y in Random.Range because by default Random.Range second bound is exclusive, and we want it to be inclusive
				var resourceCount = startResource.quantityRange.x == startResource.quantityRange.y
					? startResource.quantityRange.x
					: Random.Range( startResource.quantityRange.x, startResource.quantityRange.y + 1 );

				for ( var i = 0; i < resourceCount; i++ ) {
					if ( !TryAddItem( startResource.itemConfig ) )
						break;
				}
			}
		}

		public bool TryAddItem( InventoryItemConfig itemConfig ) => TryAddItem( new InventoryItem( itemConfig ) );

		public bool TryAddItem( InventoryItem item )
		{
			if ( !InventoryContent.TryAddItem( item, out var slotIndex ) )
				return false;
			
			_display.UpdateDisplay( slotIndex, InventoryContent.GetItemStack( slotIndex ) );
			return true;
		}
		
		public bool TryRemoveItem( int slotIndex, out InventoryItem item )
		{
			if ( !InventoryContent.TryRemoveItem( slotIndex, out item ) )
				return false;
			
			_display.UpdateDisplay( slotIndex, InventoryContent.GetItemStack( slotIndex ) );
			return true;
		}
	}
}