using UnityEngine;
using System.Collections;

public class NPC2Reception : MonoBehaviour
{
    [Header("Paneles de Interacción")]
    public GameObject panelInteractuar; // El que dice "Presiona F"

    [Header("Referencias de Inventario")]
    public GameObject itemCarta;  
    public GameObject itemLlave;  
    public GameObject inventarioPanel;

    [Header("Paneles de Diálogo NPC2")]
    public GameObject mensaje1; // "Hola que tal..." (3s)
    public GameObject mensaje2; // "Perfecto muchas gracias" (3s)
    public GameObject mensaje3; // "Vale toma esta llave..." (5s)
    public GameObject mensaje4; // "Buena suerte..." (2s)

    [Header("Configuración")]
    public GameManager gameManager;
    private bool playerNear = false;
    private bool entregaRealizada = false;

    void Update()
    {
        // Detecta si el jugador está cerca, pulsa F y tiene la carta
        if (playerNear && Input.GetKeyDown(KeyCode.F) && !entregaRealizada)
        {
            if (itemCarta != null && itemCarta.activeSelf)
            {
                // Al presionar F, quitamos el mensaje de "Presiona F" y empieza el diálogo
                if (panelInteractuar != null) panelInteractuar.SetActive(false);
                StartCoroutine(SecuenciaEntregaCarta());
            }
        }
    }

    IEnumerator SecuenciaEntregaCarta()
    {
        entregaRealizada = true;

        mensaje1.SetActive(true);
        yield return new WaitForSeconds(3f);
        mensaje1.SetActive(false);

        mensaje2.SetActive(true);
        yield return new WaitForSeconds(3f);
        mensaje2.SetActive(false);

        if (itemCarta != null) itemCarta.SetActive(false);
        if (itemLlave != null) itemLlave.SetActive(true);

        mensaje3.SetActive(true);
        yield return new WaitForSeconds(5f);
        mensaje3.SetActive(false);

        mensaje4.SetActive(true);
        yield return new WaitForSeconds(2f);
        mensaje4.SetActive(false);

        if (gameManager != null)
        {
            gameManager.SetMissionActive(true, "Mission: Go to the Therians Mine.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !entregaRealizada) 
        {
            playerNear = true;
            // Mostramos el mensaje de "Presiona F" al acercarnos
            if (panelInteractuar != null) panelInteractuar.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            playerNear = false;
            // Si nos alejamos, quitamos el mensaje
            if (panelInteractuar != null) panelInteractuar.SetActive(false);
        }
    }
}