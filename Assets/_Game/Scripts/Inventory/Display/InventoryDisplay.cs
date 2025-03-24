using System;
using System.Collections.Generic;
using UnityEngine;

namespace AncientForge.Inventory
{
	public class InventoryDisplay : MonoBehaviour
	{
		public class Events
		{
			public Action<InventoryItem, Action> OnItemPressed { get; set; }
		}
		
		private List<InventorySlot> _slots = new( );
		private InventoryBase       _inventoryBase;

		public int    SlotCount => _slots.Count;
		public Events DisplayEvents    { get; private set; }

		public void Initialize( InventoryBase inventoryBase )
		{
			DisplayEvents = new( );
			
			_inventoryBase = inventoryBase;
			_slots         = GetComponent<IInventorySlotPattern>( ).GetSlots( this );
		}

		public void UpdateUI( int slotIndex, InventoryItemStack itemStack )
		{
			if ( slotIndex > SlotCount - 1 )
				return;

			_slots[slotIndex].UpdateUI( itemStack );
		}
		
		public void OnInventorySlotPressed( int slotIndex )
		{
			var itemStack = _inventoryBase.GetItemStack( slotIndex );
			
			if(itemStack.IsEmpty)
				return;
			
			DisplayEvents.OnItemPressed?.Invoke( itemStack.Item, ( ) => {
				if(!_inventoryBase.TryRemove( slotIndex ))
					Debug.LogError("Trying to take out item from empty inventory slot");
			} );
		}

		private void OnValidate( )
		{
			if ( GetComponent<IInventorySlotPattern>( ) == null )
				Debug.LogWarning( $"InventoryBase script on object {gameObject} require IInventorySlotPattern script!" );
		}
	}
}