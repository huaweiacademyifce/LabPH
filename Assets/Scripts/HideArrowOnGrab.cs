using UnityEngine;
using Oculus.Interaction;

public class HideArrowOnGrab : MonoBehaviour
{
    public GameObject hologramArrow;

    private GrabInteractable grabInteractable;
    private bool alreadyHidden = false;

    void Awake()
    {
        grabInteractable = GetComponent<GrabInteractable>();
    }

    void OnEnable()
    {
        if (grabInteractable != null)
        {
            grabInteractable.WhenStateChanged += HandleStateChanged;
        }
    }

    void OnDisable()
    {
        if (grabInteractable != null)
        {
            grabInteractable.WhenStateChanged -= HandleStateChanged;
        }
    }

    private void HandleStateChanged(InteractableStateChangeArgs args)
    {
        if (alreadyHidden) return;

        if (args.NewState == InteractableState.Select)
        {
            alreadyHidden = true;

            if (hologramArrow != null)
            {
                hologramArrow.SetActive(false);
                Debug.Log("Seta holográfica desativada (Meta SDK).");
            }
        }
    }
}