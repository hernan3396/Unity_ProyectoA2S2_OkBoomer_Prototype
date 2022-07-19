using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : Entity
{
    /*
    De momento (que esta medio simple) dejar 
    los scripts separados, si se vuelve una 
    ensalada juntarlos todos aca
    */
    #region Components
    [Header("Components")]
    [SerializeField] private PlayerScriptable _data;
    private Rigidbody _rb;
    #endregion

    #region Scripts
    private PlayerMovement _playerMovement;
    private PlayerJump _playerJump;
    #endregion

    #region GroundChecking
    [Header("Ground Checking")]
    [SerializeField, Range(0, 1)] private float _grdDist;
    [SerializeField] private LayerMask _grdLayer;
    [SerializeField] private bool _isGrounded;
    bool _hitDetect;
    RaycastHit _hit;
    #endregion

    #region Gravity
    [SerializeField] private float _gravityMod = 1;
    #endregion

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerJump = GetComponent<PlayerJump>();

        if (TryGetComponent(out Rigidbody rb))
            _rb = rb;

        // si tiene vida guardada en los
        // prefs NO ejecutar esto
        _currentHp = _data.MaxHealth;
        _invulnerability = _data.Invulnerability;
    }

    private void FixedUpdate()
    {
        _rb.AddForce(Physics.gravity * _data.Gravity * _gravityMod, ForceMode.Acceleration);

        _isGrounded = GroundChecking();
    }

    #region GroundCheckingMethods
    private bool GroundChecking()
    {
        _hitDetect = Physics.BoxCast(_transform.position, _transform.localScale, Vector3.down, out _hit, _transform.rotation, _grdDist, _grdLayer);

        if (_hitDetect)
            return true;

        return false;
    }
    #endregion

    #region GravityMethods
    /// <Summary>
    ///  if true halves gravity, false returns it
    /// </Summary>
    public void ChangeGravity(bool change)
    {
        // estos valores supongo se pueden hacer configurables
        if (change)
            _gravityMod = 0.5f;
        else
            _gravityMod = 1;
    }
    #endregion

    protected override void Death()
    {
        throw new System.NotImplementedException();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawWireCube(transform.position + Vector3.down * _grdDist, transform.localScale);
    }

    #region Getter/Setters
    public PlayerScriptable Data
    {
        get { return _data; }
    }

    public PlayerMovement PlayerMov
    {
        get { return _playerMovement; }
    }

    public PlayerJump PlayerJump
    {
        get { return _playerJump; }
    }

    public Transform Transform
    {
        get { return _transform; }
    }

    public Rigidbody RB
    {
        get { return _rb; }
    }

    public bool IsFalling
    {
        get { return _rb.velocity.y < 0; }
    }

    public bool IsGrounded
    {
        get { return _isGrounded; }
    }
    #endregion
}
