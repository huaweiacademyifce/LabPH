using UnityEngine;

public class DropperController : MonoBehaviour
{
    public bool hasSample = false;
    public ChemicalSampleData currentSample;

    [Header("Visual")]
    public LiquidController liquidController;
    public Transform dropSpawnPoint;
    public GameObject dropPrefab;


    public void AbsorbSample(ChemicalSampleData sample)
    {
        if (sample == null) return;

        currentSample = sample;
        hasSample = true;

        if (liquidController != null)
        {
            liquidController.AbsorbIndicator(sample.baseColor);
        }

        Debug.Log("🧪 Conta-gotas absorveu: " + sample.sampleName);
    }

    public void ReleaseDrop()
    {
        if (!hasSample || dropPrefab == null) return;

        GameObject drop = Instantiate(
            dropPrefab,
            dropSpawnPoint.position,
            Quaternion.identity
        );

        Renderer r = drop.GetComponent<Renderer>();
        if (r != null)
            r.material.color = currentSample.baseColor;

        hasSample = false;

        if (liquidController != null)
            liquidController.Clear();

        Debug.Log("💧 Gota liberada");
    }
}