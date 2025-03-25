using System.Collections.Generic;
using AncientForge.BonusItems;
using UnityEngine;

namespace AncientForge.Inventory
{
	[CreateAssetMenu( fileName = "New_InventorySettingsConfig", menuName = "Config/Inventory/InventorySettings", order = 0 )]
	public class InventorySettingsConfig : ScriptableObject
	{
		public bool allowStacking;

		[System.Serializable]
		public struct StartResource
		{
			public InventoryItemConfig itemConfig;
			public Vector2Int          quantityRange;
		}

		public List<StartResource> startingResources = new( );

		[System.Serializable]
		public struct BonusItem
		{
			public BonusItemConfig itemConfig;
			public float           dropChance;
		}

		public List<BonusItem> bonusItems = new( );
	}
}