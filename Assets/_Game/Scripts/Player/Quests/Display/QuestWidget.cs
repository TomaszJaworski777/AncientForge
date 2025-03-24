using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace AncientForge.Quests
{
	public class QuestWidget : MonoBehaviour
	{
		[SerializeField] private QuestLine questLinePrefab;
		[SerializeField] private TMP_Text  titleText;

		private List<QuestLine> _lines = new( );

		public void Initialize( QuestInProgress quest )
		{
			titleText.text = quest.QuestConfig.questName;
			for ( var i = 0; i < quest.QuestConfig.requirements.Count; i++ ) {
				var line = Instantiate( questLinePrefab, transform );
				line.UpdateDisplay( quest.QuestConfig.requirements[i], quest.Progress[i] );
				_lines.Add( line );
			}
		}

		public void UpdateDisplay( QuestInProgress quest )
		{
			for ( var i = 0; i < _lines.Count; i++ ) {
				_lines[i].UpdateDisplay( quest.QuestConfig.requirements[i], quest.Progress[i] );
			}
		}
	}
}