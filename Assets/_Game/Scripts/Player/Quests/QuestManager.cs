using System;
using System.Collections.Generic;
using System.Linq;
using AncientForge.Inventory;
using UnityEngine;

namespace AncientForge.Quests
{
	public class QuestManager : MonoBehaviour
	{
		[SerializeField] private QuestListConfig questListConfig;

		private readonly List<QuestInProgress> _questsInProgress = new( );
		private          Player                _player;

		public Action<QuestInProgress> OnQuestStarted   { get; set; }
		public Action<QuestInProgress> OnQuestProgress  { get; set; }
		public Action<QuestInProgress> OnQuestCompleted { get; set; }

		public void Initialize( Player player )
		{
			_player               =  player;
			_player.OnItemCrafted += OnItemCrafted;

			foreach ( var startQuest in questListConfig.startQuests ) {
				AddQuest( startQuest );
			}
		}

		private void OnDestroy( )
		{
			_player.OnItemCrafted -= OnItemCrafted;
		}

		public void AddQuest( QuestConfig questConfig )
		{
			var newQuest = new QuestInProgress( questConfig );
			_questsInProgress.Add( newQuest );
			OnQuestStarted?.Invoke( newQuest );
		}

		private void OnItemCrafted( InventoryItem inventoryItem )
		{
			var cleanup = new List<QuestInProgress>( );
			foreach ( var quest in GetRelevantQuests( inventoryItem ) ) {
				var progressIndex = quest.QuestConfig.requirements.Select( x => x.itemConfig ).ToList( )
				                         .IndexOf( inventoryItem.ItemConfig );

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
		}

		private List<QuestInProgress> GetRelevantQuests( InventoryItem inventoryItem ) => _questsInProgress.Where( quest => quest
			.QuestConfig.requirements
			.Select( x => x.itemConfig )
			.Contains( inventoryItem.ItemConfig ) ).ToList( );
	}
}