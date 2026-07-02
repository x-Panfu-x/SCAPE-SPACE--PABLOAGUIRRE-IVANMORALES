using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Paneles del Menú")]
    public GameObject panelPrincipal;
    public GameObject panelOpciones;
    public GameObject panelControles;

    [Header("Configuración Audio")]
    public AudioMixer audioMixer;
    public Slider sliderMusica;
    public Slider sliderSonido;

    [Header("Configuración Jugador")]
    public Slider sliderSensibilidad;

    void Start()
    {
        // Cargamos los valores guardados al iniciar
        if (sliderMusica != null)
            sliderMusica.value = PlayerPrefs.GetFloat("VolMusica", 0.75f);

        if (sliderSonido != null)
            sliderSonido.value = PlayerPrefs.GetFloat("VolSonido", 0.75f);

        if (sliderSensibilidad != null)
            sliderSensibilidad.value = PlayerPrefs.GetFloat("Sensibilidad", 2f);

        // Aplicamos el audio inicial si el mixer está conectado
        if (audioMixer != null)
        {
            ApplyVolume("VolMusica", sliderMusica != null ? sliderMusica.value : 0.75f);
            ApplyVolume("VolSonido", sliderSonido != null ? sliderSonido.value : 0.75f);
        }
    }

    // Método auxiliar para aplicar el volumen logarítmico
    private void ApplyVolume(string name, float valor)
    {
        // Usamos 0.0001f como mínimo para evitar errores matemáticos de Log10
        audioMixer.SetFloat(name, Mathf.Log10(Mathf.Max(valor, 0.0001f)) * 20);
    }

    // --- Navegación ---
    public void Jugar() 
    { 
        SceneManager.LoadScene(1); 
    }

    public void AbrirOpciones() 
    { 
        panelPrincipal.SetActive(false); 
        panelOpciones.SetActive(true); 
        panelControles.SetActive(false);
    }

    public void AbrirControles() 
    { 
        panelPrincipal.SetActive(false); 
        panelOpciones.SetActive(false);
        panelControles.SetActive(true); 
    }

    public void Regresar() 
    { 
        panelPrincipal.SetActive(true); 
        panelOpciones.SetActive(false); 
        panelControles.SetActive(false); 
    }

    public void Salir() 
    { 
        Application.Quit(); 
    }

    // --- Lógica de Ajustes ---
    public void CambiarVolumenMusica(float valor) 
    {
        ApplyVolume("VolMusica", valor);
        PlayerPrefs.SetFloat("VolMusica", valor);
    }

    public void CambiarVolumenSonido(float valor) 
    {
        ApplyVolume("VolSonido", valor);
        PlayerPrefs.SetFloat("VolSonido", valor);
    }

    public void CambiarSensibilidad(float valor) 
    {
        PlayerPrefs.SetFloat("Sensibilidad", valor);
    }
}