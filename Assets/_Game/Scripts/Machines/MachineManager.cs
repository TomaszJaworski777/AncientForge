using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.Recipes;
using AncientForge.Inventory;
using AncientForge.Quests;
using UnityEngine;

namespace AncientForge.Machines
{
	[RequireComponent( typeof( MachineDisplay ) )]
	public class MachineManager : MonoBehaviour
	{
		[SerializeField] private MachineListConfig machineListConfig;

		private MachineDisplay _machineDisplay;
		private Player         _player;

		public Machines Machines { get; private set; }

		public void Initialize( Player player )
		{
			_machineDisplay = GetComponent<MachineDisplay>( );

			var machineList = new List<Machine>( );

			foreach ( var machineConfig in machineListConfig.machines ) {
				var newMachine = new Machine( machineConfig );
				_machineDisplay.InitializeMachine( newMachine );
				machineList.Add( newMachine );
			}

			Machines = new( machineList );
			_player   = player;

			_player.Inventory.DisplayEvents.OnItemPressed += OnPlayerItemPass;
			_machineDisplay.OnItemPressed                 += OnMachineItemPress;
			Machines.OnCurrentMachineChange              += OnCurrentMachineChange;
			Machines.OnCurrentRecipeChange               += OnCurrentRecipeChange;
			Machines.OnJobStarted                        += OnJobStarted;
			Machines.OnJobProgress                       += OnJobProgress;
			Machines.OnJobFinished                       += OnJobFinished;
			Machines.OnItemCrafted                       += OnItemCrafted;
			Machines.OnMachineUnlocked                   += OnMachineUnlocked;

			SelectMachine( machineList.First( ) );
		}

		private void OnDestroy( )
		{
			_player.Inventory.DisplayEvents.OnItemPressed -= OnPlayerItemPass;
			_machineDisplay.OnItemPressed                 -= OnMachineItemPress;
			Machines.OnCurrentMachineChange              -= OnCurrentMachineChange;
			Machines.OnCurrentRecipeChange               -= OnCurrentRecipeChange;
			Machines.OnJobStarted                        -= OnJobStarted;
			Machines.OnJobProgress                       -= OnJobProgress;
			Machines.OnJobFinished                       -= OnJobFinished;
			Machines.OnItemCrafted                       -= OnItemCrafted;
			Machines.OnMachineUnlocked                   -= OnMachineUnlocked;
		}

		public void OnQuestComplete( QuestConfig questConfig ) => Machines.OnQuestComplete( questConfig );

		public bool SelectMachine( Machine machine ) => Machines.SelectCurrentMachine( machine, _player );

		private void OnCurrentMachineChange( Machine oldMachine, Machine newMachine )
		{
			( var inventory, var forgeButton ) = _machineDisplay.SelectMachine( newMachine, Machines );

			newMachine.Inventory = inventory.InventoryContent;
			forgeButton.onClick.AddListener( Machines.StartJob );
		}

		private void OnPlayerItemPass( InventoryContent content, InventoryItem item, int slotIndex )
		{
			if ( item == null )
				return;

			if ( !Machines.CurrentMachine.Inventory.TryAddItem( item, out _ ) )
				return;

			content.TryRemoveItem( slotIndex, out _ );
		}

		private void OnMachineItemPress( InventoryContent content, InventoryItem item, int slotIndex )
		{
			if ( item == null )
				return;

			if ( Machines.CurrentMachine.IsWorking )
				return;

			if ( !_player.Inventory.InventoryContent.TryAddItem( item, out _ ) )
				return;

			content.TryRemoveItem( slotIndex, out _ );
		}

		private void OnCurrentRecipeChange( RecipeConfig recipeConfig )
		{
			_machineDisplay.OnJobStateChange( Machines.CurrentMachine, true );
		}

		private void OnJobStarted( Machine machine )
		{
			_machineDisplay.OnJobStateChange( machine, Machines.CurrentMachine == machine );
		}

		private void OnJobProgress( Machine machine )
		{
			_machineDisplay.OnJobProgress( machine, machine == Machines.CurrentMachine );
		}

		private void OnJobFinished( Machine machine )
		{
			_machineDisplay.OnJobStateChange( machine, Machines.CurrentMachine == machine );
		}

		private void OnItemCrafted( Machine machine, InventoryItemConfig itemConfig )
		{
			_player.Inventory.InventoryContent.TryAddItem( itemConfig, out _ );
		}

		private void OnMachineUnlocked( Machine machine )
		{
			_machineDisplay.OnMachineUnlocked( machine );
		}

		private void Update( )
		{
			Machines.ExecuteTick( Time.deltaTime );
		}
	}
}