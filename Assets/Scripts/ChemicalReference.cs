using UnityEngine;

public class ChemicalReference : MonoBehaviour
{
    [Header("Dados da Amostra")]
    public ChemicalSampleData data;

    [Header("Visual do Líquido")]
    public LiquidController liquidController;

    [Header("Configuração")]
    public bool isDropper = false;

    public void React(Color color)
    {
        if (liquidController == null)
            return;

        if (isDropper)
            liquidController.AbsorbIndicator(color);
        else
            liquidController.ApplyReaction(color);
    }
}
