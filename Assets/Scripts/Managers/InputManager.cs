using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    #region Movement
    [Header("Movement")]
    public bool CanMove = true;
    public Vector2 Move;
    #endregion

    #region Look
    [Header("Look")]
    public bool CanLook = true;
    public Vector2 Look;
    #endregion

    #region Jumping
    [Header("Jumping")]
    public bool Jump = false;
    #endregion

    #region Shooting
    [Header("Shooting")]
    public bool Shoot = false;
    #endregion

    #region MovementMethods
    public void OnMove(InputValue value)
    {
        if (!CanMove) return;
        // en el caso que necesitemos procesar esta
        // data ajustar esto, de momento creo que
        // funciona asi como esta
        Move = value.Get<Vector2>();
        EventManager.OnMove(Move);
    }
    #endregion

    #region LookMethods
    public void OnLook(InputValue value)
    {
        if (!CanLook) return;

        // en el caso que necesitemos procesar esta
        // data ajustar esto, de momento creo que
        // funciona asi como esta
        Look = value.Get<Vector2>();
        EventManager.OnLook(Look);
    }
    #endregion

    #region JumpingMethods
    public void OnJump(InputValue value)
    {
        if (!CanMove) return;

        Jump = value.isPressed;
        EventManager.OnJump(Jump);
    }
    #endregion

    #region ShootingMethods
    public void OnShoot(InputValue value)
    {
        Shoot = value.isPressed; // de momento no se usa para nada
        EventManager.OnShoot();
    }
    #endregion

    #region ChangeWeaponMethods
    public void OnChangeWeapon(InputValue value)
    {
        float newValue = value.Get<float>();
        if (newValue > 0)
            EventManager.OnChangeWeapon(1);
        else if (newValue < 0)
            EventManager.OnChangeWeapon(-1);
    }
    #endregion
}
