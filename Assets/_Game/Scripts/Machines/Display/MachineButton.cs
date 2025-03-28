﻿using System;
using AncientForge.Selection;
using AncientForge.ScaleFill;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace AncientForge.Machines
{
	public class MachineButton : MonoBehaviour, IPointerClickHandler
	{
		[SerializeField]                                              private TMP_Text        nameText;
		[SerializeField]                                              private ImageScaleFill  fill;
		[SerializeField]                                              private GameObject      lockObject;
		[FormerlySerializedAs( "selectableObject" )] [SerializeField] private HighlightObject highlightObject;

		public Action OnClick { get; set; }

		public void Initialize( Machine machine )
		{
			nameText.text = machine.MachineConfig.machineName;
			lockObject.SetActive( !machine.IsUnlocked );
			highlightObject.enabled = machine.IsUnlocked;
		}

		public void Unlock( )
		{
			lockObject.SetActive( false );
			highlightObject.enabled = true;
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