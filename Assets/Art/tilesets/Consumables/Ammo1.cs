using UnityEngine;

public class Ammo1 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            InventoryManager im = other.GetComponent<InventoryManager>();
            im.ReloadAmmo(5);
            Destroy(gameObject);
        }
    }
}