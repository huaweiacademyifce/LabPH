using UnityEngine;



[CreateAssetMenu(fileName = "NovoChemicalSample", menuName = "Lab/Chemical Sample Data")]
public class ChemicalSampleData : ScriptableObject
{
    [Header("Identificação")]
    public string sampleName;

    [Header("Tipo de amostra (reagentes)")]
    public SampleType sampleType;

    [Header("Tipo de indicador (apenas para indicadores)")]
    public bool isIndicator = false;
    public IndicatorType indicatorType;

    [Header("Dados do laboratório")]
    public float ph;

    [Header("Cores do líquido")]
    public Color baseColor;       // cor do líquido/indicador (ex: roxo, azul, laranja, transparente)
    public Color reactionColor;   // se você usar depois para “pós reação”
}