using UnityEngine;

[CreateAssetMenu(fileName = "PlayerScriptable", menuName = "OK Boomer/New Player Data", order = 0)]
public class PlayerScriptable : ScriptableObject
{
    public int MaxHealth;
    public int Invulnerability;
    public int Speed;

    #region Jump
    [Header("Jump")]
    public float HalfGravityLimit;
    public float CoyoteMaxTime;
    public int JumpStrength;
    public int FallMaxSpeed;
    public int Gravity;
    #endregion

    #region Look
    [Header("Look")]
    public float MouseSensitivity;
    public Vector2 LookLimits;
    #endregion
}