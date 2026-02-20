using UnityEngine;

public class SampleSource : MonoBehaviour
{
    public ChemicalSampleData sampleData;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("DropperTip")) return;

        DropperController dropper =
            other.GetComponentInParent<DropperController>();

        if (dropper == null) return;
        if (dropper.hasSample) return;

        dropper.AbsorbSample(sampleData);
    }
}
