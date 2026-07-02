using UnityEngine;
using TMPro; // Para el texto
using UnityEngine.UI; // NECESARIO para manejar imágenes de UI

public class KeyPickup : MonoBehaviour
{
    [Header("Referencias de UI Misión")]
    [SerializeField] private TextMeshProUGUI missionText;
    [SerializeField] private GameObject promptUI;

    [Header("Referencias de Inventario")]
    [SerializeField] private GameObject keyIcon; // Arrastra aquí tu nueva imagen KeyIcon

    private bool isPlayerNearby = false;

    void Start()
    {
        // Configuración inicial
        if (missionText != null)
            missionText.text = "Find the key to escape";

        if (promptUI != null)
            promptUI.SetActive(false);
            
        // Aseguramos que el icono esté oculto al empezar
        if (keyIcon != null)
            keyIcon.SetActive(false);
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.F))
        {
            PickUpKey();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            if (promptUI != null) promptUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            if (promptUI != null) promptUI.SetActive(false);
        }
    }

    void PickUpKey()
    {
        // 1. Actualizamos texto de misión
        if (missionText != null)
            missionText.text = "Key found. Escape!";

        // 2. Ocultamos el prompt "F para coger"
        if (promptUI != null) promptUI.SetActive(false);
        
        // 3. MOSTRAMOS EL ICONO EN EL INVENTARIO
        if (keyIcon != null)
            keyIcon.SetActive(true);
        
        // 4. Destruimos la llave
        Debug.Log("Key collected");
        Destroy(gameObject);
    }
}