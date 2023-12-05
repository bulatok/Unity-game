using UnityEngine;

public class Ammo1 : MonoBehaviour
{
	public int bulletsNum = 5;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            InventoryManager im = other.GetComponent<InventoryManager>();
            im.ReloadAmmo(bulletsNum);
            Destroy(gameObject);
        }
    }
}