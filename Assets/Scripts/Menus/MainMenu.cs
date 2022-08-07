using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    #region CameraMovement
    [Header("Camera Movement")]
    [SerializeField] private Transform _cam;
    [SerializeField] private int _animSpeed;
    #endregion

    #region Other
    [Header("Other")]
    [SerializeField] private Button _selectedBtn;
    [SerializeField] private int _fadeDur;
    #endregion

    #region CanvasGroups
    [Header("Canvas Groups")]
    [SerializeField] private CanvasGroup _mainMenuCG;
    private CanvasGroup _currentCG;
    #endregion

    #region Timers
    [Header("timers")]
    // de momento es solo 1, luego divertirse con
    // haciendo la logica para los demas, aunque no deberia ser muy dificil
    [SerializeField] private string _timerPrefs;
    [SerializeField] private TMP_Text _timer;
    #endregion

    private void Awake()
    {
        SelectNewButton(_selectedBtn);

        // setea el primer valor
        _currentCG = _mainMenuCG;
    }

    private void Start()
    {
        FadeIn(_currentCG);

        RotateCamera();
        UpdateTimer();
    }

    private void FadeIn(CanvasGroup cg)
    {
        cg.DOFade(1, _fadeDur);
    }

    public void ChangeCG(CanvasGroup nextCG)
    {
        _currentCG.DOFade(0, _fadeDur)
        .OnComplete(() =>
        {
            _currentCG.gameObject.SetActive(false);

            _currentCG = nextCG;
            _currentCG.gameObject.SetActive(true);
            FadeIn(_currentCG);
        });
    }

    public void SelectNewButton(Button nextBtn)
    {
        // no supe como hacerlo solo en ChangeCG()
        // asi que lo hice con 2 eventos
        nextBtn.Select();
    }

    public void GoToNextLevel(string value)
    {
        EventManager.OnGoToNextLevel(value);
    }

    private void RotateCamera()
    {
        _cam.DORotate(new Vector3(0, 360, 0), _animSpeed, RotateMode.FastBeyond360)
        .SetEase(Ease.InOutBack)
        .SetLoops(-1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void UpdateTimer()
    {
        // esta parte deberia venir desde un "saves manager"
        if (PlayerPrefs.HasKey(_timerPrefs))
            _timer.text = PlayerPrefs.GetString(_timerPrefs);
    }
}
