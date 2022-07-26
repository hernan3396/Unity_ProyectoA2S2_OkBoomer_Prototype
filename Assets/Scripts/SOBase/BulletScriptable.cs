using UnityEngine;

[CreateAssetMenu(fileName = "NewBullet", menuName = "OK Boomer/New Bullet", order = 0)]
public class BulletScriptable : ScriptableObject
{
    public enum BulletType
    {
        SimpleBullet,
        ExplosiveBullet
    }

    public float Duration;

    public BulletType AmmoType;
}