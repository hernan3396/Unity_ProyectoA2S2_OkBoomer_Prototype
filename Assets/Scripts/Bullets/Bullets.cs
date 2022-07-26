using UnityEngine;

public abstract class Bullets : MonoBehaviour, IShootable, IPausable
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

    #region Pause
    protected Vector3 _lastAngVel; // algunas tienen velocidad angular
    protected Vector3 _lastVel;
    protected bool _isPaused;
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

    private void Start()
    {
        EventManager.Pause += OnPause;
    }

    public virtual void Shoot()
    {
        _rb.AddForce(transform.forward * _speed, ForceMode.Impulse);
    }

    public void SetData(int damage, int speed, BulletScriptable data)
    {
        _data = data;

        // ajustes visuales
        // _meshFilter.mesh = data.Mesh;
        // _transform.localScale = _data.Size;

        // ajustes de estadisticas
        _damage = damage;
        _speed = speed;
    }

    protected virtual void BulletLifetime()
    {
        if (_isPaused) return;
        _bulletTimer += Time.deltaTime;

        if (_bulletTimer >= _data.Duration)
            DisableBullet();
    }

    protected abstract void OnHit(Collision other);

    public void OnPause(bool value)
    {
        _isPaused = value;

        if (_isPaused)
            PauseBullet();
        else
            ResumeBullet();
    }

    private void PauseBullet()
    {
        _lastAngVel = _rb.angularVelocity;
        _lastVel = _rb.velocity;
        _rb.velocity = Vector3.zero;
        _rb.useGravity = false;
    }

    private void ResumeBullet()
    {
        _rb.angularVelocity = _lastAngVel;
        _rb.velocity = _lastVel;
        _rb.useGravity = true;
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
        OnHit(other);
    }

    private void OnDestroy()
    {
        EventManager.Pause -= OnPause;
    }
}
