using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HideArrowOnGrab : MonoBehaviour
{
    public GameObject hologramArrow;

    private XRGrabInteractable grab;

    void Awake()
    {
        grab = GetComponent<XRGrabInteractable>();
    }

    void OnEnable()
    {
        if (grab != null)
            grab.selectEntered.AddListener(OnGrab);
    }

    void OnDisable()
    {
        if (grab != null)
            grab.selectEntered.RemoveListener(OnGrab);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        HideArrow();
    }

    // 🔹 ESCONDE a seta (quando pega)
    public void HideArrow()
    {
        if (hologramArrow != null)
        {
            hologramArrow.SetActive(false);
            Debug.Log("Seta holográfica desativada em: " + gameObject.name);
        }
    }

    // 🔹 MOSTRA a seta (usado no RESET)
    public void ShowArrow()
    {
        if (hologramArrow != null)
        {
            hologramArrow.SetActive(true);
            Debug.Log("Seta holográfica reativada em: " + gameObject.name);
        }
    }
}
