using UnityEngine;

public class DropperController : MonoBehaviour
{
    public bool hasSample = false;
    public ChemicalSampleData currentSample;

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
        hasSample = true;

        Color indicatorColor = GetIndicatorColor();

        if (liquidController != null)
            liquidController.AbsorbIndicator(indicatorColor);

        Debug.Log("Conta-gotas absorveu indicador: " + reactionManager.CurrentIndicator);
    }

    Color GetIndicatorColor()
    {
        switch (reactionManager.CurrentIndicator)
        {
            case IndicatorType.RepolhoRoxo:

                ColorUtility.TryParseHtmlString("#5A2D82", out var roxo);
                return roxo;

            case IndicatorType.Fenolftaleina:

                return new Color(1f,1f,1f,0.15f);

            case IndicatorType.AzulBromotimol:

                ColorUtility.TryParseHtmlString("#1E90FF", out var azul);
                return azul;

            case IndicatorType.AlaranjadoMetila:

                ColorUtility.TryParseHtmlString("#FF8C00", out var laranja);
                return laranja;
        }

        return Color.white;
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
            r.material.color = GetIndicatorColor();

        hasSample = false;

        if (liquidController != null)
            liquidController.Clear();

        Debug.Log("Gota liberada");
    }
}