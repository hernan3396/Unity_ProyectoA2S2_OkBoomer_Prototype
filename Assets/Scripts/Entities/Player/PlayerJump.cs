using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerJump : MonoBehaviour
{
    #region Components
    private Player _player;
    private PlayerScriptable _data;
    private Transform _transform;
    private Rigidbody _rb;
    #endregion

    private bool _isJumping;
    private float _coyoteTimer;

    private void Start()
    {
        _player = GetComponent<Player>();

        _transform = _player.Transform;
        _data = _player.Data;
        _rb = _player.RB;

        EventManager.Jump += Jumping;
    }

    private void Update()
    {
        if (!_player.IsGrounded)
            _coyoteTimer += Time.deltaTime;
        else
            _coyoteTimer = 0;
    }

    private void Jumping(bool value)
    {
        if (_player.IsFalling)
        {
            _isJumping = value;
            return;
        }
        _isJumping = value && _coyoteTimer <= _data.CoyoteMaxTime;
    }

    public void Jump()
    {
        _rb.velocity = new Vector3(_rb.velocity.x, _data.JumpStrength, _rb.velocity.z);

        // a veces saltaba doble, el error esta en la razon por la cual
        // salta dos veces pero, por que pasa eso?
        // _rb.AddForce(_transform.up * _data.JumpStrength, ForceMode.Impulse);
    }

    public void VariableJump()
    {
        // esta se podria pasar a un metodo junto con la de Jump pero
        // para tan pocas lineas me parecio un poco innecesario
        _rb.velocity = new Vector3(_rb.velocity.x, _rb.velocity.y * 0.5f, _rb.velocity.z);
    }

    private void OnDestroy()
    {
        EventManager.Jump -= Jumping;
    }

    public bool IsJumping
    {
        get { return _isJumping; }
    }
}
