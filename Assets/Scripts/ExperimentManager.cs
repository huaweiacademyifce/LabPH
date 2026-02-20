using UnityEngine;
using System.Collections.Generic;

public class ExperimentManager : MonoBehaviour
{
    public IndicatorType currentIndicator = IndicatorType.RepolhoRoxo;

    private HashSet<SampleType> completedSamples = new HashSet<SampleType>();

    [Header("Referências")]
    public ProgressPanelController progressPanel;
    public GameObject continueButton;

    void Start()
    {
        continueButton.SetActive(false);
    }

    public void RegisterReaction(SampleType sample)
    {
        completedSamples.Add(sample);

        if (completedSamples.Count >= 5)
        {
            continueButton.SetActive(true);
        }
    }

    public void AdvanceIndicator()
    {
        completedSamples.Clear();
        continueButton.SetActive(false);

        // avança indicador
        currentIndicator++;

        Debug.Log("Novo indicador: " + currentIndicator);

        // resetar UI simples
        progressPanel.ResetAll();

        // aqui você vai resetar frascos, conta-gotas, setas etc.
    }
}
