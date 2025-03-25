using System;
using System.Collections.Generic;
using System.Linq;
using AncientForge.Inventory;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AncientForge.Quests
{
	public class QuestManager : MonoBehaviour
	{
		[SerializeField] private QuestListConfig questListConfig;

		private readonly List<QuestInProgress> _questsInProgress = new( );

		public Action<QuestInProgress>     OnQuestStarted      { get; set; }
		public Action<QuestInProgress>     OnQuestProgress     { get; set; }
		public Action<QuestInProgress>     OnQuestCompleted    { get; set; }
		public Action<InventoryItemConfig> OnAllQuestCompleted { get; set; }

		public void Initialize( )
		{
			foreach ( var startQuest in questListConfig.startQuests ) {
				AddQuest( startQuest );
			}
		}

		public void AddQuest( QuestConfig questConfig )
		{
			var newQuest = new QuestInProgress( questConfig );
			_questsInProgress.Add( newQuest );
			OnQuestStarted?.Invoke( newQuest );
		}

		public void OnItemCrafted( InventoryItemConfig inventoryItemConfig )
		{
			if ( _questsInProgress.Count == 0 )
				return;

			var cleanup = new List<QuestInProgress>( );
			foreach ( var quest in GetRelevantQuests( inventoryItemConfig ) ) {
				var progressIndex = quest.QuestConfig.requirements.Select( x => x.itemConfig ).ToList( )
				                         .IndexOf( inventoryItemConfig );

				quest.AddProgress( progressIndex );

				if ( !quest.IsCompleted ) {
					OnQuestProgress?.Invoke( quest );
					continue;
				}

				cleanup.Add( quest );
				OnQuestCompleted?.Invoke( quest );
			}

			cleanup.ForEach( quest => _questsInProgress.Remove( quest ) );
			cleanup.Clear( );

			if ( _questsInProgress.Count != 0 ) 
				return;
			
			var rewardIndex = Random.Range( 0, questListConfig.finishQuestRewardPool.Count );
			OnAllQuestCompleted?.Invoke( questListConfig.finishQuestRewardPool[rewardIndex] );
		}

		private List<QuestInProgress> GetRelevantQuests( InventoryItemConfig inventoryItemConfig ) => _questsInProgress.Where( quest =>
			quest
				.QuestConfig.requirements
				.Select( x => x.itemConfig )
				.Contains( inventoryItemConfig ) ).ToList( );
	}
}