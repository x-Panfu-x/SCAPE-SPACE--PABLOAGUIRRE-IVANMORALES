using UnityEngine;

public class CursorManager : MonoBehaviour
{
    void Start()
    {
        // Hacer visible el cursor
        Cursor.visible = true;
        
        // Liberar el cursor para que puedas clicar en los botones
        Cursor.lockState = CursorLockMode.None;
    }
}