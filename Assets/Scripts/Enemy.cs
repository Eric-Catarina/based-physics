using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float voidYPosition = -10f; // Posição Y abaixo da qual o inimigo é considerado no void

    void Update()
    {
        if (transform.position.y < voidYPosition)
        {
            Die();
        }
    }

    void Die()
    {
        // ... Lógica de morte do inimigo (efeitos sonoros, animações, etc.) ...

        GameManager.Instance.OnEnemyDied?.Invoke(); // Chama o evento OnEnemyDied no GameManager
        Destroy(gameObject); // Destrói o inimigo
    }
}