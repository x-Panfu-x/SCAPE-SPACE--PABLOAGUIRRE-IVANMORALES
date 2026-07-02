using UnityEngine;
using System.Collections;

public class NPCQuest : MonoBehaviour
{
    [Header("Paneles de Interacción")]
    public GameObject panelRecibir;    // Arrastra "InteractNPC"
    public GameObject panelCompletar;  // Arrastra "CompletarNPC"

    [Header("Secuencia de Información")]
    public GameObject info1; // Arrastra "InformacionCiudad1NPC"
    public GameObject info2; // Arrastra "InformacionCiudad2NPC"
    public GameObject info3; // Arrastra "InformacionCiudad3NPC"
    
    [Header("Inventario")]
    public GameObject inventarioPanel; // Arrastra "InventarioPanel"
    public GameObject itemCarta;       // Arrastra la Imagen "ItemCarta"

    [Header("Configuración")]
    public GameManager gameManager;
    public GameObject grupoManzanas; 
    public int applesNeeded = 4;

    [Header("Estado")]
    public int applesCollected = 0;
    public bool questActive = false;
    private bool questStarted = false;
    private bool playerNear = false;

    void Update()
    {
        if (playerNear && Input.GetKeyDown(KeyCode.F))
        {
            if (!questStarted)
            {
                IniciarMision();
            }
            else if (questActive && applesCollected >= applesNeeded)
            {
                StartCoroutine(SecuenciaFinalMision());
            }
        }
    }

    void IniciarMision()
    {
        questStarted = true;
        questActive = true;
        if (panelRecibir != null) panelRecibir.SetActive(false);
        if (grupoManzanas != null) grupoManzanas.SetActive(true);
        UpdateMissionUI();
    }

    public void UpdateMissionUI()
    {
        string mensaje = $"Mission: Collect {applesNeeded} apples ({applesCollected}/{applesNeeded})";
        if (gameManager != null) gameManager.SetMissionActive(true, mensaje);
    }

    public void AddApple()
    {
        applesCollected++;
        UpdateMissionUI();
    }

    IEnumerator SecuenciaFinalMision()
{
    questActive = false;
    if (panelCompletar != null) panelCompletar.SetActive(false);

    // Los 3 mensajes de 5 segundos cada uno
    if (info1 != null) info1.SetActive(true);
    yield return new WaitForSeconds(5f);
    if (info1 != null) info1.SetActive(false);

    if (info2 != null) info2.SetActive(true);
    yield return new WaitForSeconds(5f);
    if (info2 != null) info2.SetActive(false);

    if (info3 != null) info3.SetActive(true);
    yield return new WaitForSeconds(5f);
    if (info3 != null) info3.SetActive(false);

    // ESTO ES LO QUE HACE QUE APAREZCA LA CARTA
    if (inventarioPanel != null) inventarioPanel.SetActive(true); // Enciende el fondo
    if (itemCarta != null) itemCarta.SetActive(true);           // Enciende la imagen "Cartaa"

    if (gameManager != null) 
        gameManager.SetMissionActive(true, "Objective: Take the letter to Xino at house #5. (Talven City)");
}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        { 
            playerNear = true; 
            if (!questStarted)
                panelRecibir.SetActive(true);
            else if (questActive && applesCollected >= applesNeeded)
                panelCompletar.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) 
        { 
            playerNear = false; 
            if (panelRecibir != null) panelRecibir.SetActive(false);
            if (panelCompletar != null) panelCompletar.SetActive(false);
        }
    }
}