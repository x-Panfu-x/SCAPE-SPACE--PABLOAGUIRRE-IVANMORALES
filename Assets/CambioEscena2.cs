using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cambiar de escena

public class CambioEscena2 : MonoBehaviour
{
    // Aquí pondrás el nombre exacto de la escena a la que quieres ir
    public string nombreEscenaDestino;

    private void OnTriggerEnter(Collider other)
    {
        // Solo cambiamos de escena si el objeto que entra tiene la etiqueta "Player"
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(nombreEscenaDestino);
        }
    }
}