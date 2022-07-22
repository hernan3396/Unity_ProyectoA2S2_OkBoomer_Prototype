using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Vector2 _spawnRange;
    // para probar de mientras lo dejo asi
    public void TakeDamage(int damage)
    {
        transform.position = new Vector3(Random.Range(_spawnRange.x, _spawnRange.y), transform.position.y, Random.Range(_spawnRange.x, _spawnRange.y));
    }
}
