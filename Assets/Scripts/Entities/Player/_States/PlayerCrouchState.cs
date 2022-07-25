using UnityEngine;

public class PlayerCrouchState : PlayerBaseState
{
    private PlayerMovement _playerMovement;
    private PlayerCrouch _playerCrouch;
    private Player _player;
    private bool _crouching;
    private float _crouchTimer;

    public override void OnEnterState(PlayerStateManager stateManager)
    {
        if (_player == null)
        {
            _player = stateManager.Player;
            _playerCrouch = _player.PlayerCrouch;
            _playerMovement = _player.PlayerMov;
        }

        _crouching = true;
        _playerCrouch.Crouch();
    }

    public override void FixedUpdateState(PlayerStateManager stateManager)
    {
        return;
    }

    public override void UpdateState(PlayerStateManager stateManager)
    {
        if (_player.Paused) return;

        if (_crouching)
        {
            _crouchTimer += Time.deltaTime;

            if (_crouchTimer >= _player.Data.CrouchTimer)
                stateManager.SwitchState(PlayerStateManager.PlayerState.Run);
        }
    }

    public override void OnExitState(PlayerStateManager stateManager)
    {
        _crouchTimer = 0;
        _crouching = false;
        _playerCrouch.EndCrouch();
    }
}
