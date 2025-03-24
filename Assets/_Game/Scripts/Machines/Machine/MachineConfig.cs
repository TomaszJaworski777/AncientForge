using AncientForge.Quests;
using UnityEngine;

namespace AncientForge.Machines
{
	[CreateAssetMenu( fileName = "New_MachineConfig", menuName = "Config/Machines/Machine", order = 0 )]
	public class MachineConfig : ScriptableObject
	{
		[Header( "Settings" )]
		public string      machineName;
		public string      description;
		public QuestConfig unlockQuest;

		[Header( "Prefabs" )]
		public MachineWidget uiPrefab;
		public MachineButton buttonPrefab;

		//TODO: list of available recipes
	}
}