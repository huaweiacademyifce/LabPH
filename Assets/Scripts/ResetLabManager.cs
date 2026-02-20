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

    void Start()
    {
        // salva estado inicial dos frascos
        frascoPosicoes = new Vector3[frascos.Length];
        frascoRotacoes = new Quaternion[frascos.Length];

        for (int i = 0; i < frascos.Length; i++)
        {
            frascoPosicoes[i] = frascos[i].position;
            frascoRotacoes[i] = frascos[i].rotation;
        }

        // salva estado inicial do conta-gotas
        dropperStartPos = dropper.position;
        dropperStartRot = dropper.rotation;
    }

    public void ResetLab()
    {
        ResetFrascos();
        ResetDropper();
        ResetSetas();
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
        // força soltar o conta-gotas se estiver na mão
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

        // 🔹 reset lógico
        if (dropperController != null)
        {
            dropperController.hasSample = false;
            dropperController.currentSample = null;

            // 🔹 reset visual (AQUI ESTÁ A CORREÇÃO)
            if (dropperController.liquidRenderer != null)
            {
                dropperController.liquidRenderer.material.color = Color.clear;
            }
        }
    }

    void ResetSetas()
    {
        foreach (HideArrowOnGrab arrow in arrowScripts)
        {
            if (arrow != null && arrow.hologramArrow != null)
            {
                arrow.hologramArrow.SetActive(true);
            }
        }
    }
}
