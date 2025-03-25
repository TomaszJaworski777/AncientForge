using AncientForge.Inventory;
using AncientForge.Selection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AncientForge.Machines
{
	public class MachineWidget : MonoBehaviour
	{
		[SerializeField] private TMP_Text         titleText;
		[SerializeField] private TMP_Text         descriptionText;
		[SerializeField] private Image            resultIcon;
		[SerializeField] private InventoryBase    inventoryBase;
		[SerializeField] private Button           forgeButton;
		[SerializeField] private SelectableObject forgeButtonSelectableObject;
		[SerializeField] private CanvasGroup      forgeButtonCanvasGroup;
		[SerializeField] private TMP_Text         durationText;
		[SerializeField] private TMP_Text         successChanceText;

		public InventoryBase MachineInventory => inventoryBase;

		public void Initialize( Machine machine )
		{
			titleText.text       = machine.MachineConfig.machineName;
			descriptionText.text = machine.MachineConfig.description;
		}
	}
}