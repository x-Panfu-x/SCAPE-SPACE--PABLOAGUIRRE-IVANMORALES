using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class ControladorBacteria : MonoBehaviour
{
    private NavMeshAgent agente;
    [SerializeField] private Transform objetivo;

    [Header("Configuración de Reset")]
    // Estas son las coordenadas globales de tu mundo
    [SerializeField] private Vector3 posGlobalBacteria = new Vector3(-7.970666f, -0.02f, 61.0844f);
    [SerializeField] private Vector3 posGlobalPlayer = new Vector3(-10.894f, 1.5949f, -19.993f);

    private void Awake()
    {
        agente = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (agente != null && agente.enabled && objetivo != null)
        {
            agente.SetDestination(objetivo.position);
            agente.speed = 5f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 1. Resetear Jugador
            ResetearJugador(other.gameObject);

            // 2. Resetear Bacteria con conversión de espacio
            if (agente != null)
            {
                // Si tiene padre, convertimos el punto global a local para que el Warp sea preciso
                Vector3 posicionParaWarp = posGlobalBacteria;
                if (transform.parent != null)
                {
                    posicionParaWarp = transform.parent.InverseTransformPoint(posGlobalBacteria);
                }

                agente.Warp(posicionParaWarp);
                agente.ResetPath();
                agente.isStopped = false;

                Debug.Log("Bacteria teletransportada a: " + posicionParaWarp);
            }

            StartCoroutine(EfectoColision());
        }
    }

    private void ResetearJugador(GameObject playerObj)
    {
        CharacterController cc = playerObj.GetComponent<CharacterController>();
        if (cc != null)
        {
            cc.enabled = false;
            playerObj.transform.position = posGlobalPlayer;
            cc.enabled = true;
        }
        else
        {
            Rigidbody rb = playerObj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
            playerObj.transform.position = posGlobalPlayer;
        }
    }

    private IEnumerator EfectoColision()
    {
        Camera.main.backgroundColor = Color.red;
        yield return new WaitForSeconds(0.5f);
        Camera.main.backgroundColor = Color.white;
    }
}