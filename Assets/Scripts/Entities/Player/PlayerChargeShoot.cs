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

    #region VFX
    [Header("VFX")]
    // estan para el prototipo, luego hay que mejorar esto
    [SerializeField] private GameObject _chargingParticles;
    [SerializeField] private GameObject _cdParticles;
    [SerializeField] private GameObject _laser;
    [SerializeField] private LineRenderer _laserLR;
    #endregion

    #region Components
    [Header("Components")]
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

            _chargingParticles.SetActive(true);
            ChangeState(States.Charging);
        }
    }

    private void Charging()
    {
        _chargingTime += Time.deltaTime;

        if (!_isShooting || ChangedWeapon())
        {
            _chargingParticles.SetActive(false);
            _cdParticles.SetActive(true);
            ChangeState(States.Cooldown);
            return;
        }

        if (_chargingTime >= _weapon.SpecialStartup)
        {
            _chargingParticles.SetActive(false);
            _laser.SetActive(true);
            ChangeState(States.Shooting);
            return;
        }
    }

    private void Shooting()
    {
        TempShoot();
        _shootingTime += Time.deltaTime;

        if (!_isShooting || ChangedWeapon())
        {
            _laser.SetActive(false);
            _cdParticles.SetActive(true);
            ChangeState(States.Cooldown);
            return;
        }

        if (_shootingTime >= _weapon.SpecialTime)
        {
            _laser.SetActive(false);
            _cdParticles.SetActive(true);
            ChangeState(States.Cooldown);
            return;
        }
    }

    private void Cooldown()
    {
        _cooldown += Time.deltaTime;

        if (_cooldown >= _weapon.SpecialCooldown)
        {
            _cdParticles.SetActive(false);
            ChangeState(States.Idle);
        }
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

    private void TempShoot()
    {
        // un disparo temporal para que al menos haga daño
        // tambien hay unas cuentas medias raras porque sino el vfx se veia raro
        // esto obviamente queda cambiado luego, pero para ahora es mas que suficiente
        if (Physics.Raycast(_laser.transform.position + _laser.transform.forward, _laser.transform.forward, out RaycastHit hit, Mathf.Infinity))
        {
            _laserLR.SetPosition(0, _laser.transform.position);
            _laserLR.SetPosition(1, hit.point);

            if (hit.collider.TryGetComponent(out Enemy enemy))
                enemy.TakeDamage(2, hit.transform);
        }
        else
        {
            _laserLR.SetPosition(0, _laser.transform.position);
            _laserLR.SetPosition(1, _laser.transform.position + _laser.transform.forward * 20);
        }
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(_laser.transform.position + _laser.transform.forward, _laser.transform.forward * 10, Color.green);
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
