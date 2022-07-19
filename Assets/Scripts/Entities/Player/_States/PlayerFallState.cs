using UnityEngine;

public class PlayerFallState : PlayerBaseState
{
    private Player _player;
    private PlayerMovement _playerMov;

    public override void OnEnterState(PlayerStateManager stateManager)
    {
        if (_player == null)
        {
            _player = stateManager.Player;
            _playerMov = _player.PlayerMov;
        }
    }

    public override void UpdateState(PlayerStateManager stateManager)
    {
        if (_player.IsGrounded)
        {
            if (!_playerMov.IsMoving)
                stateManager.SwitchState(PlayerStateManager.PlayerState.Idle);
            else
                stateManager.SwitchState(PlayerStateManager.PlayerState.Run);
        }
    }

    public override void FixedUpdateState(PlayerStateManager stateManager)
    {
        _playerMov.ApplyMovement();
    }
}
