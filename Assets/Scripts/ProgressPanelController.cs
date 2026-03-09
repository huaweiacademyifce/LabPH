using UnityEngine;

public class ProgressPanelController : MonoBehaviour
{
    [Header("Checkmarks")]
    public GameObject checkmarkAgua;
    public GameObject checkmarkVinagre;
    public GameObject checkmarkBicarbonato;
    public GameObject checkmarkSal;
    public GameObject checkmarkSabao;

    [Header("Botão Continuar")]
    public GameObject continueButton;

    private int completedCount = 0;
    private int totalRequired = 5;

    void Start()
    {
        ResetAll();
    }

    public void MarkCompleted(ChemicalSampleData sample)
    {
        if (sample == null) return;

        bool newlyActivated = false;

        switch (sample.sampleType)
        {
            case SampleType.Agua:
                if (!checkmarkAgua.activeSelf)
                {
                    checkmarkAgua.SetActive(true);
                    newlyActivated = true;
                }
                break;

            case SampleType.Vinagre:
                if (!checkmarkVinagre.activeSelf)
                {
                    checkmarkVinagre.SetActive(true);
                    newlyActivated = true;
                }
                break;

            case SampleType.Bicarbonato:
                if (!checkmarkBicarbonato.activeSelf)
                {
                    checkmarkBicarbonato.SetActive(true);
                    newlyActivated = true;
                }
                break;

            case SampleType.Sal:
                if (!checkmarkSal.activeSelf)
                {
                    checkmarkSal.SetActive(true);
                    newlyActivated = true;
                }
                break;

            case SampleType.SabaoPo:
                if (!checkmarkSabao.activeSelf)
                {
                    checkmarkSabao.SetActive(true);
                    newlyActivated = true;
                }
                break;
        }

        if (newlyActivated)
        {
            completedCount++;
            Debug.Log("Reações concluídas: " + completedCount);

            if (completedCount >= totalRequired)
            {
                continueButton.SetActive(true);
                Debug.Log("Botão Continuar ativado!");
            }
        }
    }

    public void ResetAll()
    {
        checkmarkAgua.SetActive(false);
        checkmarkVinagre.SetActive(false);
        checkmarkBicarbonato.SetActive(false);
        checkmarkSal.SetActive(false);
        checkmarkSabao.SetActive(false);

        completedCount = 0;

        if (continueButton != null)
            continueButton.SetActive(false);
    }
}