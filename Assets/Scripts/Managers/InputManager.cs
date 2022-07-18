using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    #region Movement
    public bool CanMove = true;
    public Vector2 Move;
    #endregion

    #region Look
    public bool CanLook = true;
    public Vector2 Look;
    #endregion

    private void Update()
    {
        EventManager.OnLook(Look);
    }

    private void FixedUpdate()
    {
        EventManager.OnMove(Move);
    }

    #region MovementMethods
    public void OnMove(InputValue value)
    {
        if (!CanMove) return;
        Debug.Log(value.Get<Vector2>());
        // en el caso que necesitemos procesar esta
        // data ajustar esto, de momento creo que
        // funciona asi como esta
        Move = value.Get<Vector2>();
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
    }
    #endregion
}
