using UnityEngine;
using System.Collections;

public class ElectroFloor : MonoBehaviour
{
    public float activeTime = 1f; // Time the trap is active (in seconds)
    public float inactiveTime = 2f; // Time the trap is inactive (in seconds)

    public float playerDamage = 2f;

    private bool isActive = false;
    private SpriteRenderer spriteRenderer;
    
    public AudioSource floorSound;

    void Start()
    {
        floorSound = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(ElectroTrap());
    }

    IEnumerator ElectroTrap()
    {
        while (true)
        {
            yield return new WaitForSeconds(isActive ? activeTime : inactiveTime);
            isActive = !isActive;
            spriteRenderer.enabled = isActive;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isActive && other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                if (playerDamage > 0)
                {
                    playerDamage *= -1;
                }

                player.Heal(playerDamage);
                floorSound.Play(0);
            }
        }
    }
}