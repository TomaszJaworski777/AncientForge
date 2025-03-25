using System;
using System.Collections.Generic;
using System.Linq;

namespace AncientForge.Inventory
{
	public class InventoryContent
	{
		public List<InventoryItemStack> ItemStacks { get; }
		
		public Action<InventoryItem, int> OnItemAdded   { get; set; }
		public Action<InventoryItem, int> OnItemRemoved { get; set; }
		
		public List<InventoryItemStack> Items => ItemStacks.Where( itemStack => !itemStack.IsEmpty ).ToList();
		
		public bool IsFull => ItemStacks.All( itemStack => itemStack.IsFull );
		
		public InventoryContent( int inventorySize, bool allowStacking )
		{
			ItemStacks = new( inventorySize );
			for ( var i = 0; i < inventorySize; i++ ) {
				ItemStacks.Add( new( allowStacking ) );
			}
		}

		public int GetIndexOfStack( InventoryItemStack itemStack ) => ItemStacks.IndexOf( itemStack );
		
		public InventoryItem GetItem( int slotIndex ) => GetItemStack( slotIndex ).Item;

		public InventoryItemStack GetItemStack( int slotIndex )
		{
			//Not turning that into '?' operator to reduce the workload on someone who would want to add more checks in the future
			if ( slotIndex > ItemStacks.Count - 1 )
				return null;

			return ItemStacks[slotIndex];
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

			if ( !ItemStacks[slotIndex].TryAdd( item ) )
				return false;	
			
			OnItemAdded?.Invoke( item, slotIndex );
			return true;
		}

		public bool TryRemoveItem( int slotIndex, out InventoryItem item )
		{
			item = null;

			if ( slotIndex > ItemStacks.Count - 1 )
				return false;

			if ( ItemStacks[slotIndex].IsEmpty )
				return false;

			var itemStack = GetItemStack( slotIndex );
			item = itemStack.Item;

			if ( !itemStack.TryRemove( ) )
				return false;
			
			OnItemRemoved?.Invoke( item, slotIndex );
			return true;
		}

		private bool TryFindSlotForItem( InventoryItem item, out int slotIndex )
		{
			slotIndex = 0;
			var availableItemStacks = ItemStacks.Where( itemStack =>
				( itemStack.Item == item && !itemStack.IsFull ) || ( itemStack.Item == null && itemStack.IsEmpty ) ).ToList( );

			if ( !availableItemStacks.Any( ) )
				return false;

			slotIndex = ItemStacks.IndexOf( availableItemStacks.First( ) );
			return true;
		}
	}
}