using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    protected Transform playerTransform;
    public float range;
    public float duration;
    public LayerMask layerMask;

    public void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    public abstract void Activate(); // Chamado quando o jogador ativa o powerup (com o clique esquerdo do mouse)
    public abstract void Hit(GameObject hitObject); // Chamado quando o powerup atinge a sua condição de sucesso
}
