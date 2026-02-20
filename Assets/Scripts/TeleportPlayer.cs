using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    public Transform teleportPoint;
    public GameObject painelInicial;
    public Transform xrRig; // Root do XR Rig / Camera Rig

    public void Teleport()
    {
        if (xrRig == null || teleportPoint == null)
        {
            Debug.LogError("Teleport config missing!");
            return;
        }

        // Teleporta o XR Rig inteiro
        xrRig.position = teleportPoint.position;
        xrRig.rotation = teleportPoint.rotation;

        // Desativa painel inicial
        if (painelInicial != null)
            painelInicial.SetActive(false);

        Debug.Log("Teleporte VR aplicado com sucesso!");
    }
}
