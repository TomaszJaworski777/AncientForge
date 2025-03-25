using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.Recipes;
using AncientForge.Inventory;
using AncientForge.Quests;
using UnityEngine;

namespace AncientForge.Machines
{
	public class Machine
	{
		public MachineConfig MachineConfig { get; }

		public bool IsUnlocked  { get; private set; }
		public int  UnlockStage { get; private set; }

		public InventoryContent Inventory { get; set; }

		public bool  IsWorking     { get; private set; }
		public float Progress      { get; set; }
		public float WorkDuration  { get; private set; }
		public float SuccessChance { get; private set; }

		public RecipeConfig MatchingRecipe { get; private set; }

		public Machine( MachineConfig machineConfig )
		{
			MachineConfig = machineConfig;
			UnlockStage   = machineConfig.unlockQuests.Count;
			IsUnlocked    = UnlockStage == 0;
		}

		public void QuestCompleted( QuestConfig questConfig )
		{
			if ( !MachineConfig.unlockQuests.Contains( questConfig ) )
				return;

			UnlockStage--;

			if ( UnlockStage == 0 ) {
				IsUnlocked = true;
			}
		}

		public bool UpdateRecipe( )
		{
			var matchingRecipe = MachineConfig.recipes.Where( recipe => {
				if ( recipe.ingredients.Count != Inventory.Items.Count )
					return false;

				var ingredientsCopy = new List<InventoryItemConfig>( recipe.ingredients );
				foreach ( var itemConfig in Inventory.Items.Select( itemStack => itemStack.Item.ItemConfig ) ) {
					if ( !ingredientsCopy.Contains( itemConfig ) )
						return false;

					ingredientsCopy.Remove( itemConfig );
				}

				return true;
			} ).FirstOrDefault( );

			if ( matchingRecipe == MatchingRecipe )
				return false;

			MatchingRecipe = matchingRecipe;

			return true;
		}

		public void StartJob( )
		{
			if ( MatchingRecipe == null )
				return;

			IsWorking    = true;
			WorkDuration = MatchingRecipe.duration;
		}
	}
}