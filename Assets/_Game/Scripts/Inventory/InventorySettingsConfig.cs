using UnityEngine;

namespace AncientForge.Inventory
{
	[CreateAssetMenu( fileName = "NewInventorySettingsConfig", menuName = "Config/Inventory/InventorySettings", order = 0 )]
	public class InventorySettingsConfig : ScriptableObject
	{
		public bool allowStacking;
	}
}