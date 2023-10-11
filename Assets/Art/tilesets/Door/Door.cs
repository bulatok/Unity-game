using System;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] public GameObject go;
    void Open()
    {
        go.SetActive(true);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Open();
    }
}
