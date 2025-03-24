using System.Collections.Generic;
using AncientForge.Inventory;
using UnityEngine;

namespace AncientForge.Quests
{
	[CreateAssetMenu( fileName = "New_Quest", menuName = "Config/Quests/Quest", order = 0 )]
	public class QuestConfig : ScriptableObject
	{
		[System.Serializable]
		public struct QuestRequirement
		{
			public InventoryItemConfig itemConfig;
			public int                 quantity;
		}
		
		public string                 questName;
		public List<QuestRequirement> requirements;
	}
}