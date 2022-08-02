using UnityEngine;
using TMPro;

[RequireComponent(typeof(Player))]
public class PlayerChargeShoot : MonoBehaviour, IPausable
{
    private enum States
    {
        Idle,
        Charging,
        Shooting,
        Cooldown
    }

    #region Components
    [SerializeField] private TMP_Text _stateText;
    private WeaponScriptable _weapon;
    private Player _player;
    #endregion

    #region Shooting
    private States _currentState = States.Idle;
    private bool _isShooting = false;
    private float _chargingTime;
    private float _shootingTime;
    private float _cooldown;
    #endregion

    // [SerializeField] private PoolManager[] _bulletsPool;
    private bool _isPaused = false;


    private void Start()
    {
        _player = GetComponent<Player>();

        EventManager.SpecialShoot += SpecialShootInput;

#if UNITY_EDITOR
        _stateText.gameObject.SetActive(true);
#endif
    }

    private void Update()
    {
        if (_isPaused) return;

        switch (_currentState)
        {
            case States.Idle:
                Idle();
                break;

            case States.Charging:
                Charging();
                break;

            case States.Shooting:
                Shooting();
                break;

            case States.Cooldown:
                Cooldown();
                break;

            default:
                Idle();
                break;
        }
    }

    private void SpecialShootInput(bool value)
    {
        _isShooting = value;
    }

    private void Idle()
    {
        if (_isShooting)
        {
            _weapon = _player.SelectedWeapon;

            _chargingTime = 0;
            _shootingTime = 0;
            _cooldown = 0;

            ChangeState(States.Charging);
        }
    }

    private void Charging()
    {
        _chargingTime += Time.deltaTime;

        if (!_isShooting || ChangedWeapon())
        {
            ChangeState(States.Cooldown);
            return;
        }

        if (_chargingTime >= _weapon.SpecialStartup)
        {
            ChangeState(States.Shooting);
            return;
        }
    }

    private void Shooting()
    {
        _shootingTime += Time.deltaTime;

        if (!_isShooting || ChangedWeapon())
        {
            ChangeState(States.Cooldown);
            return;
        }

        if (_shootingTime >= _weapon.SpecialTime)
        {
            ChangeState(States.Cooldown);
            return;
        }
    }

    private void Cooldown()
    {
        _cooldown += Time.deltaTime;

        if (_cooldown >= _weapon.SpecialCooldown)
            ChangeState(States.Idle);
    }

    private bool ChangedWeapon()
    {
        return _weapon != _player.SelectedWeapon;
    }

    private void ChangeState(States newState)
    {
        _currentState = newState;
        _stateText.text = "State: " + _currentState.ToString();
    }

    public void OnPause(bool value)
    {
        _isPaused = value;
    }

    private void OnDestroy()
    {
        EventManager.SpecialShoot -= SpecialShootInput;
    }
}
