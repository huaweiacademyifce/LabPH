using UnityEngine;

[CreateAssetMenu(fileName = "NovoChemicalSample", menuName = "Lab/Chemical Sample Data")]
public class ChemicalSampleData : ScriptableObject
{
    public string sampleName;
    public float ph;
    public Color reactionColor;
}
