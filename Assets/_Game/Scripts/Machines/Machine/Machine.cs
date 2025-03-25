using AncientForge.Inventory;
using AncientForge.Quests;

namespace AncientForge.Machines
{
	public class Machine
	{
		public MachineConfig MachineConfig { get; }

		public bool IsUnlocked  { get; private set; }
		public int  UnlockStage { get; private set; }

		public InventoryBase Inventory { get; set; }

		public bool IsWorking { get;  private set; }
		public float Progress { get;  set; }
		public float WorkDuration { get;  private set; }
		
		
		
		//TODO: Add property with currently processed recipe

		public Machine( MachineConfig machineConfig )
		{
			MachineConfig = machineConfig;
			UnlockStage   = machineConfig.unlockQuests.Count;
			IsUnlocked    = UnlockStage == 0;
		}

		public void QuestCompleted( QuestConfig questConfig )
		{
			if ( !MachineConfig.unlockQuests.Contains( questConfig ) )
				return;

			UnlockStage--;

			if ( UnlockStage == 0 ) {
				IsUnlocked = true;
			}
		}

		public void StartJob( float duration ) //TODO: Calculate duration based on recipe on bonuses
		{
			IsWorking    = true;
			WorkDuration = duration;
		} 
	}
}