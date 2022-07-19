using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    private Player _player;
    private PlayerMovement _playerMovement;
    private PlayerJump _playerJump;

    public override void OnEnterState(PlayerStateManager stateManager)
    {
        if (_player == null)
        {
            _player = stateManager.Player;
            _playerMovement = _player.PlayerMov;
            _playerJump = _player.PlayerJump;
        }

        _playerJump.Jump();
    }

    public override void UpdateState(PlayerStateManager stateManager)
    {
        // como no puse que caiga de momento esta asi
        if (_player.IsFalling)
            stateManager.SwitchState(PlayerStateManager.PlayerState.Fall);
    }

    public override void FixedUpdateState(PlayerStateManager stateManager)
    {
        _playerMovement.ApplyMovement();
    }
}
