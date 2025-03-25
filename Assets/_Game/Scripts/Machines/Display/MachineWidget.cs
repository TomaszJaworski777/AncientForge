using AncientForge.Inventory;
using AncientForge.Selection;
using AncientForge.ScaleFill;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AncientForge.Machines
{
	public class MachineWidget : MonoBehaviour
	{
		[SerializeField] private TMP_Text               titleText;
		[SerializeField] private TMP_Text               descriptionText;
		[SerializeField] private Image                  resultIcon;
		[SerializeField] private TooltipHighlightEffect resultTooltipHighlightEffect;
		[SerializeField] private InventoryBase          inventoryBase;
		[SerializeField] private Button                 forgeButton;
		[SerializeField] private HighlightObject        forgeButtonHighlightObject;
		[SerializeField] private CanvasGroup            forgeButtonCanvasGroup;
		[SerializeField] private ImageScaleFill         fill;
		[SerializeField] private ForgeStatText          durationText;
		[SerializeField] private ForgeStatText          successChanceText;

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

			_durationFormat      = durationText.Text;
			_successChanceFormat = successChanceText.Text;

			OnJobStateChange( null );

			return forgeButton;
		}

		public void OnJobStateChange( Machine machine )
		{
			ActivateForgeUI( machine?.MatchingRecipe != null && !machine.IsWorking );
			resultIcon.color = machine?.MatchingRecipe != null ? new( 1, 1, 1, 1 ) : new( 0, 0, 0, 0 );

			if ( machine?.MatchingRecipe == null ) {
				resultTooltipHighlightEffect.SetItemConfig( null );
				return;
			}

			durationText.DisplayStat( _durationFormat, Mathf.RoundToInt( machine.WorkDuration ),
				Mathf.RoundToInt( machine.MatchingRecipe.duration ) );

			successChanceText.DisplayStat( _successChanceFormat, Mathf.RoundToInt( machine.SuccessChance * 100f ),
				Mathf.RoundToInt( machine.MatchingRecipe.successChance * 100f ) );

			resultIcon.sprite = machine.MatchingRecipe.product.icon;
			resultTooltipHighlightEffect.SetItemConfig( machine.MatchingRecipe.product );
		}

		public void OnJobProgress( Machine machine )
		{
			fill.FillAmount = !machine.IsWorking ? 0 : machine.Progress / machine.WorkDuration;
		}

		private void ActivateForgeUI( bool state )
		{
			forgeButtonHighlightObject.enabled = state;
			forgeButtonCanvasGroup.enabled     = !state;
			durationText.gameObject.SetActive( state );
			successChanceText.gameObject.SetActive( state );
		}
	}
}