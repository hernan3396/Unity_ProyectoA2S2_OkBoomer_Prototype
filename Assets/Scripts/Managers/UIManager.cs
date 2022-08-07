using UnityEngine;
using System;
using TMPro;

public class UIManager : MonoBehaviour
{
    public enum Element
    {
        Hp,
        Bullets,
        Timer
    }

    #region UiElements
    [SerializeField] private UiElement[] _uiElements;
    #endregion

    private void Start()
    {
        EventManager.UpdateUI += UpdateUI;
        EventManager.GameStart += OnGameStart;
        EventManager.GameOver += OnGameOver;
        EventManager.LevelFinished += OnLevelFinished;
    }

    private void OnGameStart()
    {
        foreach (UiElement element in _uiElements)
            element.Element.gameObject.SetActive(true);
    }

    private void OnLevelFinished()
    {
        foreach (UiElement element in _uiElements)
            element.Element.gameObject.SetActive(false);
    }

    private void OnGameOver()
    {
        foreach (UiElement element in _uiElements)
        {
            element.Element.gameObject.SetActive(false);
        }
    }

    public void UpdateUI(Element element, int value)
    {
        _uiElements[(int)element].UpdateElement(value);
    }

    public void UpdateUI(Element element, string value)
    {
        _uiElements[(int)element].UpdateElement(value);
    }

    private void OnDestroy()
    {
        EventManager.UpdateUI -= UpdateUI;
        EventManager.GameStart -= OnGameStart;
        EventManager.GameOver -= OnGameOver;
        EventManager.LevelFinished -= OnLevelFinished;
    }
}

[Serializable]
public class UiElement
{
    public TMP_Text Element;
    public string BaseText;

    public void UpdateElement(int value)
    {
        Element.text = $"{BaseText}{value}";
    }

    public void UpdateElement(string value)
    {
        Element.text = value;
    }
}
