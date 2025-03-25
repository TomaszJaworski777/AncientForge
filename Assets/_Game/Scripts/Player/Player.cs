using AncientForge.BonusItems;
using AncientForge.Inventory;
using AncientForge.Machines;
using AncientForge.Quests;
using UnityEngine;

namespace AncientForge
{
	public class Player : MonoBehaviour
	{
		[SerializeField] private InventoryBase  inventoryBase;
		[SerializeField] private QuestManager   questManager;
		[SerializeField] private MachineManager machineManager;
		[SerializeField] private BonusManager   bonusManager;

		public InventoryBase  Inventory      => inventoryBase;
		public QuestManager   QuestManager   => questManager;
		public MachineManager MachineManager => machineManager;
		public BonusManager   BonusManager   => bonusManager;

		private void Start( )
		{
			inventoryBase.Initialize( );
			questManager.Initialize( );
			machineManager.Initialize( this );
			bonusManager.Initialize( inventoryBase.InventoryContent );

			questManager.OnAllQuestCompleted      += OnAllQuestCompleted;
			questManager.OnQuestCompleted         += OnQuestCompletedHandler;
			machineManager.Machines.OnItemCrafted += OnItemCraftedHandler;
		}
		
		private void OnDestroy( )
		{
			questManager.OnAllQuestCompleted      -= OnAllQuestCompleted;
			questManager.OnQuestCompleted         -= OnQuestCompletedHandler;
			machineManager.Machines.OnItemCrafted -= OnItemCraftedHandler;
		}

		private void OnAllQuestCompleted( InventoryItemConfig rewardItemConfig )
		{
			inventoryBase.InventoryContent.TryAddItem( rewardItemConfig, out _ );
		}
		
		private void OnQuestCompletedHandler( QuestInProgress questInProgress )
		{
			machineManager.OnQuestComplete( questInProgress.QuestConfig );
		}
		
		private void OnItemCraftedHandler( Machine machine, InventoryItemConfig itemConfig )
		{
			questManager.OnItemCrafted( itemConfig );
		}
	}
}