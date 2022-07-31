using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    private Player _player;
    private PlayerMovement _playerMovement;
    private PlayerCrouch _playerCrouch;
    private PlayerJump _playerJump;

    public override void OnEnterState(PlayerStateManager stateManager)
    {
        if (_player == null)
        {
            _player = stateManager.Player;
            _playerCrouch = _player.PlayerCrouch;
            _playerMovement = _player.PlayerMov;
            _playerJump = _player.PlayerJump;
        }
    }

    public override void UpdateState(PlayerStateManager stateManager)
    {
        if (!_playerMovement.IsMoving)
        {
            stateManager.SwitchState(PlayerStateManager.PlayerState.Idle);
            return;
        }

        if (_playerJump.IsJumping)
        {
            stateManager.SwitchState(PlayerStateManager.PlayerState.Jump);
            return;
        }

        if (_player.RB.velocity.magnitude > 1 && _playerCrouch.Crouching)
        {
            stateManager.SwitchState(PlayerStateManager.PlayerState.Crouch);
            return;
        }
    }

    public override void FixedUpdateState(PlayerStateManager stateManager)
    {
        _playerMovement.ApplyMovement();
    }
}
