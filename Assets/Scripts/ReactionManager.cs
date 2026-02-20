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

    [Header("Frascos dos Indicadores (Renderer do líquido)")]
    public Renderer frascoIndicadorRenderer;

    [Header("Gerenciadores")]
    public ExperimentManager experimentManager;

    [Header("UI de Progresso")]
    public ProgressPanelController progressPanel;
    public ProgressTabelaController progressTabela;

    [Header("Estado Atual")]
    public ChemicalReference currentFlask;

    // ===============================
    // INÍCIO DA CENA (AJUSTE 1)
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

        Debug.Log($"Indicador atual: {CurrentIndicator}");

        AtualizarCorDoIndicador();

        if (progressPanel != null)
            progressPanel.ResetAll();
    }

    // ===============================
    // ATUALIZA VISUAL DO FRASCO
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
    // CHAMADO AO GOTEJAR
    // ===============================
    public void RegisterDrop(ChemicalReference frasco)
    {
        if (frasco == null || frasco.data == null)
            return;

        if (experimentManager != null)
            experimentManager.RegisterReaction(frasco.data.sampleType);

        Debug.Log(
            $"Reação: Indicador={CurrentIndicator} | " +
            $"Amostra={frasco.data.sampleType} | pH={frasco.data.ph}"
        );

        frasco.React(frasco.data.reactionColor);

        if (progressPanel != null)
            progressPanel.MarkCompleted(frasco.data);

        if (progressTabela != null)
            progressTabela.MarkReactionCompleted(
                CurrentIndicator,
                frasco.data.sampleType
            );
    }

    // ===============================
    // FRASCO ATUAL (PEGAR OBJETO)
    // ===============================
    public void SetCurrentFlask(ChemicalReference flask)
    {
        currentFlask = flask;
    }
}
