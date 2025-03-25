using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AncientForge.Inventory.SlotPatterns
{
	public class CustomInventorySlots : MonoBehaviour, IInventorySlotPattern
	{
		public List<InventorySlot> GetSlots( InventoryDisplay inventoryDisplay )
		{
			var result = GetComponentsInChildren<InventorySlot>().ToList();

			for ( var i = 0; i < result.Count; i++ ) {
				result[i].Initialize( inventoryDisplay, i );
			}

			return result;
		}
	}
}