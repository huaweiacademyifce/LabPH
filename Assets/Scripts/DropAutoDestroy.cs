using UnityEngine;

public class DropAutoDestroy : MonoBehaviour
{
    // A referência ao ReactionManager precisa ser definida na cena, pois ele não está no objeto que colidiu.
    // Presumimos que o ReactionManager está no objeto de controle da cena.
    private ReactionManager reactionManager;

    void Start()
    {
        // Encontra o ReactionManager no início da cena.
        // Se o ReactionManager estiver em um objeto específico, use GetComponent<ReactionManager>().
        reactionManager = FindObjectOfType<ReactionManager>();

        if (reactionManager == null)
        {
            Debug.LogError("ERRO: ReactionManager não encontrado na cena!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 1. Verifica se colidiu com o cilindro interno (tag "Conteudo")
        if (other.CompareTag("Conteudo"))
        {
            // 2. O cilindro (Conteudo) está dentro do frasco. Precisamos do objeto PAI dele.
            // O componente ChemicalReference deve estar no objeto PAI do "Conteudo" ou no próprio frasco.

            // Tenta pegar o ChemicalReference no objeto que contém a tag "Conteudo"
            ChemicalReference frascoRef = other.GetComponent<ChemicalReference>();

            // Se não encontrou no próprio cilindro (Conteudo), tenta procurar no objeto pai.
            if (frascoRef == null)
            {
                frascoRef = other.GetComponentInParent<ChemicalReference>();
            }

            if (frascoRef != null && reactionManager != null)
            {
                // 3. Chama a função de reação
                reactionManager.RegisterDrop(frascoRef);

                // 4. Destrói a gota
                Destroy(gameObject);
            }
            else
            {
                // Este log é útil para debug!
                Debug.LogWarning("Não foi possível registrar a gota. FrascoRef ou ReactionManager ausentes.");
            }
        }
    }
}