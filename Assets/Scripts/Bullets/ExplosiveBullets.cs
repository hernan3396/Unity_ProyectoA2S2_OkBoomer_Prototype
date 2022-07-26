using UnityEngine;

public class ExplosiveBullets : Bullets, IExplosive
{
    private void Update()
    {
        BulletLifetime();
    }

    protected override void OnHit(Collision other)
    {
        if (other.transform.TryGetComponent(out Enemy enemy))
            enemy.TakeDamage(_damage);

        Explosion();
        DisableBullet();
    }

    public void Explosion()
    {
        Debug.Log("Explosion");
    }
}