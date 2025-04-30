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
    LineRenderer hookRenderer;

    public override void Start()
    {
        base.Start(); // Chama o Start da classe pai
        hookRenderer = GameObject.Find("HookRenderer").GetComponent<LineRenderer>();
    }

    public override void Activate()
    {
        Ray rayFromCamera = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(rayFromCamera, out RaycastHit hit, 1000, rayFromCameraLayerMask)) // Raycast para determinar a posição em que o jogador deseja atirar o gancho
        {
            mouseWorldPosition = hit.point;
            playerPosition.y += 0.1f;
            mouseWorldPosition.y = playerPosition.y; // Sobe a posição em 10 cm (metade da altura do spinner)

            
            Vector3 hookDir = mouseWorldPosition - playerPosition;
            hookDir.Normalize();
            Debug.DrawRay(playerPosition, hookDir * range, Color.red, 2); // Visualização do raio na cena

            if (Physics.Raycast(playerPosition, hookDir, out RaycastHit hitHook, range, layerMask)) // Raycast para lançar o gancho e procurar por outros spinners no caminho
            {
                Hit(hitHook.collider.gameObject);
                Debug.Log(hit.point);
            }
            else
            {
                hookRenderer.SetPosition(1, playerPosition + hookDir * range);
            }
            hookRenderer.enabled = true; // Liga a visibilidade do gancho

            Invoke("DisablePull", duration);
        }
    }

    public override void Hit(GameObject hitObject)
    {
        hasHit = true;
        hitObjectTransform = hitObject.transform;
        hitObjectRb = hitObject.GetComponent<Rigidbody>();
    }

    void DisablePull()
    {
        hasHit = false;
        hookRenderer.enabled = false;
    }

    private void Update()
    {
        playerPosition = playerTransform.position;
        hookRenderer.SetPosition(0, playerPosition + 0.1f * Vector3.up);

        if (hasHit)
        {
            hookRenderer.SetPosition(1, hitObjectTransform.position + 0.1f * Vector3.up);
            Vector3 pullForceDir = playerPosition - hitObjectTransform.position;
            pullForceDir.Normalize();
            hitObjectRb.AddForce(pullForceDir * hookPullStrength);
        }
        else
        {
            hookRenderer.SetPosition(1, Vector3.Lerp(hookRenderer.GetPosition(1), playerPosition + 0.1f * Vector3.up, 3 * Time.deltaTime));
        }
    }
}
