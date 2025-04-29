using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPad : MonoBehaviour
{

    private Animator animator;
    private bool isCharged = true;

    [SerializeField] private float rechargeTime;
    [SerializeField] private float rechargeAmount;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        if (!isCharged)
            Invoke(nameof(Recharge), rechargeTime);
        else
            Recharge();
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Player"))
        { 
            if(isCharged)
            {
                ValueBar energyBar = other.GetComponent<ValueBar>();
                
                energyBar.AddValue(rechargeAmount);
                
                isCharged = false;
                animator.SetBool("isCharged", false);

                Invoke(nameof(Recharge), rechargeTime);
            }
        }
    }

    public void Recharge()
    { 
        isCharged = true;
        animator.SetBool("isCharged", isCharged);
    }
}
