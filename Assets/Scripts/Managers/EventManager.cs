using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    public static event UnityAction<bool> Pause;
    public static void OnPause(bool value) => Pause?.Invoke(value);

    public static event UnityAction ResumeMenu;
    public static void OnResumeMenu() => ResumeMenu?.Invoke();

    public static event UnityAction<string> GoToNextLevel;
    public static void OnGoToNextLevel(string nextLevel) => GoToNextLevel?.Invoke(nextLevel);

    public static event UnityAction GameStart;
    public static void OnGameStart() => GameStart?.Invoke();

    public static event UnityAction StartUI;
    public static void OnStartUI() => StartUI?.Invoke();

    public static event UnityAction GameOver;
    public static void OnGameOver() => GameOver?.Invoke();

    public static event UnityAction<UIManager.Element, int> UpdateUI;
    public static void OnUpdateUI(UIManager.Element element, int value) => UpdateUI.Invoke(element, value);

    #region PlayerInputs
    public static event UnityAction<Vector2> Move;
    public static void OnMove(Vector2 move) => Move?.Invoke(move);

    public static event UnityAction<Vector2> Look;
    public static void OnLook(Vector2 look) => Look?.Invoke(look);

    public static event UnityAction<bool> Jump;
    public static void OnJump(bool jump) => Jump?.Invoke(jump);

    public static event UnityAction<bool> Shoot;
    public static void OnShoot(bool shoot) => Shoot?.Invoke(shoot);

    public static event UnityAction<int> ChangeWeapon;
    public static void OnChangeWeapon(int side) => ChangeWeapon?.Invoke(side);

    public static event UnityAction<bool> Crouch;
    public static void OnCrouch(bool crouch) => Crouch?.Invoke(crouch);
    #endregion
}
