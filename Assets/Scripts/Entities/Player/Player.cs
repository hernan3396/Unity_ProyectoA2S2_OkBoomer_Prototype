using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : Entity, IPausable
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
    private PlayerCrouch _playerCrouch;
    private PlayerJump _playerJump;
    #endregion

    #region BodyParts
    [Header("Body Parts")]
    [SerializeField] private Transform _fpCamera;
    [SerializeField] private Transform _body;
    #endregion

    #region Weapons
    [Header("Weapons")]
    // si va a variar en play time cambiar esto a una lista
    [SerializeField] private WeaponScriptable[] _weapons;
    [SerializeField] private Transform _shootPos;
    private int _currentWeapon = 0;
    private int _maxWeapons;
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
    [Header("Gravity")]
    [SerializeField] private float _gravityMod = 1;
    #endregion

    #region Pause
    private bool _isPaused = false;
    private Vector3 _lastVel;
    #endregion

    #region Hitboxes
    [SerializeField] private Transform[] _cameraPositions;
    [SerializeField] private GameObject[] _hitboxes;
    #endregion

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerCrouch = GetComponent<PlayerCrouch>();
        _playerJump = GetComponent<PlayerJump>();

        _maxWeapons = _weapons.Length;

        if (TryGetComponent(out Rigidbody rb))
            _rb = rb;

        // si tiene vida guardada en los
        // prefs NO ejecutar esto
        _currentHp = _data.MaxHealth;
        _invulnerability = _data.Invulnerability;
    }

    private void Start()
    {
        EventManager.GameStart += OnGameStart;
        EventManager.Pause += OnPause;
    }

    private void FixedUpdate()
    {
        if (_isPaused) return;

        _rb.AddForce(Physics.gravity * _data.Gravity * _gravityMod, ForceMode.Acceleration);

        // _rb.velocity = _utilsMov.LimitFallSpeed(_rb.velocity, _data.FallMaxSpeed);
        if (_rb.velocity.y < _data.FallMaxSpeed)
            _rb.velocity = new Vector3(_rb.velocity.x, _data.FallMaxSpeed, _rb.velocity.z);

        _isGrounded = GroundChecking();
    }

    private void OnGameStart()
    {
        EventManager.OnUpdateUI(UIManager.Element.Hp, _currentHp);
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

    #region WeaponMethods
    public void ChangeWeapons(int value)
    {
        _currentWeapon = value;
    }
    #endregion

    #region PauseMethods
    public void OnPause(bool value)
    {
        _isPaused = value;

        if (_isPaused)
            PausePlayer();
        else
            ResumePlayer();
    }

    private void PausePlayer()
    {
        _lastVel = _rb.velocity;
        _rb.velocity = Vector3.zero;
        _rb.useGravity = false;
    }

    private void ResumePlayer()
    {
        _rb.velocity = _lastVel;
        _rb.useGravity = true;
    }
    #endregion

    public override void TakeDamage(int value)
    {
        base.TakeDamage(value);
        EventManager.OnUpdateUI(UIManager.Element.Hp, _currentHp);
    }
    protected override void Death()
    {
        EventManager.OnGameOver();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawWireCube(transform.position + Vector3.down * _grdDist, transform.localScale);
    }

    private void OnDestroy()
    {
        EventManager.GameStart -= OnGameStart;
        EventManager.Pause -= OnPause;
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

    public PlayerCrouch PlayerCrouch
    {
        get { return _playerCrouch; }
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

    public Transform FpCamera
    {
        get { return _fpCamera; }
    }

    public Transform Body
    {
        get { return _body; }
    }

    public WeaponScriptable[] Weapons
    {
        get { return _weapons; }
    }

    public Transform ShootPos
    {
        get { return _shootPos; }
    }

    public int CurrentWeapon
    {
        get { return _currentWeapon; }
    }

    public int MaxWeapons
    {
        get { return _maxWeapons; }
    }

    public WeaponScriptable SelectedWeapon
    {
        get { return _weapons[_currentWeapon]; }
    }

    public bool Paused
    {
        get { return _isPaused; }
    }

    public GameObject[] Hitboxes
    {
        get { return _hitboxes; }
    }

    public Transform[] CameraPositions
    {
        get { return _cameraPositions; }
    }
    #endregion
}
