using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class PowerUp : MonoBehaviour
{
    private float respawnCooldown = 3;
    protected Transform playerTransform;
    public float range;
    public float duration;
    public Sprite icon;
    public LayerMask layerMask;

    public virtual void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    public abstract void Activate(); // Chamado quando o jogador ativa o powerup (com o clique esquerdo do mouse)
    public abstract void Hit(GameObject hitObject); // Chamado quando o powerup atinge a sua condição de sucesso

    public virtual IEnumerator Collect()
    {
        GetComponentInChildren<MeshRenderer>().enabled = false; // Desliga a visualizacao do powerup
        GetComponent<BoxCollider>().enabled = false; // Impede que o powerup seja coletado novamente antes do periodo de cooldown acabar

        yield return new WaitForSeconds(respawnCooldown);

        GetComponentInChildren<MeshRenderer>().enabled = true; // Liga a visualizacao do powerup
        GetComponent<Collider>().enabled = true;
    }
}
