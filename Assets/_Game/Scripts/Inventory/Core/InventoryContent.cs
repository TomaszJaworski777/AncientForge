using System.Collections.Generic;
using System.Linq;

namespace AncientForge.Inventory
{
	public class InventoryContent
	{
		public List<InventoryItemStack> Items { get; }

		public bool IsFull => Items.All( itemStack => itemStack.IsFull );

		/// <summary>
		/// Initialize with empty item stacks.
		/// </summary>
		/// <param name="inventorySize">Size of the inventory.</param>
		/// <param name="allowStacking">Defines if items can stack in this inventory.</param>
		public InventoryContent( int inventorySize, bool allowStacking )
		{
			Items = new( inventorySize );
			for ( var i = 0; i < inventorySize; i++ ) {
				Items.Add( new( allowStacking ) );
			}
		}

		public InventoryItem GetItem( int slotIndex ) => GetItemStack( slotIndex ).Item;

		public InventoryItemStack GetItemStack( int slotIndex )
		{
			//Not turning that into '?' operator to reduce the workload on someone who would want to add more checks in the future
			if ( slotIndex > Items.Count - 1 )
				return null;

			return Items[slotIndex];
		}

		public bool TryAddItem( InventoryItemConfig itemConfig, out int slotIndex ) =>
			TryAddItem( new InventoryItem( itemConfig ), out slotIndex );

		public bool TryAddItem( InventoryItem item, out int slotIndex )
		{
			slotIndex = 0;

			if ( IsFull )
				return false;

			if ( !TryFindSlotForItem( item, out slotIndex ) )
				return false;

			return Items[slotIndex].TryAdd( item );
		}

		public bool TryTakeItem( int slotIndex, out InventoryItem item )
		{
			item = null;

			if ( slotIndex > Items.Count - 1 )
				return false;

			if ( Items[slotIndex].IsEmpty )
				return false;

			var itemStack = GetItemStack( slotIndex );
			item = itemStack.Item;
			return itemStack.TryRemove( );
		}

		private bool TryFindSlotForItem( InventoryItem item, out int slotIndex )
		{
			slotIndex = 0;
			var availableItemStacks = Items.Where( itemStack =>
				( itemStack.Item == item && !itemStack.IsFull ) || ( itemStack.Item == null && itemStack.IsEmpty ) ).ToList( );

			if ( !availableItemStacks.Any( ) )
				return false;

			slotIndex = Items.IndexOf( availableItemStacks.First( ) );
			return true;
		}
	}
}