using UnityEngine;

public class DropperController : MonoBehaviour
{
    public bool hasSample = false;
    public ChemicalSampleData currentSample;

    [Header("Visual")]
    public Renderer liquidRenderer;   // Renderer do CContaGotas
    public Transform dropSpawnPoint;
    public GameObject dropPrefab;

    public void AbsorbSample(ChemicalSampleData sample)
    {
        currentSample = sample;
        hasSample = true;

        if (liquidRenderer != null)
        {
            liquidRenderer.material.color = sample.baseColor;
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

        // esvazia visualmente
        if (liquidRenderer != null)
            liquidRenderer.material.color = Color.clear;

        Debug.Log("💧 Gota liberada");
    }
}
