using ScriptableObjects;
using UnityEngine;

public class WeaponInventory : MonoBehaviour
{
    public InventorySlot[] slots;
}

[System.Serializable]
public class InventorySlot
{
    public int id;
    public WeaponDataWrapper data;
    public int currentAmmoInclip; //Data gameplay
    public int ammo;
}
