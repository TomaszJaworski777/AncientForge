using AncientForge.Inventory;
using UnityEngine;

namespace AncientForge.Tooltip
{
	public class TooltipManager : MonoBehaviour
	{
		[SerializeField] private RectTransform tooltipObject;
		[SerializeField] private TooltipWidget tooltipWidget;
		[SerializeField] private Canvas        canvas;

		public static TooltipManager Instance { get; private set; }

		private void Awake( )
		{
			Instance = this;
		}

		public void SpawnTooltip( Vector2 position, InventoryItemConfig itemConfig )
		{
			tooltipObject.gameObject.SetActive( true );
			tooltipObject.position = position;
			tooltipWidget.SetupTooltip( itemConfig );
		}

		public void HideTooltip( )
		{
			tooltipObject.gameObject.SetActive( false );
		}
	}
}