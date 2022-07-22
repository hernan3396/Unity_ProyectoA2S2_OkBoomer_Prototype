using UnityEngine;
using TMPro;

public class DebugUtils : MonoBehaviour
{
    [SerializeField] private PlayerScriptable _player;
    [SerializeField] private TMP_Text _speedText;
    [SerializeField] private TMP_Text _jumpStrengthText;
    [SerializeField] private TMP_Text _gravityText;
    [SerializeField] private TMP_Text _sensitivityText;
    [SerializeField] private TMP_Text _coyoteText;
    [SerializeField] private TMP_Text _apexText;

    private void Start()
    {
        _speedText.text = _player.Speed.ToString();
        _jumpStrengthText.text = _player.JumpStrength.ToString();
        _gravityText.text = _player.Gravity.ToString();
        _sensitivityText.text = _player.MouseSensitivity.ToString();
        _coyoteText.text = _player.CoyoteMaxTime.ToString();
        _apexText.text = _player.HalfGravityLimit.ToString();
    }

    public void ChangeSpeed(int value)
    {
        _player.Speed += value;
        _speedText.text = _player.Speed.ToString();
    }

    public void ChangeJump(int value)
    {
        _player.JumpStrength += value;
        _jumpStrengthText.text = _player.JumpStrength.ToString();
    }

    public void ChangeGravity(int value)
    {
        _player.Gravity += value;
        _gravityText.text = _player.Gravity.ToString();
    }

    public void ChangeSensitivity(int value)
    {
        if (_player.MouseSensitivity <= 1 && value < 0) return; // para que no quede en 0 jaj
        _player.MouseSensitivity += value;
        _sensitivityText.text = _player.MouseSensitivity.ToString();
    }

    public void ChangeCoyote(float value)
    {
        _player.CoyoteMaxTime += value;
        _coyoteText.text = _player.CoyoteMaxTime.ToString();
    }

    public void ChangeApexTime(float value)
    {
        _player.HalfGravityLimit += value;
        _apexText.text = _player.HalfGravityLimit.ToString();
    }
}
