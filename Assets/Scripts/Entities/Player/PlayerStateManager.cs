using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStateManager : MonoBehaviour
{
    public enum PlayerState
    {
        Idle,
        Run,
        Jump,
        Fall
    }

    [SerializeField] private PlayerBaseState _currentState;
    [SerializeField] private List<PlayerBaseState> _states;
    [SerializeField] private TMP_Text _stateText;

    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    public virtual void Start()
    {
        _currentState = _states[(int)PlayerState.Idle];
        _currentState.OnEnterState(this);
    }

    public virtual void Update()
    {
        _currentState.UpdateState(this);
    }

    public virtual void FixedUpdate()
    {
        _currentState.FixedUpdateState(this);
    }

    public virtual void SwitchState(PlayerState state)
    {
        _currentState?.OnExitState(this);

        _stateText.text = "State: " + state.ToString();

        _currentState = _states[(int)state];
        _currentState.OnEnterState(this);
    }

    public Player Player
    {
        get { return _player; }
    }
}
