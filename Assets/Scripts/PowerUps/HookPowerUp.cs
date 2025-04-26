using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookPowerUp : PowerUp
{
    public float hookPullStrength = 3;

    
    Vector3 playerPosition;
    Vector3 mouseWorldPosition;
    [SerializeField] LayerMask rayFromCameraLayerMask;
    bool hasHit = false;
    Transform hitObjectTransform;
    Rigidbody hitObjectRb;

    public override void Activate()
    {
        Ray rayFromCamera = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(rayFromCamera, out RaycastHit hit, 1000, rayFromCameraLayerMask)) // Raycast para determinar a posição em que o jogador deseja atirar o gancho
        {
            mouseWorldPosition = hit.point;
            mouseWorldPosition.y = 0.1f; // Sobe a posição em 10 cm (metade da altura do spinner)
            playerPosition.y = 0.1f;

            
            Vector3 hookDir = mouseWorldPosition - playerPosition;
            hookDir.Normalize();
            Debug.DrawRay(playerPosition, hookDir * range, Color.red, 2); // Visualização do raio na cena

            if (Physics.Raycast(playerPosition, hookDir, out RaycastHit hitHook, range, layerMask)) // Raycast para lançar o gancho e procurar por outros spinners no caminho
            {
                Hit(hitHook.collider.gameObject);
            }
        }
    }

    public override void Hit(GameObject hitObject)
    {
        hasHit = true;
        hitObjectTransform = hitObject.transform;
        hitObjectRb = hitObject.GetComponent<Rigidbody>();

        Invoke("DisablePull", duration);
    }

    void DisablePull()
    {
        hasHit = false;
    }

    private void Update()
    {
        playerPosition = playerTransform.position;

        if (hasHit)
        {
            Vector3 pullForceDir = playerPosition - hitObjectTransform.position;
            pullForceDir.Normalize();

            hitObjectRb.AddForce(pullForceDir * hookPullStrength);
        }
    }
}
