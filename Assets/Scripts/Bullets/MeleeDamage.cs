using UnityEngine;

public class MeleeDamage : MonoBehaviour
{
    private int _damage;

    public void SetData(int value)
    {
        _damage = value;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
            enemy.TakeDamage(_damage, other.transform);
    }
}
