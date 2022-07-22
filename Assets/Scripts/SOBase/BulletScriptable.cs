using UnityEngine;

[CreateAssetMenu(fileName = "NewBullet", menuName = "OK Boomer/New Bullet", order = 0)]
public class BulletScriptable : ScriptableObject
{
    public float Duration;
    public Vector3 Size;
    public Mesh Mesh;
    // aun no lo uso pero para futuro ya lo dejo
    public Material Material;
}