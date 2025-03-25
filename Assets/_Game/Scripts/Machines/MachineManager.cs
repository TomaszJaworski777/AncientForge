using System;
using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.Recipes;
using AncientForge.Inventory;
using UnityEngine;

namespace AncientForge.Machines
{
	[RequireComponent( typeof( MachineDisplay ) )]
	public class MachineManager : MonoBehaviour
	{
		[SerializeField] private MachineListConfig machineListConfig;

		private MachineDisplay _machineDisplay;
		private Machines       _machines;
		private Player         _player;

		public void Initialize( Player player )
		{
			_machineDisplay = GetComponent<MachineDisplay>( );

			var machineList = new List<Machine>( );

			foreach ( var machineConfig in machineListConfig.machines ) {
				var newMachine = new Machine( machineConfig );
				_machineDisplay.InitializeMachine( newMachine );
				machineList.Add( newMachine );
			}

			_machines = new( machineList );
			_player   = player;

			_player.Inventory.DisplayEvents.OnItemPressed += OnPlayerItemPass;
			_machineDisplay.OnItemPressed                 += OnMachineItemPress;
			_machines.OnCurrentMachineChange              += OnCurrentMachineChange;
			_machines.OnCurrentRecipeChange               += OnCurrentRecipeChange;

			SelectMachine( machineList.First( ) );
		}

		public bool SelectMachine( Machine machine ) => _machines.SelectCurrentMachine( machine, _player );

		private void OnCurrentMachineChange( Machine oldMachine, Machine newMachine )
		{
			var inventory = _machineDisplay.SelectMachine( newMachine, _machines );
			newMachine.Inventory      = inventory.InventoryContent;
		}

		private void OnPlayerItemPass( InventoryItem item, Action callback )
		{
			if ( item == null )
				return;

			if ( !_machineDisplay.CurrentMachineInventoryBase.TryAddItem( item) )
				return;
			
			callback?.Invoke( );
		}

		private void OnMachineItemPress( InventoryItem item, Action callback )
		{
			if ( item == null )
				return;

			if ( _machines.CurrentMachine.IsWorking )
				return;

			if ( !_player.Inventory.TryAddItem( item ) )
				return;

			callback?.Invoke( );
		}

		private void OnCurrentRecipeChange( RecipeConfig recipeConfig )
		{
			_machineDisplay.OnRecipeChange( _machines.CurrentMachine, recipeConfig );
		}

		private void Update( )
		{
			_machines.ExecuteTick( Time.deltaTime );
		}
	}
}