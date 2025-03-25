using System;
using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.Recipes;
using AncientForge.Inventory;

namespace AncientForge.Machines
{
	public class Machines
	{
		private readonly List<Machine> _machines;
		private          Machine       _currentMachine;

		public Machine CurrentMachine {
			get => _currentMachine;
			private set {
				OnCurrentMachineChange?.Invoke( _currentMachine, value );

				//We don't register to the event to ensure that this code is called after all registered event methods
				if ( _currentMachine != null ) {
					_currentMachine.Inventory.OnItemAdded   -= OnItemAdded;
					_currentMachine.Inventory.OnItemRemoved -= OnItemRemoved;
				}

				value.Inventory.OnItemAdded   += OnItemAdded;
				value.Inventory.OnItemRemoved += OnItemRemoved;

				_currentMachine = value;
				_currentMachine.UpdateRecipe( );

				OnCurrentRecipeChange?.Invoke( _currentMachine.MatchingRecipe );
			}
		}

		public Action<Machine, Machine> OnCurrentMachineChange { get; set; }
		public Action<RecipeConfig>     OnCurrentRecipeChange  { get; set; }
		public Action<Machine>          OnJobStarted           { get; set; }
		public Action<Machine>          OnJobProgress          { get; set; }
		public Action<Machine>          OnJobFinished          { get; set; }
		public Action<InventoryItem>    OnItemCrafted          { get; set; }

		public Machines( List<Machine> machineList )
		{
			_machines = machineList;
		}

		public bool SelectCurrentMachine( Machine machine, Player player )
		{
			if ( CurrentMachine == machine )
				return false;

			if ( CurrentMachine != null )
				DespawnCurrentMachine( player );

			CurrentMachine = machine;

			return true;
		}

		private void DespawnCurrentMachine( Player player )
		{
			if ( CurrentMachine.IsWorking )
				return;

			var inventoryItems = CurrentMachine.Inventory.ItemStacks;
			if ( inventoryItems.Count == 0 )
				return;

			foreach ( var itemStack in inventoryItems ) {
				var slotIndex = CurrentMachine.Inventory.GetIndexOfStack( itemStack );
				for ( var i = 0; i < itemStack.Quantity; i++ ) {
					if ( !CurrentMachine.Inventory.TryRemoveItem( slotIndex, out var item ) )
						continue;

					player.Inventory.InventoryContent.TryAddItem( item, out _ );
				}
			}
		}

		public void StartJob( )
		{
			if ( CurrentMachine == null || CurrentMachine.MatchingRecipe == null )
				return;

			CurrentMachine.StartJob( );
			OnJobStarted?.Invoke( CurrentMachine );
		}

		private void OnItemAdded( InventoryItem item, int slotIndex )
		{
			if ( !_currentMachine.UpdateRecipe( ) )
				return;

			OnCurrentRecipeChange?.Invoke( _currentMachine.MatchingRecipe );
		}

		private void OnItemRemoved( InventoryItem item, int slotIndex )
		{
			if ( !_currentMachine.UpdateRecipe( ) )
				return;

			OnCurrentRecipeChange?.Invoke( _currentMachine.MatchingRecipe );
		}

		public void ExecuteTick( float tickDuration )
		{
			foreach ( var machine in _machines.Where( machine => machine.IsWorking ) ) {
				machine.Progress += tickDuration;
			}
		}
	}
}