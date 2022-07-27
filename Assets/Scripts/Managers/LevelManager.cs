using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private string _pauseScene;

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
        LoadLevel(scene);
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
