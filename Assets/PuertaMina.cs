using UnityEngine;

public class PuertaMina : MonoBehaviour
{
    [Header("Referencias de Inventario")]
    public GameObject itemLlave;      // Objeto ItemLlave dentro del inventario
    public GameObject inventarioPanel; // El panel que quieres que desaparezca (ej: InventarioPanel)
    
    [Header("Referencias de Interfaz")]
    public GameObject misionPanel;     // El panel de misiones que quieres que desaparezca
    public GameObject panelAbrir;      // El mensaje de "Presiona F para interactuar"

    [Header("Configuración de la Puerta")]
    public float velocidadGiro = 2f;
    
    private bool jugadorCerca = false;
    private bool puertaAbierta = false;
    private Quaternion rotacionObjetivo;

    void Start()
    {
        // Guardamos la rotación inicial
        rotacionObjetivo = transform.rotation;
    }

    void Update()
    {
        // Detectamos si el jugador está en el área, pulsa F y no se ha abierto ya
        if (jugadorCerca && Input.GetKeyDown(KeyCode.F) && !puertaAbierta)
        {
            // Verificamos si la llave está activa en el inventario
            if (itemLlave != null && itemLlave.activeSelf)
            {
                AbrirPuerta();
            }
            else
            {
                Debug.Log("You need the key to open this door.");
            }
        }

        // Si la puerta está marcada como abierta, rota suavemente hacia el objetivo
        if (puertaAbierta)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, Time.deltaTime * velocidadGiro);
        }
    }

    void AbrirPuerta()
    {
        puertaAbierta = true;
        // Calculamos la rotación de 90 grados (ajusta el eje Y si es necesario)
        rotacionObjetivo *= Quaternion.Euler(0, 90, 0); 
        
        // --- ACCIÓN: DESACTIVAR PANELES ---
        if (inventarioPanel != null) inventarioPanel.SetActive(false); // Quita el inventario
        if (misionPanel != null) misionPanel.SetActive(false);         // Quita las misiones
        if (panelAbrir != null) panelAbrir.SetActive(false);           // Quita el texto de "Presiona F"
        
        Debug.Log("Door open: Mission panels and inventory disabled.");
    }

    // Detección del área de la puerta (Trigger)
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !puertaAbierta)
        {
            jugadorCerca = true;
            if (panelAbrir != null) panelInteractuarVer(); // Activa el mensaje "Presiona F"
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false;
            if (panelAbrir != null) panelAbrir.SetActive(false); // Oculta el mensaje si te alejas
        }
    }

    void panelInteractuarVer()
    {
        if (panelAbrir != null) panelAbrir.SetActive(true);
    }
}