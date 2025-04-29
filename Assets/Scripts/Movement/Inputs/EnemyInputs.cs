using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Splines;

public class EnemyInputs : CharacterInputs
{

    [SerializeField] private SplineContainer splineContainer;
    [SerializeField] private int splineToFollowIndex;
    private Spline splineToFollow;

    private int currentKnot = 0;

    private Vector3 startPosition;
    private Vector3 targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        splineToFollow = splineContainer.Splines[splineToFollowIndex];

        startPosition = transform.position;
        startPosition.y = 0;
    }

    // Update is called once per frame
    void Update()
    {
        targetPosition = splineToFollow.Next(currentKnot - 1).Position;
        targetPosition += startPosition;

        Vector3 direction = targetPosition - transform.position;
        float distance = Mathf.Abs((direction).magnitude);

        movingDirection = direction.normalized;

        Debug.Log(distance);

        if (distance < 2)
        {
            currentKnot++;
        }

    }
}
