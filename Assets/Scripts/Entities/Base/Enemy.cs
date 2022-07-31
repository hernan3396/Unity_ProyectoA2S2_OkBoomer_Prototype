using UnityEngine;
using DG.Tweening;

public abstract class Enemy : Entity, IDamagable, IPausable
{
    #region Components
    [SerializeField] protected PoolManager _bulletsPool;
    [SerializeField] protected EnemyScriptable _data;
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
        _rb = GetComponent<Rigidbody>();
        _mainMat = GetComponent<MeshRenderer>().materials[0];

        _currentHp = _data.MaxHealth;
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
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _data.VisionRange);
    }

    protected override void Death()
    {
        _isDead = true;

        _mainMat.DOFloat(1, "_DissolveValue", _data.DeathDur)
        .SetEase(Ease.OutQuint)
        .OnComplete(() => gameObject.SetActive(false));
    }
}
