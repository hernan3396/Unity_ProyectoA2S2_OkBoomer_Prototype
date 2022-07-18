using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerJump : MonoBehaviour
{
    #region Components
    private PlayerScriptable _data;
    private Rigidbody _rb;
    #endregion

    private void Start()
    {
        Player player = GetComponent<Player>();

        _data = player.Data;
        _rb = player.RB;

        EventManager.Jump += Jump;
    }

    private void Jump()
    {
        _rb.AddForce(transform.up * _data.JumpStrength, ForceMode.Impulse);
    }

    private void OnDestroy()
    {
        EventManager.Jump -= Jump;
    }
}
