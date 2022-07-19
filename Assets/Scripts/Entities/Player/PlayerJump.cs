using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerJump : MonoBehaviour
{
    #region Components
    private PlayerScriptable _data;
    private Transform _transform;
    private Rigidbody _rb;
    #endregion
    private bool _isJumping;

    private void Start()
    {
        Player player = GetComponent<Player>();

        _transform = player.Transform;
        _data = player.Data;
        _rb = player.RB;

        EventManager.Jump += Jumping;
    }

    private void Jumping(bool value)
    {
        _isJumping = value;
    }

    public void Jump()
    {
        _rb.velocity = new Vector3(_rb.velocity.x, _data.JumpStrength, _rb.velocity.z);
        // a veces saltaba doble, el error esta en la razon por la cual
        // salta dos veces pero, por que pasa eso?
        // _rb.AddForce(_transform.up * _data.JumpStrength, ForceMode.Impulse);
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
