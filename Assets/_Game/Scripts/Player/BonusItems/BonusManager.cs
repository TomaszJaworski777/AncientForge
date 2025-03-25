using System.Collections.Generic;
using System.Linq;
using AncientForge.Inventory;
using UnityEngine;

namespace AncientForge.BonusItems
{
	[RequireComponent( typeof( BonusPowersDisplay ) )]
	public class BonusManager : MonoBehaviour
	{
		private readonly Dictionary<BonusItemConfig.BonusEffectType, List<InventoryItem>> _bonusItems = new( );

		private InventoryContent   _inventoryContent;
		private BonusPowersDisplay _bonusPowersDisplay;

		public void Initialize( InventoryContent content )
		{
			_bonusPowersDisplay = GetComponent<BonusPowersDisplay>( );
			_inventoryContent   = content;

			foreach ( var itemStack in _inventoryContent.Items ) {
				AddItem( itemStack.Item );
			}

			_inventoryContent.OnItemAdded   += OnItemAdded;
			_inventoryContent.OnItemRemoved += OnItemRemoved;
		}

		private void OnDestroy( )
		{
			_inventoryContent.OnItemAdded   -= OnItemAdded;
			_inventoryContent.OnItemRemoved -= OnItemRemoved;
		}

		public float GetBonusPower( BonusItemConfig.BonusEffectType type )
		{
			if ( !_bonusItems.TryGetValue( type, out var bonusItems ) )
				return 0;

			return bonusItems.Sum( item => ( ( BonusItemConfig )item.ItemConfig ).bonusEffectPower );
		}

		public void AddItem( InventoryItem item )
		{
			if ( item.ItemConfig is not BonusItemConfig bonusItemConfig )
				return;

			_bonusItems.TryAdd( bonusItemConfig.bonusEffectType, new( ) );
			_bonusItems[bonusItemConfig.bonusEffectType].Add( item );

			_bonusPowersDisplay.OnItemAdded( bonusItemConfig );
		}

		public void RemoveItem( InventoryItem item )
		{
			if ( item.ItemConfig is not BonusItemConfig bonusItemConfig )
				return;

			if ( !_bonusItems.TryGetValue( bonusItemConfig.bonusEffectType, out var bonusItem ) )
				return;

			bonusItem.Remove( item );

			_bonusPowersDisplay.OnItemRemoved( bonusItemConfig );
		}

		private void OnItemAdded( InventoryItem item, int slotIndex )
		{
			AddItem( item );
		}

		private void OnItemRemoved( InventoryItem item, int slotIndex )
		{
			RemoveItem( item );
		}
	}
}