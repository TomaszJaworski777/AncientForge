using System;

namespace AncientForge.Inventory
{
	public class InventoryItem : IEquatable<InventoryItem>
	{
		public InventoryItemConfig ItemConfig { get; }

		public InventoryItem( InventoryItemConfig itemConfig )
		{
			ItemConfig = itemConfig;
		}
		
		public InventoryItem( InventoryItem item )
		{
			ItemConfig = item.ItemConfig;
		}

		public bool Equals(InventoryItem other)
		{
			return other is not null && Equals(ItemConfig, other.ItemConfig);
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as InventoryItem);
		}

		public override int GetHashCode()
		{
			return ItemConfig != null ? ItemConfig.GetHashCode() : 0;
		}

		public static bool operator ==(InventoryItem left, InventoryItem right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(InventoryItem left, InventoryItem right)
		{
			return !Equals(left, right);
		}
	}
}