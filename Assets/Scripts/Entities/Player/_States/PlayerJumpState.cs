using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    private Player _player;
    private PlayerMovement _playerMovement;
    private PlayerJump _playerJump;
    private CapsuleCollider _collider;

    public override void OnEnterState(PlayerStateManager stateManager)
    {
        if (_player == null)
        {
            _player = stateManager.Player;
            _playerMovement = _player.PlayerMov;
            _playerJump = _player.PlayerJump;
            _collider = _player.Hitboxes[0].GetComponent<CapsuleCollider>();
        }

        _playerJump.Jump();
        _collider.material = _player.NoFrictionMat;
    }

    public override void UpdateState(PlayerStateManager stateManager)
    {
        // como no puse que caiga de momento esta asi
        if (_player.IsFalling)
            stateManager.SwitchState(PlayerStateManager.PlayerState.Fall);

        if (!_playerJump.IsJumping)
            _playerJump.VariableJump();
    }

    public override void FixedUpdateState(PlayerStateManager stateManager)
    {
        _playerMovement.ApplyMovement();
    }

    public override void OnExitState(PlayerStateManager stateManager)
    {
        _collider.material = null;
    }
}
