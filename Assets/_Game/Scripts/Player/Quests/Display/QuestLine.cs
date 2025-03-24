using TMPro;
using UnityEngine;

namespace AncientForge.Quests
{
	public class QuestLine : MonoBehaviour
	{
		[SerializeField] private TMP_Text lineText;
		[SerializeField] private TMP_Text progressText;

		public void Display( QuestConfig.QuestRequirement requirement, int progress )
		{
			var pluralChar = requirement.quantity > 1 ? "s" : "";
			lineText.text     = $"Craft {requirement.quantity} {requirement.itemConfig.itemName}{pluralChar}.";
			progressText.text = $"{progress}/{requirement.quantity}";
		}
	}
}