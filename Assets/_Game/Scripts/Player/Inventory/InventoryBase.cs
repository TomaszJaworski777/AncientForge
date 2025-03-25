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

			RegisterEvents();
			InitializeStartResources( );
			InitializeBonusItems( );
		}
		
		public void Initialize( InventoryContent inventoryContent )
		{
			_display = GetComponent<InventoryDisplay>( );
			_display.Initialize( this );

			InventoryContent = inventoryContent;

			for ( var slotIndex = 0; slotIndex < InventoryContent.ItemStacks.Count; slotIndex++ ) {
				_display.UpdateDisplay( slotIndex, InventoryContent.GetItemStack( slotIndex ) );
			}
			
			RegisterEvents();
		}

		private void RegisterEvents( )
		{
			InventoryContent.OnItemAdded   += OnItemAdded;
			InventoryContent.OnItemRemoved += OnItemRemoved;
		}
		
		private void OnDestroy( )
		{
			InventoryContent.OnItemAdded   -= OnItemAdded;
			InventoryContent.OnItemRemoved -= OnItemRemoved;
		}

		private void InitializeStartResources( )
		{
			foreach ( var startResource in inventorySettings.startingResources ) {
				//We add +1 to y in Random.Range because by default Random.Range second bound is exclusive, and we want it to be inclusive
				var resourceCount = startResource.quantityRange.x == startResource.quantityRange.y
					? startResource.quantityRange.x
					: Random.Range( startResource.quantityRange.x, startResource.quantityRange.y + 1 );

				for ( var i = 0; i < resourceCount; i++ ) {
					if ( !InventoryContent.TryAddItem( startResource.itemConfig, out _ ) )
						break;
				}
			}
		}
		
		private void InitializeBonusItems( )
		{
			foreach ( var bonusItem in inventorySettings.bonusItems ) {
				if( Random.Range( 0.001f, 1f ) > bonusItem.dropChance )
					continue;

				InventoryContent.TryAddItem( bonusItem.itemConfig, out _ );
			}
		}
		
		private void OnItemAdded( InventoryItem item, int slotIndex )
		{
			_display.UpdateDisplay( slotIndex, InventoryContent.GetItemStack( slotIndex ) );
		}
		
		private void OnItemRemoved( InventoryItem item, int slotIndex  )
		{
			_display.UpdateDisplay( slotIndex, InventoryContent.GetItemStack( slotIndex ) );
		}
	}
}