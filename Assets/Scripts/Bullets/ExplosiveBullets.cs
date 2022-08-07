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

        if (_bounces >= 0)
            Bounce();
        else
            DisableBullet();
    }

    public void Explosion()
    {
        // Debug.Log("Explosion");
    }

    private void Bounce()
    {
        // _rb.velocity = -_rb.velocity;
        // _rb.velocity = _rb.velocity * 2;
        _bulletTimer = 0;
    }
}