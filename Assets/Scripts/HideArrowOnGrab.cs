using UnityEngine;
using Oculus.Interaction;

public class HideArrowOnGrab : MonoBehaviour
{
    [Header("Seta holográfica")]
    public GameObject hologramArrow;

    private Grabbable grabbable;
    private bool arrowHidden = false;

    void Start()
    {
        grabbable = GetComponent<Grabbable>();
    }

    void Update()
    {
        if (grabbable == null) return;

        // se alguém estiver segurando o objeto
        if (!arrowHidden && grabbable.SelectingPointsCount > 0)
        {
            HideArrow();
        }
    }

    public void HideArrow()
    {
        if (hologramArrow != null)
        {
            hologramArrow.SetActive(false);
        }

        arrowHidden = true;
    }

    public void ShowArrow()
    {
        if (hologramArrow != null)
        {
            hologramArrow.SetActive(true);
        }

        // 🔹 RESET DO ESTADO
        arrowHidden = false;
    }
}