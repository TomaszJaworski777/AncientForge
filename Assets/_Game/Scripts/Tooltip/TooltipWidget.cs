using AncientForge.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AncientForge.Tooltip
{
	public class TooltipWidget : MonoBehaviour
	{
		[SerializeField] private Image    icon;
		[SerializeField] private TMP_Text title;
		[SerializeField] private TMP_Text description;

		public void SetupTooltip( InventoryItemConfig itemConfig )
		{
			icon.sprite      = itemConfig.icon;
			title.text       = itemConfig.itemName;
			description.text = itemConfig.description;
		}
	}
}