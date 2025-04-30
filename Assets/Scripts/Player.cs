using UnityEngine;

public class Player : MonoBehaviour
{
    public float voidYPosition = -10f; // Posição Y abaixo da qual o player é considerado no void

    void Update()
    {
        if (transform.position.y < voidYPosition)
        {
            Die();
        }
    }

    void Die()
    {
        // ... Lógica de morte do player (efeitos sonoros, animações, etc.) ...

        GameManager.Instance.LoseGame(); // Chama a função LoseGame no GameManager
        Destroy(gameObject); // Destrói o player
    }
}