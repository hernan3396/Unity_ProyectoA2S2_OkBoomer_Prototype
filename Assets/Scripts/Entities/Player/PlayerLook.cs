using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerLook : MonoBehaviour
{
    #region Components
    private PlayerScriptable _data;
    #endregion

    #region BodyParts
    [Header("Body Parts")]
    [SerializeField] private Transform _fpCamera;
    [SerializeField] private Transform _body;
    #endregion

    private Vector2 _rotations = new Vector2(0, 90);

    private void Start()
    {
        _data = GetComponent<Player>().Data;
        EventManager.Look += LookAtMouse;
    }

    private void LookAtMouse(Vector2 look)
    {
        look *= _data.MouseSensitivity;

        // up & down
        _rotations.x -= look.y;
        _rotations.x = Mathf.Clamp(_rotations.x, _data.LookLimits.x, _data.LookLimits.y);

        // Sideways
        _rotations.y += look.x;

        Quaternion headRotation = Quaternion.AngleAxis(_rotations.x, Vector3.right);
        Quaternion bodyRotation = Quaternion.AngleAxis(_rotations.y, Vector3.up);

        _fpCamera.localRotation = headRotation;
        _body.localRotation = bodyRotation;
    }

    private void OnDestroy()
    {
        EventManager.Look -= LookAtMouse;
    }
}
