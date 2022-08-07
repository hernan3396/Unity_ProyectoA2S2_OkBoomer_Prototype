using UnityEngine;

public class TimeManager : MonoBehaviour, IPausable
{
    #region Components
    private UIManager _uiManager;
    #endregion

    [SerializeField] private string _prefsName = "Hyperwave_Lv1Timer";

    private bool _isPaused = false;
    private float time;
    private float msec;
    private float sec;
    private float min;
    private string totalTime;

    private void Awake()
    {
        _uiManager = GetComponent<UIManager>();
    }

    private void Start()
    {
        EventManager.Pause += OnPause;
        EventManager.LevelFinished += OnLevelFinished;

        // Debug.Log(PlayerPrefs.GetString(_prefsName));
    }

    private void Update()
    {
        if (_isPaused) return;
        UpdateTimer();
    }

    private void UpdateTimer()
    {
        time += Time.deltaTime;
        msec = (int)((time - (int)time) * 100);
        sec = (int)(time % 60);
        min = (int)(time / 60 % 60);
        totalTime = string.Format("{0:00}:{1:00}:{2:00}", min, sec, msec);

        _uiManager.UpdateUI(UIManager.Element.Timer, totalTime);
    }

    private void OnLevelFinished()
    {
        _isPaused = true;

        // esta parte deberia estar en su script especifico
        // al pasar al alpha ajustar esto entre otras cosas
        if (PlayerPrefs.HasKey("Hyperwave_Lv1Timer_float"))
        {
            if (PlayerPrefs.GetFloat("Hyperwave_Lv1Timer_float") >= time)
            {
                PlayerPrefs.SetFloat("Hyperwave_Lv1Timer_float", time);
                PlayerPrefs.SetString(_prefsName, totalTime);
            }
            return;
        }

        PlayerPrefs.SetFloat("Hyperwave_Lv1Timer_float", time);
        PlayerPrefs.SetString(_prefsName, totalTime);
    }

    public void OnPause(bool value)
    {
        _isPaused = value;
    }

    private void OnDestroy()
    {
        EventManager.Pause += OnPause;
        EventManager.LevelFinished -= OnLevelFinished;
    }
}