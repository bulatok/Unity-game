using System;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public PlayerController.Inventory inventory;

    private void Start() {
        inventory = GetComponent<PlayerController>().inventory;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            inventory.activeSlot = Math.Min(inventory.weapons.Count, 1) - 1;
        } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            inventory.activeSlot = Math.Min(inventory.weapons.Count, 2) - 1;
        } else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            inventory.activeSlot = Math.Min(inventory.weapons.Count, 3) - 1;
        }
    }
}
