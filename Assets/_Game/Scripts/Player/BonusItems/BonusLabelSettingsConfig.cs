using System.Collections.Generic;
using UnityEngine;

namespace AncientForge.BonusItems
{
	[CreateAssetMenu( fileName = "New_BonusLabelSettings", menuName = "Config/BonusItems/BonusLabelSettings", order = 0 )]
	public class BonusLabelSettingsConfig : ScriptableObject
	{
		[System.Serializable]
		public struct BonusLabel
		{
			public BonusItemConfig.BonusEffectType type;
			public string                          format;
			public float                           displayMultiplier;
		}

		public List<BonusLabel> labels = new();
	}
}