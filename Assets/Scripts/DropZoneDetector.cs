using UnityEngine;

public class DropZoneDetector : MonoBehaviour
{
    [Header("Referências")]
    public ChemicalReference parentFrasco;   // Qual frasco esta DropZone pertence
    public ReactionManager reactionManager;  // O gerente que receberá o evento da gota

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Droplet")) // Partícula da gota terá tag "Droplet"
        {
            reactionManager.RegisterDrop(parentFrasco);
        }
    }
}
