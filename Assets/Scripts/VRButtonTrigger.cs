using UnityEngine;
using UnityEngine.Events;

public class VRButtonTrigger : MonoBehaviour
{
    [Header("Evento disparado ao pressionar")]
    public UnityEvent onPressed;

    [Header("Evita múltiplos disparos seguidos")]
    public float cooldown = 0.5f;

    private float lastPressTime = -999f;

    private void OnTriggerEnter(Collider other)
    {
        // aceita qualquer collider do controle/mão
        if (!other.GetComponentInParent<Rigidbody>() &&
            !other.GetComponentInParent<Camera>())
        {
            return;
        }

        if (Time.time - lastPressTime < cooldown)
            return;

        lastPressTime = Time.time;

        Debug.Log("Botão VR pressionado");
        onPressed.Invoke();
    }

    // caso você queira resetar manualmente em algum momento
    public void ResetButton()
    {
        lastPressTime = -999f;
    }
}