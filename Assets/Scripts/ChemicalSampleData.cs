using UnityEngine;

[CreateAssetMenu(fileName = "NovoChemicalSample", menuName = "Lab/Chemical Sample Data")]
public class ChemicalSampleData : ScriptableObject
{
    [Header("Identificação")]
    public string sampleName;
    public SampleType sampleType;   // ← NOVO (Água, Vinagre, etc.)

    [Header("Dados do laboratório")]
    public float ph;

    [Header("Cores do líquido")]
    public Color baseColor;       // cor inicial
    public Color reactionColor;   // cor após reagir
}
