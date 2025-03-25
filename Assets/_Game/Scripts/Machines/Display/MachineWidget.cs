using _Game.Scripts.Recipes;
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

		private string _durationFormat      = string.Empty;
		private string _successChanceFormat = string.Empty;

		public InventoryBase MachineInventory => inventoryBase;

		public Button Initialize( Machine machine )
		{
			if ( machine.Inventory == null )
				inventoryBase.Initialize( );
			else
				inventoryBase.Initialize( machine.Inventory );

			titleText.text       = machine.MachineConfig.machineName;
			descriptionText.text = machine.MachineConfig.description;

			_durationFormat      = durationText.text;
			_successChanceFormat = successChanceText.text;

			OnJobStateChange( null );

			return forgeButton;
		}

		public void OnJobStateChange( Machine machine )
		{
			ActivateForgeUI( machine?.MatchingRecipe != null && !machine.IsWorking );

			if ( machine?.MatchingRecipe  == null )
				return;

			durationText.text      = string.Format( _durationFormat, machine.WorkDuration );
			successChanceText.text = string.Format( _successChanceFormat, Mathf.RoundToInt( machine.SuccessChance * 100f ) );
		}

		public void OnJobProgress( Machine machine )
		{
			
		}
		
		private void ActivateForgeUI( bool state )
		{
			forgeButtonSelectableObject.enabled = state;
			forgeButtonCanvasGroup.enabled      = !state;
			durationText.gameObject.SetActive( state );
			successChanceText.gameObject.SetActive( state );
		}
	}
}