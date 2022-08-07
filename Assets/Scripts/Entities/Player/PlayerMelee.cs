using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Player))]
public class PlayerMelee : MonoBehaviour
{
    // aca use corutinas por vago
    private bool _canMelee = true;
    private GameObject _hitbox;
    private WeaponScriptable meleeWeapon;

    private void Start()
    {
        Player player = GetComponent<Player>();
        meleeWeapon = player.MeleeWeapon;

        _hitbox = player.Melee;
        _hitbox.GetComponent<MeleeDamage>().SetData(meleeWeapon.Damage);

        EventManager.Melee += StartMelee;
    }

    private void StartMelee()
    {
        if (!_canMelee) return;

        StopAllCoroutines();
        StartCoroutine("Melee");
    }

    IEnumerator Melee()
    {
        _canMelee = false;
        yield return new WaitForSeconds(meleeWeapon.Startup);

        _hitbox.SetActive(true);
        yield return new WaitForSeconds(meleeWeapon.Cooldown);

        _hitbox.SetActive(false);
        _canMelee = true;
    }
}
