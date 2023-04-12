
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private float walkspeed;
    private Rigidbody2D rb;
    private bool _icanmove;
    private Vector2 _direction;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        Move();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            _direction = RandomDirection();
            _icanmove = true;
           
        }
    }

    private Vector2 RandomDirection()
    {
        float xdir;
        xdir = Random.Range(-10, 10);
        float ydir;
        ydir = Random.Range(-10, 10);
        Vector2 dir = new Vector2(xdir, ydir);
        return dir;
    }

    private void Move()
    {
        if (_icanmove)
        {
            Debug.Log(_direction);
            rb.velocity = (_direction * Time.deltaTime * walkspeed);
        }
    }

}
