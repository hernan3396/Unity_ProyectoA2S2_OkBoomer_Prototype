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
        _rb.AddForce(Physics.gravity * _data.Gravity, ForceMode.Acceleration);
    }

    protected override void Death()
    {
        throw new System.NotImplementedException();
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
    #endregion
}
