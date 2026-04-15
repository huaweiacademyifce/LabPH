using UnityEngine;

public class ReactionManager : MonoBehaviour
{
    [Header("Áudio")]
    public AudioSource audioSource;
    public AudioClip dropSound;

    [Header("Canvas Final")]
    public FinalCanvasController finalCanvasController;

    [Header("Ordem dos Indicadores")]
    public IndicatorType[] indicatorOrder =
    {
        IndicatorType.RepolhoRoxo,
        IndicatorType.Fenolftaleina,
        IndicatorType.AzulBromotimol,
        IndicatorType.AlaranjadoMetila
    };

    private int currentIndicatorIndex = 0;
    public IndicatorType CurrentIndicator => indicatorOrder[currentIndicatorIndex];

    [Header("Referências")]
    public Renderer frascoIndicadorRenderer;
    public ProgressPanelController progressPanel;
    public ProgressTabelaController progressTabela;
    public ExperimentManager experimentManager;

    [Header("Frascos")]
    public ChemicalReference[] allFlasks;

    public void ResetAllFlasks()
    {
        foreach (var flask in allFlasks)
        {
            if (flask != null)
                flask.ResetVisual();
        }
    }

    void Start()
    {
        AtualizarCorDoIndicador();
    }

    // 🔥 CHAMADO QUANDO TERMINA AS 5 REAÇÕES
    public void OnAllReactionsCompleted()
    {
        Debug.Log("Todas reações concluídas!");

        // 👉 ÚLTIMO INDICADOR
        if (CurrentIndicator == IndicatorType.AlaranjadoMetila)
        {
            Debug.Log("Último indicador → mostrar canvas final");

            if (finalCanvasController != null)
                finalCanvasController.ShowFinalCanvas();

            return;
        }

        // 👉 INDICADORES NORMAIS
        if (progressPanel != null)
            progressPanel.ShowContinueButton();
    }

    // 🔘 BOTÃO CONTINUAR (dos 3 primeiros indicadores)
    public void OnContinueButtonPressed()
    {
        currentIndicatorIndex++;

        AtualizarCorDoIndicador();

        if (progressPanel != null)
            progressPanel.ResetAll();

        // 🔥 ESSENCIAL
        ResetAllFlasks();
    }

    private void AtualizarCorDoIndicador()
    {
        if (frascoIndicadorRenderer == null)
            return;

        Color novaCor = Color.clear;

        switch (CurrentIndicator)
        {
            case IndicatorType.RepolhoRoxo:
                ColorUtility.TryParseHtmlString("#5A2D82", out novaCor);
                novaCor.a = 0.5f;
                break;

            case IndicatorType.Fenolftaleina:
                novaCor = new Color(1f, 1f, 1f, 0.15f);
                break;

            case IndicatorType.AzulBromotimol:
                ColorUtility.TryParseHtmlString("#3CB371", out novaCor);
                novaCor.a = 0.5f;
                break;

            case IndicatorType.AlaranjadoMetila:
                ColorUtility.TryParseHtmlString("#FFC300", out novaCor);
                novaCor.a = 0.5f;
                break;
        }

        frascoIndicadorRenderer.material.color = novaCor;
    }

    // ===============================
    // CORES DAS REAÇÕES
    // ===============================
    private Color GetReactionColor(IndicatorType indicator, SampleType sample)
    {
        switch (indicator)
        {
            case IndicatorType.AlaranjadoMetila:
                switch (sample)
                {
                    case SampleType.Vinagre:
                        return Color.red;
                    default:
                        ColorUtility.TryParseHtmlString("#FFD300", out var amarelo);
                        return amarelo;
                }

            case IndicatorType.Fenolftaleina:
                switch (sample)
                {
                    case SampleType.Bicarbonato:
                        ColorUtility.TryParseHtmlString("#FF69B4", out var rosa);
                        return rosa;
                    case SampleType.SabaoPo:
                        ColorUtility.TryParseHtmlString("#FF1493", out var rosaForte);
                        return rosaForte;
                    default:
                        return Color.clear;
                }

            case IndicatorType.AzulBromotimol:
                switch (sample)
                {
                    case SampleType.Vinagre:
                        ColorUtility.TryParseHtmlString("#FFD399", out var amarelo);
                        return amarelo;
                    case SampleType.Bicarbonato:
                        ColorUtility.TryParseHtmlString("#1E90FF", out var azulClaro);
                        return azulClaro;
                    case SampleType.SabaoPo:
                        ColorUtility.TryParseHtmlString("#0000FF", out var azulForte);
                        return azulForte;
                    default:
                        ColorUtility.TryParseHtmlString("#00A86B", out var verde);
                        return verde;
                }

            case IndicatorType.RepolhoRoxo:
                switch (sample)
                {
                    case SampleType.Vinagre: return Color.red;
                    case SampleType.Bicarbonato: return Color.green;
                    case SampleType.SabaoPo: return new Color(0f, 0.5f, 0f);
                    case SampleType.Sal: return new Color(0.58f, 0.44f, 0.86f);
                    default: return new Color(0.42f, 0.35f, 0.80f);
                }
        }

        return Color.white;
    }

    // ===============================
    // QUANDO A GOTA CAI
    // ===============================
    public void RegisterDrop(ChemicalReference frasco)
    {
        if (frasco == null || frasco.data == null)
            return;

        if (audioSource != null && dropSound != null)
        {
            audioSource.PlayOneShot(dropSound);
        }

        if (experimentManager != null)
            experimentManager.RegisterReaction(frasco.data.sampleType);

        Color reactionColor = GetReactionColor(CurrentIndicator, frasco.data.sampleType);

        if (reactionColor != Color.clear)
        {
            frasco.React(reactionColor);
        }

        if (progressPanel != null)
            progressPanel.MarkCompleted(frasco.data);

        if (progressTabela != null)
            progressTabela.MarkReactionCompleted(
                CurrentIndicator,
                frasco.data.sampleType
            );
    }

    public void ResetToStart()
    {
        currentIndicatorIndex = 0;
        AtualizarCorDoIndicador();
    }
}