using System.Collections.Generic;
using AncientForge.Inventory;
using UnityEngine;

namespace AncientForge.Recipes
{
	[CreateAssetMenu( fileName = "New_Recipe", menuName = "Config/Recipe", order = 0 )]
	public class RecipeConfig : ScriptableObject
	{
		public List<InventoryItemConfig> ingredients;
		public InventoryItemConfig       product;
		public float                     duration;
		public float                     successChance;
	}
}