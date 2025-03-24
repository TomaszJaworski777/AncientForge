using UnityEngine;

namespace AncientForge.Inventory
{
	[CreateAssetMenu( fileName = "NewInventoryItemConfig", menuName = "Config/Inventory/ItemConfig", order = 0 )]
	public class InventoryItemConfig : ScriptableObject
	{
		public string itemName;
		public string description;
		public Sprite icon;
		public int    stackSize;
	}
}