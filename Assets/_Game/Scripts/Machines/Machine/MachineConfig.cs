using System.Collections.Generic;
using _Game.Scripts.Recipes;
using AncientForge.Quests;
using UnityEngine;

namespace AncientForge.Machines
{
	[CreateAssetMenu( fileName = "New_MachineConfig", menuName = "Config/Machines/Machine", order = 0 )]
	public class MachineConfig : ScriptableObject
	{
		[Header( "Settings" )]
		public string            machineName;
		public string            description;
		public List<QuestConfig> unlockQuests = new();

		[Header( "Prefabs" )]
		public MachineWidget uiPrefab;
		public MachineButton buttonPrefab;

		[Header( "Recipes" )]
		public List<RecipeConfig> recipes;
	}
}