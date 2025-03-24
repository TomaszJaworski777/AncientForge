using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AncientForge.Inventory.SlotPatterns
{
	[RequireComponent( typeof( GridLayoutGroup ) )]
	public class InventoryGeneratedGridSlots : MonoBehaviour, IInventorySlotPattern
	{
		[SerializeField] private InventorySlot inventorySlotPrefab;
		[SerializeField] private Vector2Int    gridSize;

		public List<InventorySlot> GetSlots( InventoryBase inventoryBase )
		{
			var result = new List<InventorySlot>( );

			for ( var i = 0; i < gridSize.x * gridSize.y; i++ ) {
				var slot = Instantiate( inventorySlotPrefab, transform );
				slot.Initialize( inventoryBase );
				result.Add( slot );
			}

			return result;
		}
	}
}