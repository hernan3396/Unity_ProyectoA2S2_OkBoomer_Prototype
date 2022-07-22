using UnityEngine;

public abstract class Bullets : MonoBehaviour, IShootable
{
    #region Components
    protected TrailRenderer _trailRenderer;
    protected MeshFilter _meshFilter;
    protected Transform _transform;
    protected BoxCollider _boxCol;
    protected Rigidbody _rb;
    #endregion

    #region Data
    protected BulletScriptable _data;
    protected float _bulletTimer;
    // estos ultimos 2 vienen del arma
    protected int _damage;
    protected int _speed;
    #endregion

    private void Awake()
    {
        // componentes de modelo
        _meshFilter = GetComponent<MeshFilter>();
        _boxCol = GetComponent<BoxCollider>();

        _trailRenderer = GetComponent<TrailRenderer>();
        _transform = GetComponent<Transform>();
        _rb = GetComponent<Rigidbody>();
    }

    public virtual void Shoot()
    {
        _rb.AddForce(transform.forward * _speed, ForceMode.Impulse);
    }

    public void SetData(int damage, int speed, BulletScriptable data)
    {
        _data = data;

        // ajustes visuales
        _meshFilter.mesh = data.Mesh;
        _transform.localScale = _data.Size;

        // ajustes de estadisticas
        _damage = damage;
        _speed = speed;
    }

    protected virtual void BulletLifetime()
    {
        _bulletTimer += Time.deltaTime;

        if (_bulletTimer >= _data.Duration)
            DisableBullet();
    }

    protected virtual void DisableBullet()
    {
        // resetea parametros
        _bulletTimer = 0;
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
        _trailRenderer.Clear();

        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.TryGetComponent(out Enemy enemy))
            enemy.TakeDamage(_damage);

        DisableBullet();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(_damage);
        }
    }
}
