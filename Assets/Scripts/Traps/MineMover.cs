using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineMover : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnMouseDrag()
    {
        Move();
    }

    private void Move()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        
        _rigidbody2D.MovePosition(objPosition);
    }
}
