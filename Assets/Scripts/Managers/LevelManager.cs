using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private string _pauseScene;

    private void Start()
    {
        EventManager.Pause += OnPause;
    }

    public void OnPause(bool value)
    {
        LoadAsync(_pauseScene, value);
    }

    private void LoadAsync(string scene, bool load)
    {
        if (load)
            SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
        else if (SceneManager.GetSceneByName(scene).isLoaded) // chequeo por las dudas supongo
            SceneManager.UnloadSceneAsync(scene);
    }

    private void OnDestroy()
    {
        EventManager.Pause -= OnPause;
    }
}
