using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ResetLabManager : MonoBehaviour
{
    [Header("Frascos")]
    public Transform[] frascos;
    private Vector3[] frascoPosicoes;
    private Quaternion[] frascoRotacoes;

    [Header("Conta-gotas")]
    public Transform dropper;
    public DropperController dropperController;
    public XRGrabInteractable dropperGrab;
    public XRInteractionManager interactionManager;

    private Vector3 dropperStartPos;
    private Quaternion dropperStartRot;

    [Header("Setas holográficas")]
    public HideArrowOnGrab[] arrowScripts;

    [Header("Progress Panel")]
    public ProgressPanelController progressPanel;

    [Header("Líquidos internos")]
    public LiquidController[] liquids;

    [Header("Reaction Manager")]
    public ReactionManager reactionManager;

    void Start()
    {
        frascoPosicoes = new Vector3[frascos.Length];
        frascoRotacoes = new Quaternion[frascos.Length];

        for (int i = 0; i < frascos.Length; i++)
        {
            frascoPosicoes[i] = frascos[i].position;
            frascoRotacoes[i] = frascos[i].rotation;
        }

        dropperStartPos = dropper.position;
        dropperStartRot = dropper.rotation;
    }

    public void ResetLab()
    {
        // primeiro troca o indicador
        if (reactionManager != null)
        {
            reactionManager.OnContinueButtonPressed();
        }

        // depois reseta o laboratório
        ResetFrascos();
        ResetDropper();
        ResetSetas();
        ResetLiquids();

        if (progressPanel != null)
            progressPanel.ResetAll();
    }

    void ResetFrascos()
    {
        for (int i = 0; i < frascos.Length; i++)
        {
            Rigidbody rb = frascos[i].GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }

            frascos[i].SetPositionAndRotation(
                frascoPosicoes[i],
                frascoRotacoes[i]
            );
        }
    }

    void ResetDropper()
    {
        if (dropperGrab != null && interactionManager != null)
        {
            interactionManager.CancelInteractableSelection(dropperGrab as IXRSelectInteractable);
        }

        Rigidbody rb = dropper.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        dropper.SetPositionAndRotation(dropperStartPos, dropperStartRot);

        if (dropperController != null)
        {
            dropperController.hasSample = false;
            dropperController.currentSample = null;

            if (dropperController.liquidController != null)
                dropperController.liquidController.Clear();
        }
    }

    void ResetSetas()
    {
        foreach (HideArrowOnGrab arrow in arrowScripts)
        {
            if (arrow != null)
                arrow.ShowArrow();
        }
    }

    void ResetLiquids()
    {
        foreach (LiquidController liquid in liquids)
        {
            if (liquid != null)
                liquid.ResetToBaseColor();
        }
    }
}