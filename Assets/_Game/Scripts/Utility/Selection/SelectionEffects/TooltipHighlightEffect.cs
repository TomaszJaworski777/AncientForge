using AncientForge.Inventory;
using AncientForge.Tooltip;
using UnityEngine;

namespace AncientForge.Selection
{
	public class TooltipHighlightEffect : MonoBehaviour, IHighlightEffect
	{
		private InventoryItemConfig _itemConfig;
		private RectTransform       _transform;

		private void Awake( )
		{
			_transform = GetComponent<RectTransform>( );
		}

		public void SetItemConfig( InventoryItemConfig itemConfig )
		{
			_itemConfig = itemConfig;

			if ( _itemConfig == null )
				Deactivate( );
		}

		public void Activate( )
		{
			if ( _itemConfig == null )
				return;

			TooltipManager.Instance.SpawnTooltip( _transform.position, _itemConfig );
		}

		public void Deactivate( )
		{
			TooltipManager.Instance.HideTooltip( );
		}
	}
}