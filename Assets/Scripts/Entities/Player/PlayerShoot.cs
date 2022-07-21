using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private Player _player;
    private WeaponScriptable _weapon;

    private void Start()
    {
        _player = GetComponent<Player>();

        EventManager.Shoot += Shoot;
    }

    private void Shoot()
    {
        // weaponSelected
        _weapon = _player.SelectedWeapon;
        GameObject go = Instantiate(_weapon.AmmoType, _player.ShootPos.position, _player.FpCamera.rotation, null);

        if (go.TryGetComponent(out Bullets bullet))
        {
            bullet.SetData(_weapon.Damage, _weapon.AmmoSpeed);
            bullet.Shoot();
        }
    }

    private void OnDestroy()
    {
        EventManager.Shoot -= Shoot;
    }
}
