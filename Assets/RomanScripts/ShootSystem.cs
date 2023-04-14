using UnityEngine;

public class ShootSystem : MonoBehaviour
{
    private Transform _shootpos;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _firedelay = 0.5f;
    private float _nextshot = 0;

    private void Start()
    {
        _shootpos=GetComponent<Transform>();
    }

    private void Update()
    {
        Shoot();
    }

    private void Shoot()
    {
        if (Input.GetButton("Fire1") && Time.time > _nextshot)
        {
            Instantiate(_bullet, _shootpos.position, transform.rotation);
            _nextshot = Time.time + _firedelay;
        }
    }

  

}
