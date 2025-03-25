using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace AncientForge.BonusItems
{
	public class BonusPowersDisplay : MonoBehaviour
	{
		[SerializeField] private BonusLabelSettingsConfig labelSettingsConfig;
		[SerializeField] private TMP_Text                 powerPrefab;
		[SerializeField] private RectTransform            content;

		private readonly Dictionary<BonusItemConfig, TMP_Text> _powerLabels = new( );

		public void OnItemAdded( BonusItemConfig itemConfig )
		{
			if ( _powerLabels.ContainsKey( itemConfig ) )
				return;

			if ( labelSettingsConfig.labels.All( label => label.type != itemConfig.bonusEffectType ) )
				return;

			var label      = Instantiate( powerPrefab, content );
			var bonusLabel = labelSettingsConfig.labels.First( bonusLabel => bonusLabel.type == itemConfig.bonusEffectType );
			label.text = string.Format( bonusLabel.format, itemConfig.itemName,
				Mathf.RoundToInt( itemConfig.bonusEffectPower * bonusLabel.displayMultiplier ) );
		}

		public void OnItemRemoved( BonusItemConfig itemConfig )
		{
			if ( !_powerLabels.TryGetValue( itemConfig, out var label ) )
				return;

			Destroy( label.gameObject );
		}
	}
}