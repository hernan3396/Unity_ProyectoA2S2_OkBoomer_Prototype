using UnityEngine;
using System;
using TMPro;

public class UIManager : MonoBehaviour
{
    public enum Element
    {
        Hp,
        Bullets
    }

    #region UiElements
    [SerializeField] private UiElement[] _uiElements;
    #endregion

    private void Start()
    {
        EventManager.UpdateUI += UpdateUI;
        EventManager.GameStart += OnGameStart;
        EventManager.GameOver += OnGameOver;
    }

    private void OnGameStart()
    {
        foreach (UiElement element in _uiElements)
        {
            element.Element.gameObject.SetActive(true);
        }
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

    private void OnDestroy()
    {
        EventManager.UpdateUI -= UpdateUI;
        EventManager.GameStart -= OnGameStart;
        EventManager.GameOver -= OnGameOver;
    }
}

[Serializable]
public class UiElement
{
    public TMP_Text Element;
    public string BaseText;

    public void UpdateElement(int value)
    {
        Element.text = $"{BaseText} {value}";
    }
}