using UnityEngine;
using UnityEngine.Events;

public class VRButtonTrigger : MonoBehaviour
{
    public UnityEvent onPressed;

    private bool pressed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (pressed) return;

        // aceita qualquer collider do controle/mão
        if (other.GetComponentInParent<Camera>() ||
            other.GetComponentInParent<Rigidbody>())
        {
            pressed = true;

            Debug.Log("Botão VR pressionado");

            onPressed.Invoke();
        }
    }

    public void ResetButton()
    {
        pressed = false;
    }
}