using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button _selectedBtn;
    [SerializeField] private RectTransform _levelPanel;
    [SerializeField] private int _fadeDur;

    private void Awake()
    {
        _selectedBtn.Select();
    }

    public void Resume()
    {
        EventManager.OnResumeMenu();
    }

    public void MainMenu(string value)
    {
        _levelPanel.gameObject.SetActive(true);
        _levelPanel.DOScaleX(1.05f, _fadeDur)
        .SetEase(Ease.OutExpo)
        .OnComplete(() => EventManager.OnGoToNextLevel(value));
    }

    public void Quit()
    {
        Application.Quit();
    }
}
