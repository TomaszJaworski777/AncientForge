using UnityEngine;

namespace AncientForge.Machines
{
	[RequireComponent(typeof(MachineDisplay))]
	public class MachineManager : MonoBehaviour
	{
		[SerializeField] private MachineListConfig machineList;
	}
}