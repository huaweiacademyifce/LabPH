using UnityEngine;

public class UIStartButton : MonoBehaviour
{
    [Header("Painel que será desativado ao clicar no botão")]
    public GameObject painelInicial;

    [Header("Ponto para onde o jogador será teleportado")]
    public Transform teleportPoint;

    [Header("Objeto que será movido para o local do experimento (ex: Player, FPC, XR Origin)")]
    public GameObject playerRig;

    public void StartExperience()
    {
        Debug.Log("BOTAO CLICADO! (StartExperience foi chamado)");

        // 1. Desativa painel de introdução
        if (painelInicial != null)
            painelInicial.SetActive(false);

        // 2. Teleporta o player (somente se estiver configurado)
        if (playerRig != null && teleportPoint != null)
        {
            playerRig.transform.position = teleportPoint.position;
            playerRig.transform.rotation = teleportPoint.rotation;

            Debug.Log("Player teleportado para o ponto da mesa.");
        }
        else
        {
            Debug.LogWarning("PlayerRig ou TeleportPoint não configurados no UIStartButton.");
        }
    }
}
