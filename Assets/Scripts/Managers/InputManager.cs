using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    #region Movement
    public bool CanMove = true;
    public Vector2 Move;
    #endregion

    #region MovementMethods
    public void OnMove(InputValue value)
    {
        if (!CanMove) return;

        // en el caso que necesitemos procesar esta
        // data ajustar esto, de momento creo que
        // funciona asi como esta
        Move = value.Get<Vector2>();
    }
    #endregion
}
