using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    private PlayerMovement _playerMovement;
    private PlayerCrouch _playerCrouch;
    private PlayerJump _playerJump;

    public override void OnEnterState(PlayerStateManager stateManager)
    {
        if (_playerMovement == null)
        {
            _playerCrouch = stateManager.Player.PlayerCrouch;
            _playerMovement = stateManager.Player.PlayerMov;
            _playerJump = stateManager.Player.PlayerJump;
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

        if (_playerMovement.IsMoving && _playerCrouch.Crouching)
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
