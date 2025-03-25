using System;
using System.Collections.Generic;
using AncientForge.Inventory;
using UnityEngine;
using UnityEngine.UI;

namespace AncientForge.Machines
{
	public class MachineDisplay : MonoBehaviour
	{
		[SerializeField] private RectTransform buttonListParent;
		[SerializeField] private RectTransform machineUiParent;

		private          MachineManager                           _machineManager;
		private readonly Dictionary<MachineConfig, MachineButton> _machineButtons = new( );
		private          MachineWidget                            _currentMachineWidget;

		public Action<InventoryContent, InventoryItem, int> OnItemPressed { get; set; }

		private void Awake( )
		{
			_machineManager = GetComponent<MachineManager>( );
		}

		public void InitializeMachine( Machine machine )
		{
			var machineButton = Instantiate( machine.MachineConfig.buttonPrefab, buttonListParent );
			if ( _machineButtons.TryAdd( machine.MachineConfig, machineButton ) ) {
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

		public (InventoryBase inventory, Button forgeButton) SelectMachine( Machine machine, Machines machines )
		{
			if ( _currentMachineWidget != null ) {
				_currentMachineWidget.MachineInventory.DisplayEvents.OnItemPressed -= OnInventoryItemPress;
				Destroy( _currentMachineWidget.gameObject );
			}

			_currentMachineWidget = Instantiate( machine.MachineConfig.uiPrefab, machineUiParent );
			var forgeButton = _currentMachineWidget.Initialize( machine );
			
			_currentMachineWidget.MachineInventory.DisplayEvents.OnItemPressed += OnInventoryItemPress;
			
			return ( _currentMachineWidget.MachineInventory, forgeButton);
		}

		private void OnInventoryItemPress( InventoryContent content, InventoryItem item, int slotIndex )
		{
			OnItemPressed?.Invoke(content, item, slotIndex);
		}
		
		public void OnJobStateChange( Machine machine, bool currentMachine )
		{
			if(_currentMachineWidget == null || !currentMachine)
				return;
			
			_currentMachineWidget.OnJobStateChange( machine );
		}
		
		public void OnJobProgress( Machine machine, bool currentMachine )
		{
			if(currentMachine)
				_currentMachineWidget.OnJobProgress( machine );
			
			_machineButtons[machine.MachineConfig].OnJobProgress( machine );
		}

		public void OnMachineUnlocked( Machine machine )
		{
			_machineButtons[machine.MachineConfig].Unlock();
		}
	}
}