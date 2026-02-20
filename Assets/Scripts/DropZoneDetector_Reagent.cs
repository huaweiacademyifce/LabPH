using UnityEngine;

public class DropZoneDetector_Reagent : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("DropperTip")) return;

        DropperController dropper =
            other.GetComponentInParent<DropperController>();

        if (dropper == null) return;
        if (!dropper.hasSample) return;

        dropper.ReleaseDrop();
    }
}
