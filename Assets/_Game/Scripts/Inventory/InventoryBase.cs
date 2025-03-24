using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace AncientForge.Inventory
{
	public class InventoryBase : MonoBehaviour //TODO: Inventory manipulation with updating UI and calling events
	{
		[FormerlySerializedAs( "inventorySettings" )] [SerializeField] private InventorySettingsConfig inventorySettingsConfig;
		
		private List<InventorySlot> _slots = new();
		private InventoryContent    _content;
		
		private void Awake( )
		{
			_slots   = GetComponent<IInventorySlotPattern>( ).GetSlots( this );
			_content = new( _slots.Count );
		}

		public void OnInventorySlotPressed( int slotIndex )
		{
			var itemStack = _content.GetItemStack( slotIndex );
			Debug.Log($"slot {slotIndex} pressed. isEmpty = {itemStack.IsEmpty}");
		}
		
		private void OnValidate( )
		{
			if( GetComponent<IInventorySlotPattern>() == null )
				Debug.LogWarning( $"InventoryBase script on object {gameObject} require IInventorySlotPattern script!" );
		}
	}
}
