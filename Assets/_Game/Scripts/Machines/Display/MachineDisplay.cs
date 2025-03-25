using System.Collections.Generic;
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

		private void Awake( )
		{
			_machineManager = GetComponent<MachineManager>( );
		}

		public void InitializeMachine( Machine machine )
		{
			var machineButton = Instantiate( machine.MachineConfig.buttonPrefab, buttonListParent );
			if ( _machines.TryAdd( machine.MachineConfig, machineButton ) ) {
				machineButton.Initialize( machine );
				machineButton.OnClick += ( ) => _machineManager.SelectMachine( machine );
				return;
			}

			Destroy( machineButton.gameObject );
		}

		public InventoryBase SelectMachine( Machine machine )
		{
			if ( _currentMachineWidget != null ) {
				Destroy( _currentMachineWidget.gameObject );
			}

			_currentMachineWidget = Instantiate( machine.MachineConfig.uiPrefab, machineUiParent );
			_currentMachineWidget.Initialize( machine );
			return _currentMachineWidget.MachineInventory;
		}
	}
}