using System.Collections.Generic;
using UnityEngine;

namespace AncientForge.Inventory
{
	public class InventoryBase : MonoBehaviour
	{
		[SerializeField] private bool allowStacking;
		
		private List<InventorySlot> _slots = new();

		public bool AllowStacking => allowStacking;
		
		private void Awake( )
		{
			_slots = GetComponent<IInventorySlotPattern>( ).GetSlots( this );
		}

		private void OnValidate( )
		{
			if( GetComponent<IInventorySlotPattern>() == null )
				Debug.LogWarning( $"InventoryBase script on object {gameObject} require IInventorySlotPattern script!" );
		}
	}
}
