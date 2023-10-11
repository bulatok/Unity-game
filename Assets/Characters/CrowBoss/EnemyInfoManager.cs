using System;
using UnityEngine;

public class EnemyInfoManager : MonoBehaviour
{
    private float _width;
    private float _height;
    private Vector2 _pos;

    private void Start()
    {
        _width = transform.localScale.x;
        _height = transform.localScale.y;
    }

    public void Change(float percentage)
    {
        Vector2 newPos = new Vector2(-percentage / 2 * _width, 0);
        transform.Translate(newPos - _pos);
        _pos = newPos;
        transform.localScale.Scale(new Vector2(1 - percentage, 0));
    }
    
}