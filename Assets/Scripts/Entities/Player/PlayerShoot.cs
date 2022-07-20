using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private Player _player;
    private WeaponScriptable[] _weapons;

    private void Start()
    {
        _player = GetComponent<Player>();
        _weapons = _player.Weapons;

        EventManager.Shoot += Shoot;
    }

    private void Shoot()
    {
        // ese 0 pasarle el del arma seleccionada luego
        WeaponScriptable weapon = _weapons[_player.CurrentWeapon];
        GameObject go = Instantiate(weapon.AmmoType, _player.ShootPos.position, Quaternion.identity, null);
        go.GetComponent<Bullets>().ShootBullet(weapon.Damage, weapon.AmmoSpeed, _player.FpCamera.forward);
    }

    private void OnDestroy()
    {
        EventManager.Shoot -= Shoot;
    }
}
