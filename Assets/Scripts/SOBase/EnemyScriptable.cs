using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "OK Boomer/New Enemy", order = 0)]
public class EnemyScriptable : ScriptableObject
{
    public int MaxHealth;
    public float Invulnerability;
    public int Speed;
    public WeaponScriptable Weapon;
    public int VisionRange;
}
