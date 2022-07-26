using UnityEngine;
using TMPro;

public class DebugUtils : MonoBehaviour
{
    [SerializeField] private Transform _playerPos;
    [SerializeField] private PlayerScriptable _playerData;
    [SerializeField] private TMP_Text _speedText;
    [SerializeField] private TMP_Text _jumpStrengthText;
    [SerializeField] private TMP_Text _gravityText;
    [SerializeField] private TMP_Text _sensitivityText;
    [SerializeField] private TMP_Text _coyoteText;
    [SerializeField] private TMP_Text _apexText;

    private void Start()
    {
        _speedText.text = _playerData.Speed.ToString();
        _jumpStrengthText.text = _playerData.JumpStrength.ToString();
        _gravityText.text = _playerData.Gravity.ToString();
        _sensitivityText.text = _playerData.MouseSensitivity.ToString();
        _coyoteText.text = _playerData.CoyoteMaxTime.ToString();
        _apexText.text = _playerData.HalfGravityLimit.ToString();
    }

    public void ChangeSpeed(int value)
    {
        _playerData.Speed += value;
        _speedText.text = _playerData.Speed.ToString();
    }

    public void ChangeJump(int value)
    {
        _playerData.JumpStrength += value;
        _jumpStrengthText.text = _playerData.JumpStrength.ToString();
    }

    public void ChangeGravity(int value)
    {
        _playerData.Gravity += value;
        _gravityText.text = _playerData.Gravity.ToString();
    }

    public void ChangeSensitivity(int value)
    {
        if (_playerData.MouseSensitivity <= 1 && value < 0) return; // para que no quede en 0 jaj
        _playerData.MouseSensitivity += value;
        _sensitivityText.text = _playerData.MouseSensitivity.ToString();
    }

    public void ChangeCoyote(float value)
    {
        _playerData.CoyoteMaxTime += value;
        _coyoteText.text = _playerData.CoyoteMaxTime.ToString();
    }

    public void ChangeApexTime(float value)
    {
        _playerData.HalfGravityLimit += value;
        _apexText.text = _playerData.HalfGravityLimit.ToString();
    }

    public void MovePlayer(Transform endPos)
    {
        _playerPos.position = endPos.position;
    }
}
