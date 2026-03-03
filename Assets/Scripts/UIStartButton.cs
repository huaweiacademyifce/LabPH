using UnityEngine;

public class UIStartButton : MonoBehaviour
{
    [Header("Painel que será desativado ao clicar no botão")]
    public GameObject painelInicial;

    [Header("Ponto para onde o jogador será teleportado")]
    public Transform teleportPoint;

    [Header("Root do Camera Rig (BuildingBlock Camera Rig)")]
    public Transform cameraRigRoot;

    public void StartExperience()
    {
        Debug.Log("BOTAO CLICADO!");

        if (cameraRigRoot == null || teleportPoint == null)
        {
            Debug.LogError("CameraRigRoot ou TeleportPoint não configurado!");
            return;
        }

        // Teleporta o rig inteiro
        cameraRigRoot.position = teleportPoint.position;
        cameraRigRoot.rotation = teleportPoint.rotation;

        if (painelInicial != null)
            painelInicial.SetActive(false);

        Debug.Log("Teleporte aplicado com sucesso!");
    }
}