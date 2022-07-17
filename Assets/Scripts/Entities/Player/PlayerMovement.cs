using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerMovement : MonoBehaviour
{
    #region Components
    private PlayerScriptable _data;
    private Transform _transform;
    private Rigidbody _rb;
    #endregion

    private void Start()
    {
        Player player = GetComponent<Player>();

        _transform = player.Transform;
        _data = player.Data;
        _rb = player.RB;

        EventManager.Move += Moving;
    }

    private void Moving(Vector2 move)
    {
        Vector3 moveDirection = _transform.right * move.x + _transform.forward * move.y;
        Vector3 rbVelocity = moveDirection.normalized * _data.Speed;

        rbVelocity.y = _rb.velocity.y; // mantenemos la velocidad en y que tenia el cuerpo
        _rb.velocity = rbVelocity;
    }

    private void OnDestroy()
    {
        EventManager.Move -= Moving;
    }
}
