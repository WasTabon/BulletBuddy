using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
     [SerializeField] private float _moveSpeed;

     private readonly int _multiplier = 100; // шоб не писати 500 в інспекторі, а 5
     
     private Rigidbody2D _rigidbody2D;

     private void Start()
     {
          _rigidbody2D = GetComponent<Rigidbody2D>();
     }

     private void Update()
     {
          Move();
     }

     private void Move()
     {
          Vector2 dir = new Vector2(XInput(), YInput());
          _rigidbody2D.velocity = dir * (_moveSpeed * Time.deltaTime * _multiplier);
     }

     private float XInput()
     {
          return Input.GetAxis("Horizontal");
     }

     private float YInput()
     {
          return Input.GetAxis("Vertical");
     }

}
