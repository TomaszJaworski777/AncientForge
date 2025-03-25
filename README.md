**Starting the Project:**
* **Unity Version:** *6000.0.43f1*
* **Scene:** *Gameplay*

</br>

**UI Layout:**
* Player Inventory in the bottom-left corner
* Machine List in the bottom-right corner
* Quests in the top-right corner
* Bonus List in the top-left corner
  
</br>

# Architectural Decisions
1. **All-UI Approach**
   * I built the entire game using Unity UI elements. This makes things like using the Particle System more challenging, but keeps the project consistent and simpler to maintain in terms of UI scaling.
2. **Dynamic UI for Scalability**
   * Because the project could grow quite large, I used a lot of dynamic UI elements (e.g., Player Inventory, Machine List, Quest List, etc.). This way, the interface can adapt more easily if we add new features or change existing ones.
3. **Data-Driven Design**
   * I utilized **ScriptableObjects** heavily and wrapped most core logic inside pure C# classes.
   * For example, the player inventory has:
       * A **ScriptableObject** that holds base definition of item.
       * A **C# class** (InventoryItemStack) that contains group of items, and simple logic to manipulate this data.
       * A **C# class** (InventoryContent) that contains all the logic (adding/removing items, tracking changes).
       * A **MonoBehaviour** (InventoryDisplay) that controls entirety of displaying information onto the screen.
       * A **MonoBehaviour** (InventoryBase) that references InventoryContent C# class, acting as the bridge between the data/logic and the actual UI.
4. **Encapsulated Inventory Items**
   * Instead of using a single ScriptableObject for each item, I chose to encapsulate it, to allow each item to store unique variables.
   * This makes it easy to store different stats or states per item instance (e.g., a piece of coal could eventually have multiple uses or varying qualities).
5. **Passing Classes in UI Updates**
   * In the UI update methods, I decided to pass the relevant classes every time instead of caching them once during initialization.
   * This makes the UI more flexible: if the core data structures change or rebuild themselves, the UI doesn’t have to know any internal details— it just grabs whatever data is currently valid.
6. **DisplayEvents in the InventoryDisplay**
   * I created an object called `DisplayEvents` to centralize UI events.
   * When I need a new UI event, I add a public property in `DisplayEvents` rather than scattering event references all over the code. This keeps everything organized and easy to expand.
7. **Player Class**
   * The Player class became tricky, since it’s the meeting point for Inventory, Machines, and the Quest system. I decided to use event wrappers so I don't have to cross-reference these systems directly—it’s too easy to end up with messy, tangled code.
8. **Bonus Items**
   * The requirement was to ensure bonus item effects don’t stack when they’re truly identical. I accomplished this by preventing stacking for multiple copies of the same item’s effect. However, if we introduce a different item that provides a similar effect, those effects do stack. I went with this approach to keep things flexible for future expansions.
9. **Multi-Objective Systems**
   * Both Quests and Machines support multiple objectives (e.g., a machine can require several completed quests to unlock, or a quest might need multiple item types).
   * Recipes can also require multiple copies of the same ingredient (e.g., 3x gold ore → ember dust).
   * I didn’t have time to add multi-product recipes, but the structure is set up so it would be straightforward to implement.
10. **Prefab Variants**
    * For repetitive objects like machine UIs or machine buttons, I used prefab variants.
    * Even though there’s currently only one machine button, it’s easy to create multiple variants with unique appearances or behaviors. You simply pass the relevant prefab to the machine’s ScriptableObject and it handles the rest.

***

I had only 48 hours to work on this. There was more I wanted to do, like a recipe book or a shop system for buying raw materials and selling processed items. That would solve the problem of running out of materials and having to restart. Still, I had a lot of fun building this.
