using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Transform _parenttransform;

    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(transform.up * _speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }


}
