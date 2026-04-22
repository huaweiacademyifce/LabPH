using UnityEngine;

public class DropperController : MonoBehaviour
{
    public bool hasSample = false;
    public ChemicalSampleData currentSample;

    [Header("Áudio")]
    public AudioSource audioSource;
    public AudioClip absorbSound;

    [Header("Visual")]
    public LiquidController liquidController;

    [Header("Drop")]
    public Transform dropSpawnPoint;
    public GameObject dropPrefab;

    [Header("Managers")]
    public ReactionManager reactionManager;

    public void AbsorbSample(ChemicalSampleData sample)
    {
        if (sample == null) return;

        currentSample = sample;

        if (!hasSample)
        {
            hasSample = true;

            if (audioSource != null && absorbSound != null)
                audioSource.PlayOneShot(absorbSound);
        }

        // 🔥 pega cor do frasco indicador
        Color indicatorColor = reactionManager.frascoIndicadorRenderer.material.color;

        if (liquidController != null)
            liquidController.AbsorbIndicator(indicatorColor);

        Debug.Log("Conta-gotas absorveu indicador: " + reactionManager.CurrentIndicator);
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
        {
            // 🔥 usa mesma cor do frasco
            r.material.color = reactionManager.frascoIndicadorRenderer.material.color;
        }

        DropCollision dropCollision = drop.GetComponent<DropCollision>();

        if (dropCollision != null)
            dropCollision.Init(reactionManager);

        hasSample = false;

        if (liquidController != null)
            liquidController.Clear();

        Debug.Log("Gota liberada");
    }
}