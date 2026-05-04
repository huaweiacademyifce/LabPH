using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorPlateController : MonoBehaviour
{
    [Header("Placas dos Indicadores")]
    public GameObject placaRepolhoRoxo;
    public GameObject placaFenolftaleina;
    public GameObject placaAzulBromotimol;
    public GameObject placaAlaranjadoMetila;

    public void ShowPlate(IndicatorType indicator)
    {
        HideAll();

        switch (indicator)
        {
            case IndicatorType.RepolhoRoxo:
                placaRepolhoRoxo.SetActive(true);
                break;

            case IndicatorType.Fenolftaleina:
                placaFenolftaleina.SetActive(true);
                break;

            case IndicatorType.AzulBromotimol:
                placaAzulBromotimol.SetActive(true);
                break;

            case IndicatorType.AlaranjadoMetila:
                placaAlaranjadoMetila.SetActive(true);
                break;
        }
    }

    public void HideAll()
    {
        placaRepolhoRoxo.SetActive(false);
        placaFenolftaleina.SetActive(false);
        placaAzulBromotimol.SetActive(false);
        placaAlaranjadoMetila.SetActive(false);
    }
}
