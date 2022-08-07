using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private string _pauseScene = "Pause";
    [SerializeField] private string _uiScene = "UI";
    [SerializeField] private RectTransform _transitionPanel;
    [SerializeField] private int _fadeDur;

    private void Start()
    {
        EventManager.GoToNextLevel += OnNextLevel;
        EventManager.GameOver += OnGameOver;
        EventManager.StartUI += OnStartUI;
        EventManager.Pause += OnPause;
    }

    public void OnPause(bool value)
    {
        LoadLevel(_pauseScene, true);
    }

    public void OnStartUI()
    {
        LoadLevel(_uiScene, true);
    }

    public void OnNextLevel(string scene)
    {
        EventManager.OnLevelFinished();

        _transitionPanel.gameObject.SetActive(true);
        _transitionPanel.DOScaleX(1.05f, _fadeDur)
        .SetEase(Ease.OutExpo)
        .OnComplete(() => LoadLevel(scene));
    }

    public void LoadLevel(string scene, bool async = false)
    {
        if (async)
        {
            if (SceneManager.GetSceneByName(scene).isLoaded)
            {
                SceneManager.UnloadSceneAsync(scene);
                return;
            }

            SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
            return;
        }

        DOTween.KillAll();
        SceneManager.LoadScene(scene);
    }

    private void OnGameOver()
    {
        _transitionPanel.localScale = new Vector3(0, 1.1f, 1);
        _transitionPanel.gameObject.SetActive(true);
        _transitionPanel.DOScale(1.05f, _fadeDur)
        .SetEase(Ease.OutExpo)
        .OnComplete(() => LoadLevel(SceneManager.GetActiveScene().name));
    }

    private void OnDestroy()
    {
        EventManager.GoToNextLevel -= OnNextLevel;
        EventManager.GameOver -= OnGameOver;
        EventManager.StartUI -= OnStartUI;
        EventManager.Pause -= OnPause;
    }
}
