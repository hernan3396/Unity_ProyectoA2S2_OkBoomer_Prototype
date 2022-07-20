using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerLook : MonoBehaviour
{
    #region Components
    private Player _player;
    private PlayerScriptable _data;
    #endregion

    private Vector2 _rotations = new Vector2(0, 90);

    private void Start()
    {
        _player = GetComponent<Player>();
        _data = _player.Data;
        EventManager.Look += LookAtMouse;
    }

    private void LookAtMouse(Vector2 look)
    {
        // lo divido entre 10 para que quede un numero mas lindo
        // en el inspector (2 en vez de 0.2 por ejemplo)
        look *= _data.MouseSensitivity / 10;

        // up & down
        _rotations.x -= look.y;
        _rotations.x = Mathf.Clamp(_rotations.x, _data.LookLimits.x, _data.LookLimits.y);

        // Sideways
        _rotations.y += look.x;

        Quaternion headRotation = Quaternion.AngleAxis(_rotations.x, Vector3.right);
        Quaternion bodyRotation = Quaternion.AngleAxis(_rotations.y, Vector3.up);

        _player.FpCamera.localRotation = headRotation;
        _player.Body.localRotation = bodyRotation;
    }

    private void OnDestroy()
    {
        EventManager.Look -= LookAtMouse;
    }
}
