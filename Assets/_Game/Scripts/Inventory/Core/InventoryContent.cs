using System.Collections.Generic;
using System.Linq;

namespace AncientForge.Inventory
{
	public class InventoryContent
	{
		private readonly List<InventoryItemStack> _items;

		public bool IsFull => _items.All( itemStack => itemStack.IsFull );

		/// <summary>
		/// Initialize with empty item stacks.
		/// </summary>
		/// <param name="inventorySize">Size of the inventory.</param>
		public InventoryContent( int inventorySize )
		{
			_items = new( inventorySize );
			for ( var i = 0; i < inventorySize; i++ ) {
				_items.Add( new( ) );
			}
		}

		public InventoryItem GetItem( int slotIndex ) => GetItemStack( slotIndex ).Item;

		public InventoryItemStack GetItemStack( int slotIndex )
		{
			//Not turning that into '?' operator to reduce the workload on someone who would want to add more checks in the future
			if ( slotIndex > _items.Count - 1 )
				return null;

			return _items[slotIndex];
		}

		public bool TryAddItem( InventoryItemConfig itemConfig ) => TryAddItem( new InventoryItem( itemConfig ) );

		public bool TryAddItem( InventoryItem item )
		{
			if ( IsFull )
				return false;

			if ( !TryFindSlotForItem( item, out var slotIndex ) )
				return false;

			return _items[slotIndex].Add( item );
		}

		public bool TryTakeItem( int slotIndex, out InventoryItem item )
		{
			item = null;
			
			if ( slotIndex > _items.Count - 1 )
				return false;

			if ( _items[slotIndex].IsEmpty )
				return false;

			var itemStack = GetItemStack( slotIndex );
			item = itemStack.Item;
			return itemStack.Remove( );
		}

		private bool TryFindSlotForItem( InventoryItem item, out int slotIndex )
		{
			slotIndex = 0;
			var availableItemStacks = _items.Where( itemStack => itemStack.Item == item && !itemStack.IsFull ).ToList( );

			if ( !availableItemStacks.Any( ) )
				return false;

			slotIndex = _items.IndexOf( availableItemStacks.First( ) );
			return true;
		}
	}
}