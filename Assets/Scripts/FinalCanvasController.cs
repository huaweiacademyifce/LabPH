using UnityEngine;

public class FinalCanvasController : MonoBehaviour
{
    public GameObject canvasFinal;

    public void ShowFinalCanvas()
    {
        canvasFinal.SetActive(true);
    }

    public void HideFinalCanvas()
    {
        canvasFinal.SetActive(false);
    }
}