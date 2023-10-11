using System;
using UnityEngine;

public class EnemyAttackZone : MonoBehaviour
{

    [SerializeField] public Enemy enemy;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            enemy.Attack();
        }
    }
}