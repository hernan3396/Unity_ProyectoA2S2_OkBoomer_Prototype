using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "OK Boomer/New Weapon", order = 0)]
public class WeaponScriptable : ScriptableObject
{
    public int Id;
    public string Name;
    public int Damage;

    #region Ammo
    [Header("Ammo")]
    public BulletScriptable AmmoType;
    public int AmmoSpeed;
    public int MaxAmmo;
    #endregion

    #region Cooldown
    [Header("Cooldown")]
    public float StartUpTime;
    public float Cooldown;
    #endregion

    #region Recoil
    [Header("Recoil")]
    public int RecoilForce;
    public float RecoilTime;
    #endregion

    // esto lo dejo ya por si lo llegamos a usar de esta
    // manera (con el modelo en el scriptable)
    // public GameObject Model;
}