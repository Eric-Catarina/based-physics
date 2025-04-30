using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPunchPowerUp : PowerUp
{
    public float pushStrength = 1;
    Vector3 playerPosition;
    ParticleSystem rangeVFX;

    public override void Start()
    {
        base.Start();
        rangeVFX = GameObject.Find("VFXGroundPunchRange").GetComponent<ParticleSystem>();
    }

    public override void Activate()
    {
        rangeVFX.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);

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

    public override IEnumerator Collect()
    {
        rangeVFX.Play();
        return base.Collect();
    }

    // Visualização da esfera na cena durante o Play Mode
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (playerTransform != null)
        {
            Gizmos.DrawWireSphere(playerTransform.position, range); 
        }
    }
}
