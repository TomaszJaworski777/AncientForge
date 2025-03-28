﻿using System;
using System.Collections.Generic;
using AncientForge.BonusItems;
using UnityEngine;

namespace AncientForge.Inventory
{
	public class InventoryDisplay : MonoBehaviour
	{
		public class Events
		{
			public Action<InventoryContent, InventoryItem, int> OnItemPressed { get; set; }
		}

		private List<InventorySlot> _slots = new( );
		private InventoryBase       _inventoryBase;

		public int    SlotCount     => _slots.Count;
		public Events DisplayEvents { get; private set; }

		public void Initialize( InventoryBase inventoryBase )
		{
			DisplayEvents = new( );

			_inventoryBase = inventoryBase;
			_slots         = GetComponent<IInventorySlotPattern>( ).GetSlots( this );
		}

		public void UpdateDisplay( int slotIndex, InventoryItemStack itemStack )
		{
			if ( slotIndex > SlotCount - 1 )
				return;

			_slots[slotIndex].UpdateDisplay( itemStack );
		}

		public void OnInventorySlotPressed( int slotIndex )
		{
			var itemStack = _inventoryBase.InventoryContent.GetItemStack( slotIndex );

			if ( itemStack.IsEmpty )
				return;

			if ( itemStack.Item.ItemConfig is BonusItemConfig )
				return;

			DisplayEvents.OnItemPressed?.Invoke( _inventoryBase.InventoryContent, itemStack.Item, slotIndex );
		}

		private void OnValidate( )
		{
			if ( GetComponent<IInventorySlotPattern>( ) == null )
				Debug.LogWarning( $"InventoryBase script on object {gameObject} require IInventorySlotPattern script!" );
		}
	}
}