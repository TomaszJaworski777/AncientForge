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

		public void Initialize( Machine machine )
		{
			titleText.text       = machine.MachineConfig.machineName;
			descriptionText.text = machine.MachineConfig.description;

			_durationFormat      = durationText.text;
			_successChanceFormat = successChanceText.text;

			OnRecipeChange( null, null );

			forgeButton.onClick.AddListener( ( ) => {
				if ( machine.MatchingRecipe == null )
					return;

				machine.StartJob( );
			} );
		}

		public void OnRecipeChange( Machine machine, RecipeConfig recipeConfig )
		{
			forgeButtonSelectableObject.enabled = recipeConfig != null;
			forgeButtonCanvasGroup.enabled      = recipeConfig == null;
			durationText.gameObject.SetActive( recipeConfig != null );
			successChanceText.gameObject.SetActive( recipeConfig != null );

			if ( recipeConfig == null )
				return;

			durationText.text      = string.Format( _durationFormat, machine.WorkDuration );
			successChanceText.text = string.Format( _durationFormat, Mathf.RoundToInt( machine.SuccessChance * 100f ) );
		}
	}
}