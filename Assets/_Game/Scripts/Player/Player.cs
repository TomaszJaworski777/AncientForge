using System;
using AncientForge.Inventory;
using AncientForge.Quests;
using UnityEngine;

namespace AncientForge
{
	public class Player : MonoBehaviour
	{
		[SerializeField] private InventoryBase inventoryBase;
		[SerializeField] private QuestManager  questManager;

		public InventoryBase Inventory    => inventoryBase;
		public QuestManager  QuestManager => questManager;

		public Action<InventoryItem>   OnItemCrafted    { get; set; }
		public Action<QuestInProgress> OnQuestCompleted { get; set; }

		private void Awake( )
		{
			questManager.Initialize( this );
		}

		private void OnEnable( )
		{
			questManager.OnQuestCompleted += OnQuestCompletedHandler;
		}

		private void OnDisable( )
		{
			questManager.OnQuestCompleted -= OnQuestCompletedHandler;
		}

		private void OnQuestCompletedHandler( QuestInProgress quest ) => OnQuestCompleted?.Invoke( quest );

		private void OnItemCraftedHandler( InventoryItem inventoryItem ) => OnItemCrafted?.Invoke( inventoryItem );
	}
}