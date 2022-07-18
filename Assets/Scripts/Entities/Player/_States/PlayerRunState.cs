using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    private PlayerMovement _playerMovement;
    private PlayerJump _playerJump;

    public override void OnEnterState(PlayerStateManager stateManager)
    {
        if (_playerMovement == null)
            _playerMovement = stateManager.Player.PlayerMov;

        if (_playerJump == null)
            _playerJump = stateManager.Player.PlayerJump;
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
    }

    public override void FixedUpdateState(PlayerStateManager stateManager)
    {
        _playerMovement.ApplyMovement();
    }
}
