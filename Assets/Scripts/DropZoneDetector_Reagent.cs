using UnityEngine;

public class DropZoneDetector_Reagent : MonoBehaviour
{
    [Header("Qual reagente ESTE frasco representa (ex: Vinagre)")]
    public ChemicalSampleData targetReagentData;

    [Header("Progresso UI (arraste do Canvas)")]
    public ProgressPanelController progressPanel;
    public ProgressTabelaController progressTabela;

    [SerializeField] private float cooldown = 0.5f;
    private float nextAllowedTime;

    private void OnTriggerEnter(Collider other)
    {
        if (Time.time < nextAllowedTime) return;

        // Em vez de CompareTag (que tá bugando aí), usamos um "marker"
        var tip = other.GetComponent<DropperTipMarker>();
        if (tip == null) return;

        DropperController dropper = other.GetComponentInParent<DropperController>();
        if (dropper == null) return;

        if (!dropper.hasSample || dropper.currentSample == null) return;

        if (dropper.dropSpawnPoint == null || dropper.dropPrefab == null)
        {
            Debug.LogWarning("[DropZone] dropSpawnPoint ou dropPrefab não configurados no DropperController.");
            return;
        }

        // precisa ter reagente configurado
        if (targetReagentData == null)
        {
            Debug.LogWarning("[DropZone] targetReagentData está vazio nesse DropZone.");
            return;
        }

        // solta gota
        nextAllowedTime = Time.time + cooldown;
        dropper.ReleaseDrop();

        // ✅ Marca painel simples (reagentes)
        if (progressPanel != null)
            progressPanel.MarkCompleted(targetReagentData);

        // ✅ Marca tabela (indicador X reagente)
        if (progressTabela != null && dropper.currentSample.isIndicator)
        {
            progressTabela.MarkReactionCompleted(
                dropper.currentSample.indicatorType,
                targetReagentData.sampleType
            );
        }

        Debug.Log("[DropZone] 💧 Gota liberada + Progresso atualizado!");
    }
}