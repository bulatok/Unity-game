using UnityEngine;

public class Ammo1 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            player.ReloadAmmo(5);
            Destroy(gameObject);
        }
    }
}