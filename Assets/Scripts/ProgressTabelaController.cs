using UnityEngine;
using System.Collections.Generic;

public class ProgressTabelaController : MonoBehaviour
{
    [System.Serializable]
    public class CheckmarkEntry
    {
        public IndicatorType indicator;
        public SampleType sample;
        public GameObject checkmark;
    }

    [Header("Checkmarks da Tabela")]
    public List<CheckmarkEntry> checkmarks;

    private Dictionary<(IndicatorType, SampleType), GameObject> lookup;

    void Awake()
    {
        lookup = new Dictionary<(IndicatorType, SampleType), GameObject>();

        foreach (var entry in checkmarks)
        {
            entry.checkmark.SetActive(false);

            var key = (entry.indicator, entry.sample);
            if (!lookup.ContainsKey(key))
                lookup.Add(key, entry.checkmark);
        }
    }

    public void MarkReactionCompleted(IndicatorType indicator, SampleType sample)
{
    var key = (indicator, sample);

    if (lookup.TryGetValue(key, out GameObject checkmark))
    {
        checkmark.SetActive(true);
        Debug.Log("Tabela marcada: " + indicator + " + " + sample);
    }
    else
    {
        Debug.LogWarning("Tabela NÃO encontrou combinação: " + indicator + " + " + sample);
    }
}
}
