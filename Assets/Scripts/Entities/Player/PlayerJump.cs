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
    [SerializeField] private float _coyoteTimer;
    [SerializeField] private int _bufferedJumps;

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
        // if (value)
        // {
        //     _bufferedJumps += 1;
        //     Invoke("RemoveBufferedJump", _data.JumpBufferTime);
        // }

        // _isJumping = (value && _coyoteTimer <= _data.CoyoteMaxTime) || _bufferedJumps > 0;
        _isJumping = (value && _coyoteTimer <= _data.CoyoteMaxTime);
    }

    public void Jump()
    {
        _rb.velocity = new Vector3(_rb.velocity.x, _data.JumpStrength, _rb.velocity.z);

        // una vez que saltes pone el buffer en 0
        // CancelInvoke();
        // _bufferedJumps = 0;
        // a veces saltaba doble, el error esta en la razon por la cual
        // salta dos veces pero, por que pasa eso?
        // _rb.AddForce(_transform.up * _data.JumpStrength, ForceMode.Impulse);
    }

    public void VariableJump()
    {
        _rb.velocity = new Vector3(_rb.velocity.x, _rb.velocity.y * 0.5f, _rb.velocity.z);
    }

    private void RemoveBufferedJump()
    {
        _bufferedJumps -= 1;
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
