using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "OK Boomer/New Weapon", order = 0)]
public class WeaponScriptable : ScriptableObject
{
    public int Id;
    public string Name;

    #region Stats
    [Header("Stats")]
    // mientras mas cercano a (0,0) mas preciso
    public Vector2 Accuracy;
    public float Cooldown;
    public float Startup;
    public int Damage;
    #endregion

    #region SpecialShoot
    [Header("Special Shoot")]
    public float SpecialStartup;
    public float SpecialTime;
    public float SpecialCooldown;
    #endregion

    #region Ammo
    [Header("Ammo")]
    public BulletScriptable AmmoType;
    public int MaxBounces;
    public int AmmoSpeed;
    public int MaxAmmo;
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