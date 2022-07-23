using UnityEngine;

public class CursorManager : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        EventManager.Pause += OnPause;
    }

    private void OnPause(bool value)
    {
        if (value)
            Cursor.lockState = CursorLockMode.Confined;
        else
            Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnDestroy()
    {
        EventManager.Pause -= OnPause;
    }
}
