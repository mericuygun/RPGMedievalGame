using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class AttackSystem : Singleton<AttackSystem>
{
    public Animator animator;
    bool IsAttacking;
    int attackIndex;
    bool canAttack;
    int combo;
    public float comboTimer;
    const string strCombo1 = "Hit1";
    const string strCombo2 = "Hit2";
    const string strCombo3 = "Hit3";
    public Collider weaponCollider;
    public GameObject swordTrail;

    void Start()
    {
        SwordColliderClose();
        canAttack = true;
        combo = 0;        
        IsAttacking = false;
        animator=gameObject.GetComponent<Animator>();
        swordTrail.GetComponent<ParticleSystem>().Stop();
    }

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canAttack == true)
        {
            Debug.Log("Attacking..");
            animator.SetTrigger("Attack");
        }
    }
    void Combo()
    {
        comboTimer += 1 * Time.deltaTime;
        if (Input.GetMouseButtonDown(0))
        {
            if (combo == 0)
            {
                animator.Play("Hit1");
                combo++;
                comboTimer = 0;
            }
            else if (combo == 2 && comboTimer <= 1.5f)
            {
                animator.Play("Hit2");
                combo++;
            }
            else if (combo == 3 && comboTimer <= 1.5f)
            {
                animator.Play("Hit3");
                combo = 0;
            }
        }
    }
    public void SwordColliderOpen()
    {
        weaponCollider.enabled = true;
        swordTrail.GetComponent<ParticleSystem>().Play();
    }
    public void SwordColliderClose()
    {
        weaponCollider.enabled = false;
        swordTrail.GetComponent<ParticleSystem>().Stop();

    }
    



}
