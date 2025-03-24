using System.Collections.Generic;

namespace AncientForge.Inventory
{
	public interface IInventorySlotPattern
	{
		public List<InventorySlot> GetSlots( InventoryDisplay inventoryDisplay );
	}
}