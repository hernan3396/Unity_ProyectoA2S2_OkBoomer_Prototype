using UnityEngine;

public class Bullets : MonoBehaviour
{
    // usar esta clase como padre de las demas balas y
    // a los hijos añadirle interfaces ej: 
    // IExplotable, IBounceable

    // usar solo hitbox de caja asi usamos un pool solo
    // y editar el tamaño antes de dispararla
    // y el modelo

    [SerializeField, Range(0, 3)] private float _bulletDuration;
    private float _bulletTimer;
    private Rigidbody _rb;
    private int _damage;
    private int _speed;

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

    public virtual void Shoot()
    {
        _rb.AddForce(transform.forward * _speed, ForceMode.Impulse);
    }

    public void SetData(int damage, int speed)
    {
        _damage = damage;
        _speed = speed;
    }
}
