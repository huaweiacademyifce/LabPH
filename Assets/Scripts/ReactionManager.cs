using UnityEngine;

public class ReactionManager : MonoBehaviour
{
    [Header("Configuração da Reação")]
    public float reactionDuration = 1.0f;

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

    [Header("Frasco do Indicador")]
    public Renderer frascoIndicadorRenderer;

    [Header("Gerenciadores")]
    public ExperimentManager experimentManager;

    [Header("UI de Progresso")]
    public ProgressPanelController progressPanel;
    public ProgressTabelaController progressTabela;

    [Header("Estado Atual")]
    public ChemicalReference currentFlask;

    // ===============================
    // START
    // ===============================

    private void Start()
    {
        AtualizarCorDoIndicador();
    }

    // ===============================
    // BOTÃO CONTINUAR
    // ===============================

    public void OnContinueButtonPressed()
    {
        AdvanceIndicator();
    }

    // ===============================
    // TROCA DE INDICADOR
    // ===============================

    private void AdvanceIndicator()
    {
        currentIndicatorIndex++;

        if (currentIndicatorIndex >= indicatorOrder.Length)
        {
            Debug.Log("Experimento finalizado");
            return;
        }

        Debug.Log("Novo indicador: " + CurrentIndicator);

        AtualizarCorDoIndicador();

        if (progressPanel != null)
            progressPanel.ResetAll();
    }

    // ===============================
    // COR DO FRASCO INDICADOR
    // ===============================

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

                ColorUtility.TryParseHtmlString("#1E90FF", out novaCor);
                novaCor.a = 0.5f;

                break;

            case IndicatorType.AlaranjadoMetila:

                ColorUtility.TryParseHtmlString("#FF8C00", out novaCor);
                novaCor.a = 0.5f;

                break;
        }

        frascoIndicadorRenderer.material.color = novaCor;
    }

    // ===============================
    // CALCULAR COR DA REAÇÃO
    // ===============================

    private Color GetReactionColor(IndicatorType indicator, SampleType sample)
    {
        switch (indicator)
        {
            // -----------------------
            // FENOLFTALEÍNA
            // -----------------------

            case IndicatorType.Fenolftaleina:

                switch (sample)
                {
                    case SampleType.Agua:
                    case SampleType.Vinagre:
                    case SampleType.Sal:

                        return Color.white;

                    case SampleType.Bicarbonato:

                        ColorUtility.TryParseHtmlString("#FF69B4", out var rosa);
                        return rosa;

                    case SampleType.SabaoPo:

                        ColorUtility.TryParseHtmlString("#FF1493", out var rosaForte);
                        return rosaForte;
                }

                break;


            // -----------------------
            // REPOLHO ROXO
            // -----------------------

            case IndicatorType.RepolhoRoxo:

                switch (sample)
                {
                    case SampleType.Agua:

                        ColorUtility.TryParseHtmlString("#6A5ACD", out var roxoClaro);
                        return roxoClaro;

                    case SampleType.Vinagre:

                        ColorUtility.TryParseHtmlString("#FF0000", out var vermelho);
                        return vermelho;

                    case SampleType.Bicarbonato:

                        ColorUtility.TryParseHtmlString("#00FF00", out var verde);
                        return verde;

                    case SampleType.Sal:

                        ColorUtility.TryParseHtmlString("#9370DB", out var roxoNeutro);
                        return roxoNeutro;

                    case SampleType.SabaoPo:

                        ColorUtility.TryParseHtmlString("#008000", out var verdeEscuro);
                        return verdeEscuro;
                }

                break;
        }

        return Color.white;
    }

    // ===============================
    // QUANDO UMA GOTA CAI
    // ===============================

    public void RegisterDrop(ChemicalReference frasco)
    {
        if (frasco == null || frasco.data == null)
            return;

        if (experimentManager != null)
            experimentManager.RegisterReaction(frasco.data.sampleType);

        Debug.Log(
            $"Reação: Indicador={CurrentIndicator} | " +
            $"Amostra={frasco.data.sampleType}"
        );

        Color reactionColor =
            GetReactionColor(CurrentIndicator, frasco.data.sampleType);

        frasco.React(reactionColor);

        if (progressPanel != null)
            progressPanel.MarkCompleted(frasco.data);

        if (progressTabela != null)
            progressTabela.MarkReactionCompleted(
                CurrentIndicator,
                frasco.data.sampleType
            );
    }

    // ===============================
    // FRASCO ATUAL
    // ===============================

    public void SetCurrentFlask(ChemicalReference flask)
    {
        currentFlask = flask;
    }
}