namespace AncientForge.Inventory
{
	public class InventoryItemStack
	{
		public InventoryItem Item     { get; private set; }
		public int           Quantity { get; private set; }

		public bool IsEmpty => Item == null || Quantity == 0;
		public bool IsFull => Item != null && Item.ItemConfig.stackSize == Quantity;

		/// <summary>
		/// Tries to add an item to the item stack.
		/// </summary>
		/// <param name="item">Instance of the item to add.</param>
		/// <returns>Returns true if item was added successfully, false if adding the item failed.</returns>
		public bool TryAdd( InventoryItem item )
		{
			if ( IsFull )
				return false;
			
			Item ??= item;
			Quantity++;

			return true;
		}

		/// <summary>
		/// Tries to remove 1 item from the stack. If stack quantity reaches 0, it automatically will set item to null.
		/// </summary>
		/// <returns>Returns true if item was removed successfully, returns false if item removal failed.</returns>
		public bool TryRemove( )
		{
			if( Quantity == 0 || Item == null )
				return false;
			
			if ( --Quantity == 0 )
				Item = null;

			return true;
		}

		/// <summary>
		/// Resets the item stack to 0.
		/// </summary>
		public void Reset( )
		{
			Item     = null;
			Quantity = 0;
		}
	}
}