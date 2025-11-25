using UnityEngine;

public class ReactionManager : MonoBehaviour
{
    public void RegisterDrop(ChemicalReference frasco)
    {
        // Parte lógica será implementada depois
        Debug.Log("Gota detectada em: " + frasco.data.sampleName);
    }
}
