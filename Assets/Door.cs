using UnityEngine;
using System.Collections; // Necesario para la Corrutina

public class Door2 : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private GameObject promptUI;
    [SerializeField] private GameObject keyIcon;

    [Header("Configuración de Movimiento")]
    [SerializeField] private float rotationAmount = 90f;
    [SerializeField] private float smoothSpeed = 2f; // Velocidad de apertura
    
    private bool isPlayerNearby = false;
    private bool isOpened = false;

    void Start()
    {
        if (promptUI != null) promptUI.SetActive(false);
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.F) && !isOpened)
        {
            if (keyIcon != null && keyIcon.activeInHierarchy)
            {
                // Iniciamos la corrutina en lugar de llamar a una función normal
                StartCoroutine(OpenDoorRoutine());
            }
        }
    }

    // Esta función hace que la puerta se mueva poco a poco
    IEnumerator OpenDoorRoutine()
    {
        isOpened = true;
        if (promptUI != null) promptUI.SetActive(false);
        if (keyIcon != null) keyIcon.SetActive(false);

        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = startRotation * Quaternion.Euler(0, rotationAmount, 0);
        float time = 0;

        // Bucle que mueve la puerta durante 1 segundo aprox (ajustable)
        while (time < 1f)
        {
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, time);
            time += Time.deltaTime * smoothSpeed;
            yield return null; // Espera al siguiente frame
        }

        transform.rotation = targetRotation; // Aseguramos que quede perfecta al final
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isOpened)
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
}