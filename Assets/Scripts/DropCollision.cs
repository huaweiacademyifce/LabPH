using UnityEngine;

public class DropCollision : MonoBehaviour
{
    private ReactionManager reactionManager;
    private bool hasHit = false;

    public void Init(ReactionManager manager)
    {
        reactionManager = manager;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasHit) return;

        ChemicalReference frasco = other.GetComponent<ChemicalReference>();

        if (frasco != null)
        {
            hasHit = true;

            reactionManager.RegisterDrop(frasco);

            Destroy(gameObject);
        }
    }
}