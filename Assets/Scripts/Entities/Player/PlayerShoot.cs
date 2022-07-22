using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private Player _player;
    private WeaponScriptable _weapon;
    [SerializeField] private PoolManager _test;

    private void Start()
    {
        _player = GetComponent<Player>();

        EventManager.Shoot += Shoot;
    }

    private void Shoot()
    {
        // weaponSelected
        _weapon = _player.SelectedWeapon;
        GameObject newBullet = _test.GetPooledObject();
        newBullet.transform.position = _player.ShootPos.position;
        newBullet.transform.rotation = _player.FpCamera.rotation;

        if (newBullet.TryGetComponent(out Bullets bullet))
        {
            bullet.SetData(_weapon.Damage, _weapon.AmmoSpeed, _weapon.AmmoType);
            newBullet.SetActive(true);
            bullet.Shoot();
        }
    }

    private void OnDestroy()
    {
        EventManager.Shoot -= Shoot;
    }
}
