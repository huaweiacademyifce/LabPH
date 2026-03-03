using UnityEngine;

public class SampleSource : MonoBehaviour
{
    public ChemicalSampleData sampleData;

    private void OnTriggerEnter(Collider other)
    {
        // Só dispara se for a ponta do conta-gotas
        if (other.GetComponent<DropperTipMarker>() == null) return;

        DropperController dropper = other.GetComponentInParent<DropperController>();
        if (dropper == null) return;

        if (dropper.hasSample) return; // já está cheio

        dropper.AbsorbSample(sampleData);
        Debug.Log("🧪 Chamando AbsorbSample...");
    }
}