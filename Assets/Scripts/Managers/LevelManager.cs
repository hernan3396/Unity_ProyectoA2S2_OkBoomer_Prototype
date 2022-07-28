using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private string _pauseScene;
    [SerializeField] private RectTransform _transitionPanel;
    [SerializeField] private int _fadeDur;

    private void Start()
    {
        EventManager.GoToNextLevel += OnNextLevel;
        EventManager.Pause += OnPause;
    }

    public void OnPause(bool value)
    {
        LoadLevel(_pauseScene, true);
    }

    public void OnNextLevel(string scene)
    {
        DOTween.KillAll();

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

        SceneManager.LoadScene(scene);
    }

    private void OnDestroy()
    {
        EventManager.GoToNextLevel -= OnNextLevel;
        EventManager.Pause -= OnPause;
    }
}
