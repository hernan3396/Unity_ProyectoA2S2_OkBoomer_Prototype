using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerShoot : MonoBehaviour, IPausable
{
    [SerializeField] private PoolManager[] _bulletsPool;
    private WeaponScriptable _weapon;
    private Player _player;

    #region Shoot
    private bool _isShooting = false;
    private bool _canShoot = true;
    private float _shootTimer;
    private bool _weaponOnCD;
    #endregion

    private bool _isPaused = false;

    private void Start()
    {
        _player = GetComponent<Player>();

        EventManager.Shoot += ShootInput;
        EventManager.Pause += OnPause;
    }

    private void Update()
    {
        if (_isPaused) return;

        if (_weaponOnCD)
        {
            WeaponCooldown();
            return;
        }

        if (_isShooting && _canShoot)
            Shoot();
    }

    private void ShootInput(bool value)
    {
        _isShooting = value;
    }

    private void Shoot()
    {
        _canShoot = false;
        _weaponOnCD = true;

        // weaponSelected
        _weapon = _player.SelectedWeapon;

        GameObject newBullet = _bulletsPool[(int)_weapon.AmmoType.AmmoType].GetPooledObject();
        if (!newBullet) return;

        newBullet.transform.position = _player.ShootPos.position;
        newBullet.transform.rotation = _player.FpCamera.rotation;

        if (newBullet.TryGetComponent(out Bullets bullet))
        {
            bullet.SetData(_weapon.Damage, _weapon.AmmoSpeed, _weapon.AmmoType);
            newBullet.SetActive(true);
            bullet.Shoot(_weapon.Accuracy.x, _weapon.Accuracy.y);
        }
    }

    private void WeaponCooldown()
    {
        _shootTimer += Time.deltaTime;

        if (_shootTimer >= _weapon.Cooldown)
            ResetShoot();
    }

    private void ResetShoot()
    {
        _shootTimer = 0;
        _canShoot = true;
        _weaponOnCD = false;
    }

    public void OnPause(bool value)
    {
        _isShooting = false;
        _isPaused = value;
    }

    private void OnDestroy()
    {
        EventManager.Shoot -= ShootInput;
        EventManager.Pause -= OnPause;
    }
}
