using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerMovement : MonoBehaviour
{
    #region Components
    private PlayerScriptable _data;
    private Transform _transform;
    private Rigidbody _rb;
    #endregion

    private Vector3 _dirInput;

    private void Start()
    {
        Player player = GetComponent<Player>();

        _transform = player.Transform;
        _data = player.Data;
        _rb = player.RB;

        EventManager.Move += ChangeDirection;
    }

    private void ChangeDirection(Vector2 move)
    {
        _dirInput = move;
    }

    public void ApplyMovement()
    {
        Vector3 dir = (_transform.right * _dirInput.x + _transform.forward * _dirInput.y).normalized;
        Vector3 rbVelocity = dir * _data.Speed;

        rbVelocity.y = _rb.velocity.y; // mantenemos la velocidad en y que tenia el cuerpo
        _rb.velocity = rbVelocity;
    }

    private void OnDestroy()
    {
        EventManager.Move -= ChangeDirection;
    }

    public bool IsMoving
    {
        get { return _dirInput.magnitude > 0.01f; }
    }
}
