using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    #region CameraMovement
    [Header("Camera Movement")]
    [SerializeField] private Transform _cam;
    [SerializeField] private int _animSpeed;
    #endregion

    #region Other
    [Header("Other")]
    [SerializeField] private RectTransform _nextLevelPanel;
    [SerializeField] private Button _selectedBtn;
    [SerializeField] private int _fadeDur;
    #endregion

    #region CanvasGroups
    [Header("Canvas Groups")]
    [SerializeField] private CanvasGroup _mainMenuCG;
    [SerializeField] private CanvasGroup _levelSelectCG;
    private CanvasGroup _currentCG;
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
        // cambiar esta parte al level manager asi lo hace desde ahi
        _nextLevelPanel.gameObject.SetActive(true);
        _nextLevelPanel.DOScaleX(1.05f, _fadeDur * 3) // por lo visto 1 no es exacto el tamaño de la pantalla asi que lo hago un poco mas grande
        .SetEase(Ease.OutExpo)
        .OnComplete(() => EventManager.OnGoToNextLevel(value));
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
}
