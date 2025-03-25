using AncientForge.Inventory;
using UnityEngine;

namespace AncientForge.BonusItems
{
	[CreateAssetMenu( fileName = "New_BonusItem", menuName = "Config/BonusItems/BonusItem", order = 0 )]
	public class BonusItemConfig : InventoryItemConfig
	{
		public enum BonusEffectType
		{
			DURATION,
			SUCCESS_CHANCE,
		}

		public BonusEffectType bonusEffectType;
		public float           bonusEffectPower;
	}
}