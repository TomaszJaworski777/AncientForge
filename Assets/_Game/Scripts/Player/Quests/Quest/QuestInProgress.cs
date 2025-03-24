using System.Collections.Generic;
using System.Linq;

namespace AncientForge.Quests
{
	public class QuestInProgress
	{
		public QuestConfig QuestConfig { get; }
		public List<int>   Progress    { get; }

		public bool IsCompleted => !QuestConfig.requirements.Where( ( requirement, i ) => requirement.quantity > Progress[i] ).Any( );

		public QuestInProgress( QuestConfig questConfig )
		{
			QuestConfig = questConfig;
			Progress    = new( );

			foreach ( var _ in questConfig.requirements ) {
				Progress.Add( 0 );
			}
		}

		public void AddProgress( int progressIndex )
		{
			if ( progressIndex > Progress.Count - 1 )
				return;

			Progress[progressIndex]++;
		}
	}
}