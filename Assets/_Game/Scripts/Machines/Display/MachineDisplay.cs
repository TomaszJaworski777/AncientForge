using System;
using System.Collections.Generic;
using _Game.Scripts.Recipes;
using AncientForge.Inventory;
using UnityEngine;

namespace AncientForge.Machines
{
	public class MachineDisplay : MonoBehaviour
	{
		[SerializeField] private RectTransform buttonListParent;
		[SerializeField] private RectTransform machineUiParent;

		private          MachineManager                           _machineManager;
		private readonly Dictionary<MachineConfig, MachineButton> _machines = new( );
		private          MachineWidget                            _currentMachineWidget;

		public Action<InventoryContent, InventoryItem, int> OnItemPressed { get; set; }

		private void Awake( )
		{
			_machineManager = GetComponent<MachineManager>( );
		}

		public void InitializeMachine( Machine machine )
		{
			var machineButton = Instantiate( machine.MachineConfig.buttonPrefab, buttonListParent );
			if ( _machines.TryAdd( machine.MachineConfig, machineButton ) ) {
				machineButton.Initialize( machine );
				machineButton.OnClick += ( ) => {
					if ( !machine.IsUnlocked )
						return;

					_machineManager.SelectMachine( machine );
				};
				
				return;
			}

			Destroy( machineButton.gameObject );
		}

		public InventoryBase SelectMachine( Machine machine, Machines machines )
		{
			if ( _currentMachineWidget != null ) {
				_currentMachineWidget.MachineInventory.DisplayEvents.OnItemPressed -= OnInventoryItemPress;
				Destroy( _currentMachineWidget.gameObject );
			}

			_currentMachineWidget = Instantiate( machine.MachineConfig.uiPrefab, machineUiParent );
			_currentMachineWidget.Initialize( machine, machines );
			
			_currentMachineWidget.MachineInventory.DisplayEvents.OnItemPressed += OnInventoryItemPress;
			
			return _currentMachineWidget.MachineInventory;
		}

		private void OnInventoryItemPress( InventoryContent content, InventoryItem item, int slotIndex )
		{
			OnItemPressed?.Invoke(content, item, slotIndex);
		}
		
		public void OnJobStateChange( Machine machine )
		{
			if(_currentMachineWidget == null)
				return;
			
			_currentMachineWidget.OnJobStateChange( machine );
		}
	}
}