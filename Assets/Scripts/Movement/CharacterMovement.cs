using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(ValueBar))]
public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private GameObject impactEffectPrefab;

    [Header("Stats")]

    [SerializeField] private float movementSpeed;
    [SerializeField] private float angularSpeed;

    [Tooltip("Value to multiply the rotation speed while using the rotation boost")]
    [SerializeField] private float rotationMultiplier;

    [Tooltip("Value to be reduced from the energy bar when using the rotation boost")]
    [SerializeField] private float energyCost;

    [Header("Limitations")]

    [SerializeField] private float maxMovementSpeedToAdd;

    [SerializeField] private float minAngularSpeed;
    [SerializeField] private float maxAngularSpeed;

    public static float maxImpactForce = 6f;

    private float currentMovementSpeed;
    private float currentAngularSpeed;

    private Rigidbody rb;
    private CharacterInputs inputs;
    private ValueBar energyBar;

    #region Properties

    public float getMovementSpeed
    {
        get { return movementSpeed; }
        set { movementSpeed = value; }
    }

    public float getAngularSpeed
    {
        get { return angularSpeed; }
        set { angularSpeed = value; }
    }

    public float getRotationMultiplier
    {
        get { return rotationMultiplier; }
        set { rotationMultiplier = value; }
    }

    public float getEnergyCost
    {
        get { return energyCost; }
        set { energyCost = value; }
    }

    public float getMaxMovementSpeedToAdd
    {
        get { return maxMovementSpeedToAdd; }
        set { maxMovementSpeedToAdd = value; }
    }

    public float getMinAngularSpeed
    {
        get { return minAngularSpeed; }
        set { minAngularSpeed = value; }
    }

    public float getMaxAngularSpeed
    {
        get { return maxAngularSpeed; }
        set { maxAngularSpeed = value; }
    }

    public float getCurrentMovementSpeed
    {
        get { return currentMovementSpeed; }
        set { currentMovementSpeed = value; }
    }

    public float getCurrentAngularSpeed
    {
        get { return currentAngularSpeed; }
        set { currentAngularSpeed = value; }
    }

    public Rigidbody getRb
    {
        get { return rb; }
        private set { rb = value; }
    }

    public CharacterInputs getInputs
    {
        get { return inputs; }
        private set { inputs = value; }
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        inputs = GetComponent<CharacterInputs>();
        energyBar = GetComponent<ValueBar>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Movement();
        Rotation();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        {
            CharacterMovement other = collision.gameObject.GetComponent<CharacterMovement>();
            CalculateCollisionLaunch(other);

            if(collision.gameObject.CompareTag("Player"))
            {
                Vector3 pos = collision.contacts[0].point;

                for (int i = 1; i < collision.contactCount; i++)
                {
                    pos += collision.contacts[i].point;
                }

                pos /= collision.contacts.Length;

                GameObject instance = Instantiate(impactEffectPrefab, pos, impactEffectPrefab.transform.rotation);
                Destroy(instance, 1f);
            }

        }
    }

    public void Movement()
    {
        currentMovementSpeed = new Vector3(rb.velocity.x, 0, rb.velocity.z).magnitude;

        if (currentMovementSpeed < maxMovementSpeedToAdd)
        {
            Vector3 force = inputs.getMovingDirection * movementSpeed * Time.deltaTime;
            rb.AddForce(force, ForceMode.Impulse);
        }
    }

    public void Rotation()
    {
        float speedPercentage = currentMovementSpeed / maxMovementSpeedToAdd;
        float rotMultiplier = 1f;

        if(getInputs.getIsBoostingRotation && !energyBar.getIsMin)
        {
            rotMultiplier = rotationMultiplier;
            energyBar.AddValue(-energyCost * Time.deltaTime);
        }

        currentAngularSpeed = Mathf.Lerp(minAngularSpeed * rotMultiplier, maxAngularSpeed * rotMultiplier, speedPercentage);
    }

    public void HitStop()
    {
        if (gameObject.CompareTag("Player"))
        {
            StartCoroutine(ReturnTimeStop());
            Time.timeScale = 0f;
        }
    }

    public IEnumerator ReturnTimeStop()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        Time.timeScale = 1f;
    }

    public void CalculateCollisionLaunch(CharacterMovement other)
    {
        Vector3 LaunchDirection = (transform.position - other.gameObject.transform.position).normalized;

        float force = (other.currentAngularSpeed / other.maxAngularSpeed) * CharacterMovement.maxImpactForce;

        if (force > maxImpactForce)
        {
            other.HitStop();
        }

        rb.AddForce(LaunchDirection * force, ForceMode.Impulse);
    }

}
