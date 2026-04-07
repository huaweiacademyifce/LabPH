using UnityEngine;

public class IgnoreGrabbableCollisions : MonoBehaviour
{
    public string targetLayerName = "Interactable";

    void Start()
    {
        SetupIgnoreCollisions();
    }

    void SetupIgnoreCollisions()
    {
        int targetLayer = LayerMask.NameToLayer(targetLayerName);

        Collider[] allColliders = FindObjectsOfType<Collider>();

        for (int i = 0; i < allColliders.Length; i++)
        {
            if (allColliders[i].gameObject.layer != targetLayer)
                continue;

            for (int j = i + 1; j < allColliders.Length; j++)
            {
                if (allColliders[j].gameObject.layer != targetLayer)
                    continue;

                Physics.IgnoreCollision(allColliders[i], allColliders[j]);
            }
        }

        Debug.Log("✅ Colisão ignorada entre objetos da layer: " + targetLayerName);
    }
}