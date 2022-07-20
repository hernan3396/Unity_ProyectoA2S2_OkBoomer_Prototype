using UnityEngine;
using TMPro;

[RequireComponent(typeof(Player))]
public class PlayerWeaponManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _weaponText;
    private Player _player;

    private void Start()
    {
        _player = GetComponent<Player>();

        EventManager.ChangeWeapon += ChangeWeapon;
    }

    private void ChangeWeapon(int side)
    {
        int currentWeapon = _player.CurrentWeapon;
        int maxWeapons = _player.MaxWeapons;

        currentWeapon += side;

        if (currentWeapon > maxWeapons - 1)
        {
            currentWeapon = 0;
        }

        if (currentWeapon < 0)
        {
            currentWeapon = maxWeapons - 1;
        }

        _player.ChangeWeapons(currentWeapon);
        _weaponText.text = _player.Weapons[currentWeapon].Name;
    }

    private void OnDestroy()
    {
        EventManager.ChangeWeapon -= ChangeWeapon;
    }
}
