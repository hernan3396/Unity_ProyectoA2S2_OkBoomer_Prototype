using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerMovement : MonoBehaviour, IPausable
{
    #region Components
    private PlayerScriptable _data;
    private Transform _transform;
    private Player _player;
    private Rigidbody _rb;
    #endregion

    private Vector3 _dirInput;

    private void Start()
    {
        _player = GetComponent<Player>();

        _transform = _player.Transform;
        _data = _player.Data;
        _rb = _player.RB;

        EventManager.Move += ChangeDirection;
        EventManager.Pause += OnPause;
    }

    private void ChangeDirection(Vector2 move)
    {
        _dirInput = move;
    }

    public void ApplyMovement()
    {
        if (_player.Paused) return;
        Vector3 dir = (_transform.right * _dirInput.x + _transform.forward * _dirInput.y).normalized;
        Vector3 rbVelocity = dir * _data.Speed;

        rbVelocity.y = _rb.velocity.y; // mantenemos la velocidad en y que tenia el cuerpo
        _rb.velocity = rbVelocity;
    }

    public void OnPause(bool value)
    {
        if (value)
            _dirInput = Vector3.zero;
    }

    private void OnDestroy()
    {
        EventManager.Move -= ChangeDirection;
        EventManager.Pause -= OnPause;
    }

    public bool IsMoving
    {
        get { return _dirInput.magnitude > 0.01f; }
    }
}
