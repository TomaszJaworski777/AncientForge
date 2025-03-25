using System;
using AncientForge.Selection;
using AncientForge.WidthFill;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AncientForge.Machines
{
	public class MachineButton : MonoBehaviour, IPointerClickHandler
	{
		[SerializeField] private TMP_Text         nameText;
		[SerializeField] private ImageWidthFill   fill;
		[SerializeField] private GameObject       lockObject;
		[SerializeField] private SelectableObject selectableObject;

		public Action OnClick { get; set; }

		public void Initialize( Machine machine )
		{
			nameText.text = machine.MachineConfig.machineName;
			lockObject.SetActive( !machine.IsUnlocked );
			selectableObject.enabled = machine.IsUnlocked;
		}

		public void Unlock( )
		{
			lockObject.SetActive( false );
			selectableObject.enabled = true;
		}

		public void OnPointerClick( PointerEventData eventData )
		{
			OnClick?.Invoke( );
		}

		public void OnJobProgress( Machine machine )
		{
			fill.FillAmount = !machine.IsWorking ? 0 : machine.Progress / machine.WorkDuration;
		}
	}
}