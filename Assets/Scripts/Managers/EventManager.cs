using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    public static event UnityAction<Vector2> Move;
    public static void OnMove(Vector2 move) => Move?.Invoke(move);

    public static event UnityAction<Vector2> Look;
    public static void OnLook(Vector2 look) => Look?.Invoke(look);

    public static event UnityAction<bool> Jump;
    public static void OnJump(bool jump) => Jump?.Invoke(jump);

    public static event UnityAction Shoot;
    public static void OnShoot() => Shoot?.Invoke();

    public static event UnityAction<int> ChangeWeapon;
    public static void OnChangeWeapon(int side) => ChangeWeapon?.Invoke(side);
}
