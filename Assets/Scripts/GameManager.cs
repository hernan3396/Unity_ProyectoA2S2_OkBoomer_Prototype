using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    [SerializeField] private RectTransform _levelPanel;
    [SerializeField] private int _fadeDur;

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }

    private void Start()
    {
        if (_levelPanel)
            StartLevel();
    }

    // lo dejo aca porque de momento
    // no se donde ponerlo
    private void StartLevel()
    {
        _levelPanel.gameObject.SetActive(true);
        _levelPanel.DOScaleX(0, _fadeDur)
        .SetEase(Ease.OutExpo)
        .OnComplete(() => _levelPanel.gameObject.SetActive(false));
    }

    private void OnDestroy()
    {
        if (_instance != null)
            _instance = null;
    }

    public static GameManager GetInstance
    {
        get { return _instance; }
    }
}
