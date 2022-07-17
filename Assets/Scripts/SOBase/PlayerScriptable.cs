using UnityEngine;

[CreateAssetMenu(fileName = "PlayerScriptable", menuName = "OK Boomer/New Player Data", order = 0)]
public class PlayerScriptable : ScriptableObject
{
    public int MaxHealth;
    public int Invulnerability;
    public int Speed;
    public int JumpHeight;
}