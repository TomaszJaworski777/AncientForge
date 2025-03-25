using System.Collections.Generic;
using AncientForge.Inventory;
using UnityEngine;

namespace AncientForge.Quests
{
	[CreateAssetMenu( fileName = "New_QuestList", menuName = "Config/Quests/QuestList", order = 0 )]
	public class QuestListConfig : ScriptableObject
	{
		public List<QuestConfig>         startQuests;
		public List<InventoryItemConfig> finishQuestRewardPool = new( );
	}
}