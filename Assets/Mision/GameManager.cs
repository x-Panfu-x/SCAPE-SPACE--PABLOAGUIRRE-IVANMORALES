using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public MissionUI missionUI;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        if (missionUI == null) missionUI = FindObjectOfType<MissionUI>();
        
        // Mensaje inicial que ves en tu imagen
        SetMissionActive(true, "Mission: Talk to Ekali, he's inside the (Old Mill)");
    }

    public void SetMissionActive(bool active, string newText = "")
    {
        if (missionUI != null)
        {
            missionUI.SetMissionActive(active, newText);
        }
    }

    public void CompleteMission()
    {
        SetMissionActive(false);
    }
}