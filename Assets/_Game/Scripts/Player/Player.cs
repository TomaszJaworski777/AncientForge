using AncientForge.Inventory;
using UnityEngine;

namespace AncientForge
{
	public class Player : MonoBehaviour
	{
		[SerializeField] private InventoryBase inventoryBase;

		public InventoryBase Inventory => inventoryBase;
	}
}