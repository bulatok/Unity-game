using UnityEngine;
using System.Collections;

public class DeathPoint : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("DeathPoint");
            PlayerController player = other.GetComponent<PlayerController>();
            player.Heal(-5000);
        }
    }
}