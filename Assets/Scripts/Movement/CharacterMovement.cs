using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour
{
    [Header("Stats")]

    [SerializeField] private float movementSpeed;
    [SerializeField] private float angularSpeed;

    [Tooltip("Value to multiply the rotation speed while using the rotation boost")]
    [SerializeField] private float rotationMultiplier;

    [Header("Limitations")]

    [SerializeField] private float maxMovementSpeedToAdd;

    [SerializeField] private float minAngularSpeed;
    [SerializeField] private float maxAngularSpeed;    

    private float currentMovementSpeed;
    private float currentAngularSpeed;

    private Rigidbody rb;
    private CharacterInputs inputs;

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
        private set {  rb = value; }
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
    }

    // Update is called once per frame
    void LateUpdate()
    {
        currentMovementSpeed = new Vector3(rb.velocity.x, 0, rb.velocity.z).magnitude;

        if (currentMovementSpeed < maxMovementSpeedToAdd)
        {
            Vector3 force = inputs.getMovingDirection * movementSpeed * Time.deltaTime;
            rb.AddForce(force, ForceMode.Impulse);
        }

        float speedPercentage = currentMovementSpeed / maxMovementSpeedToAdd;
        float rotMultiplier = getInputs.getIsBoostingRotation ? rotationMultiplier : 1f;

        currentAngularSpeed = Mathf.Lerp(minAngularSpeed * rotMultiplier, maxAngularSpeed * rotMultiplier, speedPercentage);
    }
}
