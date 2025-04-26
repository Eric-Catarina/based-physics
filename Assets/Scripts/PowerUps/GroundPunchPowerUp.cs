using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPunchPowerUp : PowerUp
{
    public float pushStrength = 1;
    Vector3 playerPosition;

    public override void Activate()
    {
        playerPosition = playerTransform.position;

        RaycastHit[] hitObjects = Physics.SphereCastAll(playerTransform.position, range, Vector3.up, range, layerMask);
        
        foreach(RaycastHit hitObject in hitObjects)
        {
            Hit(hitObject.collider.gameObject);
        }
    }

    public override void Hit(GameObject hitObject)
    {
        Transform hitObjectTransform = hitObject.transform;
        Rigidbody hitObjectRb = hitObject.GetComponent<Rigidbody>();

        Vector3 pushForce = hitObjectTransform.position - playerPosition;
        float hitObjectDistance = pushForce.magnitude; // Guarda a distancia para calcular a intensidade da forca que sera aplicada no objeto
        pushForce.Normalize();

        hitObjectDistance /= range; // Normaliza a distancia com base no alcance do powerup
        hitObjectRb.AddForce(pushForce * (pushStrength/hitObjectDistance), ForceMode.Impulse); // Quanto mais perto o objeto atingido estiver do player, maio sera a forca aplicada
    }

    // Visualização da esfera na cena durante o Play Mode
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        if (playerTransform != null)
        {
            Gizmos.DrawWireSphere(playerTransform.position, range); 
        }
    }
}
