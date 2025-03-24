using System.Collections.Generic;
using UnityEngine;

namespace AncientForge.Machines
{
	[CreateAssetMenu( fileName = "New_MachineListConfig", menuName = "Config/Machines/MachineList", order = 0 )]
	public class MachineListConfig : ScriptableObject
	{
		public List<MachineConfig> machines;
	}
}