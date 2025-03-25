namespace AncientForge.Inventory
{
	public class InventoryItemStack
	{
		private bool _allowStacking;

		public InventoryItem Item     { get; private set; }
		public int           Quantity { get; private set; }

		public bool IsEmpty => Item == null || Quantity == 0;

		public bool IsFull =>
			Item != null && ( ( Item.ItemConfig.stackSize == Quantity && _allowStacking ) || ( Quantity == 1 && !_allowStacking ) );

		public InventoryItemStack( bool allowStacking )
		{
			_allowStacking = allowStacking;
		}
		
		public bool TryAdd( InventoryItem item )
		{
			if ( IsFull )
				return false;

			Item ??= item;
			Quantity++;

			return true;
		}
		
		public bool TryRemove( )
		{
			if ( Quantity == 0 || Item == null )
				return false;

			if ( --Quantity == 0 )
				Item = null;

			return true;
		}
		
		public void Reset( )
		{
			Item     = null;
			Quantity = 0;
		}
	}
}