using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : Entity
{
    #region Components
    [Header("Components")]
    [SerializeField] private PlayerScriptable _data;
    private Rigidbody _rb;
    #endregion

    private void Awake()
    {
        _transform = GetComponent<Transform>();

        if (TryGetComponent(out Rigidbody rb))
            _rb = rb;

        // si tiene vida guardada en los
        // prefs NO ejecutar esto
        _currentHp = _data.MaxHealth;
        _invulnerability = _data.Invulnerability;
    }

    protected override void Death()
    {
        throw new System.NotImplementedException();
    }

    #region Getter/Setters
    public PlayerScriptable Data
    {
        get { return _data; }
    }

    public Transform Transform
    {
        get { return _transform; }
    }

    public Rigidbody RB
    {
        get { return _rb; }
    }
    #endregion
}
