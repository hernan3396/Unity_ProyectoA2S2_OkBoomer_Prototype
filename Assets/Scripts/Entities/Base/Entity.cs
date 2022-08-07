using UnityEngine;
using System.Collections;

public abstract class Entity : MonoBehaviour, IDamagable
{
    #region Components
    protected Transform _transform;
    #endregion

    #region Parameters
    protected int _currentHp;
    protected bool _isInmune = false;
    protected int _invulnerability;
    #endregion

    public void Damage(int value)
    {
        TakeDamage(value);
    }

    public virtual void TakeDamage(int value)
    {
        if (_isInmune) return;
        _isInmune = true;

        _currentHp -= value;

        StartCoroutine("InmuneReset");

        if (_currentHp <= 0)
            Death();
    }

    protected IEnumerator InmuneReset()
    {
        yield return new WaitForSeconds(_invulnerability);
        _isInmune = false;
    }

    protected abstract void Death();
}
