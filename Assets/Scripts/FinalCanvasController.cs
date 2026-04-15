using UnityEngine;

public class FinalCanvasController : MonoBehaviour
{
    public GameObject canvasFinal;
    [Header("Áudio")]
    public AudioSource audioSource;
    public AudioClip finalAudio;

    [Header("Referências")]
    public ResetLabManager resetLabManager;
    public ReactionManager reactionManager;
    public ProgressTabelaController progressTabela;

    public void ShowFinalCanvas()
    {
        canvasFinal.SetActive(true);
        if (audioSource != null && finalAudio != null)
        {
            audioSource.PlayOneShot(finalAudio);
        }
    }

    public void HideFinalCanvas()
    {
        canvasFinal.SetActive(false);
    }

    public void OnFinalContinue()
    {
        HideFinalCanvas();

        if (resetLabManager != null)
            resetLabManager.ResetLab();

        if (reactionManager != null)
            reactionManager.ResetToStart();

        if (progressTabela != null)
            progressTabela.ResetTabela();
    }
}