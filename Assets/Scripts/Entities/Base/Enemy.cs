using UnityEngine;

public abstract class Enemy : Entity, IDamagable, IPausable
{
    #region Components
    [SerializeField] private EnemyScriptable _data;
    private Rigidbody _rb;
    #endregion

    #region Pause
    private bool _isPaused = false;
    private Vector3 _lastVel;
    #endregion

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    #region Pause
    public void OnPause(bool value)
    {
        _isPaused = value;
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
        throw new System.NotImplementedException();
    }
}
