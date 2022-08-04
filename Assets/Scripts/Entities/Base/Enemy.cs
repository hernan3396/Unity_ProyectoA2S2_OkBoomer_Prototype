using UnityEngine;
using DG.Tweening;

public abstract class Enemy : Entity, IDamagable, IPausable
{
    #region Components
    [SerializeField] protected PoolManager _bulletsPool;
    [SerializeField] protected PoolManager _bloodPool;
    [SerializeField] protected EnemyScriptable _data;
    private BoxCollider _boxCol;
    private Material _mainMat;
    private Rigidbody _rb;
    #endregion

    #region Pause
    protected bool _isPaused = false;
    protected bool _isDead = false;
    private Vector3 _lastVel;
    #endregion

    private void Awake()
    {
        _mainMat = GetComponent<MeshRenderer>().materials[0];
        _transform = GetComponent<Transform>();
        _boxCol = GetComponent<BoxCollider>();
        _rb = GetComponent<Rigidbody>();

        _currentHp = _data.MaxHealth;
    }

    public void TakeDamage(int value, Transform bullet)
    {
        if (_isInmune) return;
        if (_isDead) return;

        GameObject blood = _bloodPool.GetPooledObject();
        if (!blood) return;

        blood.transform.position = bullet.position;
        blood.transform.forward = bullet.forward;

        blood.SetActive(true);
        // en el codigo de las particulas de la sangre
        // ya esta puesto play on awake y disable en stop action

        TakeDamage(value);
    }

    #region Pause
    public void OnPause(bool value)
    {
        _isPaused = value;

        if (_isPaused)
            PauseEnemy();
        else
            ResumeEnemy();
    }

    protected virtual void PauseEnemy()
    {
        _lastVel = _rb.velocity;
        _rb.velocity = Vector3.zero;
        _rb.useGravity = false;
    }
    protected virtual void ResumeEnemy()
    {
        _rb.velocity = _lastVel;
        _rb.useGravity = true;
    }
    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _data.VisionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _data.AttackRange);
    }

    protected override void Death()
    {
        _isDead = true;
        _boxCol.enabled = false;

        _mainMat.DOFloat(1, "_DissolveValue", _data.DeathDur)
        .SetEase(Ease.OutQuint)
        .OnComplete(() => gameObject.SetActive(false));
    }
}
