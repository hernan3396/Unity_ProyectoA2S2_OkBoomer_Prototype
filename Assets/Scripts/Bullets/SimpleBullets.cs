using UnityEngine;

public class SimpleBullets : Bullets
{
    private void Update()
    {
        BulletLifetime();
    }

    protected override void OnHit(Collision other)
    {
        if (other.transform.TryGetComponent(out Enemy enemy))
            enemy.TakeDamage(_damage, transform);

        if (other.transform.TryGetComponent(out Player player))
            player.TakeDamage(_damage);

        DisableBullet();
    }
}
