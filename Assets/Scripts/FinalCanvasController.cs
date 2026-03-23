using UnityEngine;

public class FinalCanvasController : MonoBehaviour
{
    public GameObject canvasFinal;

    [Header("Referências")]
    public ResetLabManager resetLabManager;
    public ReactionManager reactionManager;
    public ProgressTabelaController progressTabela;

    public void ShowFinalCanvas()
    {
        canvasFinal.SetActive(true);
    }

    public void HideFinalCanvas()
    {
        canvasFinal.SetActive(false);
    }

    // ✅ BOTÃO FINAL
    public void OnFinalContinue()
    {
        // Esconde canvas final
        HideFinalCanvas();

        // Reseta laboratório (posição, líquidos, painel)
        if (resetLabManager != null)
            resetLabManager.ResetLab();

        // Volta pro primeiro indicador
        if (reactionManager != null)
            reactionManager.ResetToStart();

        // Reseta tabela (AGORA FUNCIONA)
        if (progressTabela != null)
            progressTabela.ResetTabela();

        Debug.Log("Experimento reiniciado COMPLETO");
    }
}