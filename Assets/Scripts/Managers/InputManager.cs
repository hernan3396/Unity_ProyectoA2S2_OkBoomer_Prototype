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

    #region Pause
    [Header("Pause")]
    public bool Pause = false;
    #endregion

    private void Start()
    {
        EventManager.ResumeMenu += OnPause;
    }

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
        if (!CanLook) return;

        Shoot = value.isPressed; // de momento no se usa para nada
        EventManager.OnShoot();
    }
    #endregion

    #region ChangeWeaponMethods
    public void OnChangeWeapon(InputValue value)
    {
        if (!CanLook) return;

        float newValue = value.Get<float>();
        if (newValue > 0)
            EventManager.OnChangeWeapon(1);
        else if (newValue < 0)
            EventManager.OnChangeWeapon(-1);
    }
    #endregion

    #region PauseMethods
    public void OnPause()
    {
        Pause = !Pause;
        CanMove = !Pause;
        CanLook = !Pause;

        // frena el movimiento de las inputs
        // sino queda saltando o moviendose
        Jump = false;
        EventManager.OnJump(Jump);

        EventManager.OnPause(Pause);
    }
    #endregion

    private void OnDestroy()
    {
        EventManager.ResumeMenu -= OnPause;
    }
}
