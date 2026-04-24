using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    public Transform teleportPoint;
    public GameObject painelInicial;

    public Transform xrRig;          // OVR Rig root
    public Transform centerEye;      // CenterEyeAnchor (Câmera)

    public void Teleport()
    {
        if (xrRig == null || teleportPoint == null || centerEye == null)
        {
            Debug.LogError("Teleport config missing!");
            return;
        }

        // 🔥 TELEPORTA POSIÇÃO
        xrRig.position = teleportPoint.position;

        // 🔥 CALCULA DIFERENÇA DE ROTAÇÃO DA CABEÇA
        float currentY = centerEye.eulerAngles.y;
        float targetY = teleportPoint.eulerAngles.y + 180f;

        float delta = targetY - currentY;

        // 🔥 APLICA NO RIG
        xrRig.Rotate(0f, delta, 0f);

        // UI
        if (painelInicial != null)
            painelInicial.SetActive(false);

        Debug.Log("Teleporte com rotação VR corrigida!");
    }
}