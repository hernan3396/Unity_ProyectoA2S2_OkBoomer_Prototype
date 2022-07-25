using UnityEngine;

public class PlayerFallState : PlayerBaseState
{
    private Player _player;
    private PlayerJump _playerJump;
    private PlayerMovement _playerMov;

    #region GravityChange
    private float _gravityTimer;
    private bool _changedGravity;
    #endregion

    public override void OnEnterState(PlayerStateManager stateManager)
    {
        if (_player == null)
        {
            _player = stateManager.Player;
            _playerJump = _player.PlayerJump;
            _playerMov = _player.PlayerMov;
        }

        if (_playerJump.IsJumping)
        {
            _changedGravity = true;
            _gravityTimer = 0;
            _player.ChangeGravity(true);
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

            _player.ChangeGravity(false); // por si no se reinicio antes la gravedad
        }

        // halves gravity at jump apex
        // slightly notable
        if (_changedGravity)
        {
            _gravityTimer += Time.deltaTime;

            if (_gravityTimer >= _player.Data.HalfGravityLimit)
            {
                _changedGravity = false;
                _player.ChangeGravity(false);
            }
        }

    }

    public override void FixedUpdateState(PlayerStateManager stateManager)
    {
        _playerMov.ApplyMovement();
    }
}
