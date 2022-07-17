using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : Entity
{
    #region Components
    [SerializeField] private PlayerScriptable _data;
    [SerializeField] private InputManager _input;
    private Rigidbody _rb;
    #endregion

    private void Awake()
    {
        _transform = GetComponent<Transform>();

        if (TryGetComponent(out Rigidbody rb))
            _rb = rb;

        // si tiene vida guardada en los
        // prefs NO ejecutar esto
        _currentHp = _data.MaxHealth;
        _invulnerability = _data.Invulnerability;
    }

    private void FixedUpdate()
    {
        Vector3 moveDirection = _transform.right * _input.Move.x + _transform.forward * _input.Move.y;
        Vector3 rbVelocity = moveDirection.normalized * _data.Speed;

        rbVelocity.y = _rb.velocity.y; // mantenemos la velocidad en y que tenia el cuerpo
        _rb.velocity = rbVelocity;
    }

    protected override void Death()
    {
        throw new System.NotImplementedException();
    }
}
