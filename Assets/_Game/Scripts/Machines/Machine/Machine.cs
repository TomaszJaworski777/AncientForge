using System;
using System.Collections.Generic;
using System.Linq;
using AncientForge.Recipes;
using AncientForge.Inventory;
using AncientForge.Quests;

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

		public bool CheckForUnlock( QuestConfig questConfig )
		{
			if ( !MachineConfig.unlockQuests.Contains( questConfig ) )
				return false;

			if ( --UnlockStage != 0 ) 
				return false;
			
			IsUnlocked = true;
			
			return true;
		}

		public bool UpdateRecipe( )
		{
			var matchingRecipe = MachineConfig.recipes.Where( recipe => {
				if ( recipe.ingredients.Count != Inventory.Items.Count )
					return false;

				//This odd method allows us to check for duplicate items in recipe
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

		public void CalculateJobParameters( )
		{
			if ( MatchingRecipe == null )
				return;

			WorkDuration  = MatchingRecipe.duration;
			SuccessChance = MatchingRecipe.successChance;
		}

		public void StartJob( )
		{
			if ( MatchingRecipe == null )
				return;

			IsWorking = true;
		}

		public void JobComplete( )
		{
			IsWorking      = false;
			Progress       = 0;

			WorkDuration  = 0;
			SuccessChance = 0;

			ClearInventory( null );
		}

		public void ClearInventory( Action<InventoryItem, int> callback )
		{
			foreach ( var itemStack in Inventory.Items ) {
				var slotIndex = Inventory.GetIndexOfStack( itemStack );
				for ( var i = 0; i < itemStack.Quantity; i++ ) {
					if ( !Inventory.TryRemoveItem( slotIndex, out var item ) )
						continue;

					callback?.Invoke( item, slotIndex );
				}
			}
		}
	}
}