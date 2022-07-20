using UnityEngine;

public class Bullets : MonoBehaviour
{
    [SerializeField, Range(0, 3)] private float _bulletDuration;
    private float _bulletTimer;
    private Rigidbody _rb;
    private int _damage;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _bulletTimer += Time.deltaTime;

        if (_bulletTimer >= _bulletDuration)
            Destroy(gameObject);
    }

    public void ShootBullet(int damage, int speed, Vector3 dir)
    {
        _damage = damage;
        _rb.AddForce(dir.normalized * speed, ForceMode.Impulse);
    }
}
