using UnityEngine;

public class ApplePickup : MonoBehaviour
{
    public NPCQuest npc; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (npc != null)
            {
                npc.AddApple(); 
                gameObject.SetActive(false); // La manzana desaparece al cogerla
            }
        }
    }
}