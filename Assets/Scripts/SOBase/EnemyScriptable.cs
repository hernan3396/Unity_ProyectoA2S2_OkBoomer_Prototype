using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "OK Boomer/New Enemy", order = 0)]
public class EnemyScriptable : ScriptableObject
{
    public int MaxHealth;
    public float Invulnerability;
    public WeaponScriptable Weapon;

    #region Movement
    public int Speed;
    public int WalkPointRange;
    #endregion

    #region Vision
    public int VisionRange;
    public int AttackRange;
    #endregion
}
