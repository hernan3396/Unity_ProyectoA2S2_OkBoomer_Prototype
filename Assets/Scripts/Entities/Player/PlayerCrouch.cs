using UnityEngine;
using DG.Tweening;

public class PlayerCrouch : MonoBehaviour
{
    private enum Hitboxes
    {
        Standing,
        Crouching
    }

    #region Components
    private PlayerScriptable _data;
    private Rigidbody _rb;
    private Player _player;
    private Transform _camTransform;
    #endregion

    private GameObject[] _hitboxes;
    private Transform[] _cameraPos;

    private bool _isCrouching = false;

    private void Start()
    {
        _player = GetComponent<Player>();
        _rb = _player.RB;
        _data = _player.Data;
        _camTransform = _player.FpCamera.transform;

        _hitboxes = _player.Hitboxes;
        _cameraPos = _player.CameraPositions;

        EventManager.Crouch += CrouchInput;
    }

    private void CrouchInput(bool value)
    {
        _isCrouching = value;
    }

    public void Crouch()
    {
        _camTransform.position = _cameraPos[(int)Hitboxes.Crouching].position;
        // _camTransform.DOMove(_cameraPos[(int)Hitboxes.Crouching].position, 1)
        // .SetEase(Ease.OutQuart);

        _hitboxes[(int)Hitboxes.Standing].SetActive(false);
        _hitboxes[(int)Hitboxes.Crouching].SetActive(true);

        Vector3 newVel = new Vector3(_rb.velocity.x * _data.CrouchVel, _rb.velocity.y, _rb.velocity.z * _data.CrouchVel);
        _rb.velocity = newVel;
    }

    public void EndCrouch()
    {
        _camTransform.position = _cameraPos[(int)Hitboxes.Standing].position;

        // _camTransform.DOMove(_cameraPos[(int)Hitboxes.Standing].position, 1)
        // .SetEase(Ease.OutQuart);

        _hitboxes[(int)Hitboxes.Standing].SetActive(true);
        _hitboxes[(int)Hitboxes.Crouching].SetActive(false);
    }

    private void OnDestroy()
    {
        EventManager.Crouch -= CrouchInput;
    }

    public bool Crouching
    {
        get { return _isCrouching; }
    }
}
