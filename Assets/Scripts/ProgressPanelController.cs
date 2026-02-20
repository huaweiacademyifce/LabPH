using UnityEngine;

public class ProgressPanelController : MonoBehaviour
{
    [Header("Checkmarks")]
    public GameObject checkmarkAgua;
    public GameObject checkmarkVinagre;
    public GameObject checkmarkBicarbonato;
    public GameObject checkmarkSal;
    public GameObject checkmarkSabao;

    void Start()
    {
        ResetAll();
    }

    public void MarkCompleted(ChemicalSampleData sample)
    {
        if (sample == null) return;

        switch (sample.sampleType)
        {
            case SampleType.Agua:
                checkmarkAgua.SetActive(true);
                break;

            case SampleType.Vinagre:
                checkmarkVinagre.SetActive(true);
                break;

            case SampleType.Bicarbonato:
                checkmarkBicarbonato.SetActive(true);
                break;

            case SampleType.Sal:
                checkmarkSal.SetActive(true);
                break;

            case SampleType.SabaoPo:
                checkmarkSabao.SetActive(true);
                break;
        }
    }

    public void ResetAll()
    {
        checkmarkAgua.SetActive(false);
        checkmarkVinagre.SetActive(false);
        checkmarkBicarbonato.SetActive(false);
        checkmarkSal.SetActive(false);
        checkmarkSabao.SetActive(false);
    }
}
