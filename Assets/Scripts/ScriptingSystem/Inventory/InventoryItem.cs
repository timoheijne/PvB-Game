using UnityEngine;

[CreateAssetMenu(fileName = "INVENTORYITEM", menuName = "Objects/Inventory Item", order = 0)]
public class InventoryItem : ScriptableObject
{
    public string ItemName;
    public GameObject Prefab;
}