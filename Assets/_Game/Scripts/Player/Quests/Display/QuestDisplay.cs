using System.Collections.Generic;
using UnityEngine;

namespace AncientForge.Quests
{
	[RequireComponent( typeof( QuestManager ) )]
	public class QuestDisplay : MonoBehaviour
	{
		[SerializeField] private QuestWidget   questWidgetPrefab;
		[SerializeField] private RectTransform contentParent;

		private readonly Dictionary<QuestInProgress, QuestWidget> _questWidgets = new( );
		private          QuestManager                             _questManager;

		private void Awake( )
		{
			_questManager = GetComponent<QuestManager>( );
		}

		private void OnEnable( )
		{
			_questManager.OnQuestStarted   += OnQuestStarted;
			_questManager.OnQuestProgress  += OnQuestProgress;
			_questManager.OnQuestCompleted += OnQuestCompleted;
		}

		private void OnDisable( )
		{
			_questManager.OnQuestStarted   -= OnQuestStarted;
			_questManager.OnQuestProgress  -= OnQuestProgress;
			_questManager.OnQuestCompleted -= OnQuestCompleted;
		}

		private void OnQuestStarted( QuestInProgress quest )
		{
			var widget = Instantiate( questWidgetPrefab, contentParent );
			widget.Initialize( quest );

			if ( _questWidgets.TryAdd( quest, widget ) )
				return;

			Destroy( widget.gameObject );
		}

		private void OnQuestProgress( QuestInProgress quest )
		{
			if ( !_questWidgets.TryGetValue( quest, out var widget ) )
				return;

			widget.UpdateDisplay( quest );
		}

		private void OnQuestCompleted( QuestInProgress quest )
		{
			if ( !_questWidgets.TryGetValue( quest, out var widget ) )
				return;

			Destroy( widget.gameObject );
			_questWidgets.Remove( quest );
		}
	}
}