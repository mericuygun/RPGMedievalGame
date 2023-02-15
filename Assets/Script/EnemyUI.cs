using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Pixelplacement;
using DG.Tweening;

public class EnemyUI : MonoBehaviour
{
    [Header("Enemy Settings")]
    public string enemyName;
    public float health;
    public int enemyLevel;
    public float damage;
    [Range(0, 10)]
    public int missChange;
    public float experience;
    public bool underAttack;

    [Space]
    [Space]

    public Slider enemyHpSlider;
    public TextMeshProUGUI enemyHpText;
    public TextMeshProUGUI enemyInfo;
    public Canvas mainCanvas;    
    public Animator animator;
    public GameObject deathEffect;
    public float fakeHp;
    float attackTimer;
    public int hitormiss;



    void Start()
    {
        underAttack = false;


    }

    void Update()
    {
        attackTimer += 1 * Time.deltaTime;
        DisplayInfos();
    }
    public void GetDamage(float damage)
    {
        Debug.Log("Vurduðun Hasar : " + damage);
        fakeHp = health - damage;
        enemyHpSlider.DOValue(fakeHp, 0.5f);
        health -= damage;
        animator.Play("Damaged");
        if (underAttack == false)
        {
            underAttack = true;
            Debug.Log("Saldýrý Altýndayým");
        }
    }
    public void Death()
    {
        if (health <= 0)
        {
            animator.Play("Die");
            deathEffect.GetComponent<ParticleSystem>().Play();
            ExperienceManager.Instance.GainExp(experience);
            health = 0;
            underAttack = false;
            enemyInfo.gameObject.SetActive(false);
            enemyHpText.gameObject.SetActive(false);
            enemyHpSlider.gameObject.SetActive(false);
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<CapsuleCollider>().enabled = false;

        }
    }
    void DisplayInfos()
    {
        enemyHpText.text = health.ToString();
        enemyInfo.text = "Lvl " + enemyLevel + "  " + enemyName;
        mainCanvas.transform.LookAt(Camera.main.transform);
    }
    public void DamageDetermine()
    {
        hitormiss = Random.Range(0, 11);
        if (hitormiss <= 10 - missChange)
        {
            Damage(damage);
            attackTimer = 0;
        }
        else
        {
            attackTimer = 0;
        }


    }
    public void Damage(float enemyDamage)
    {
        HealthSystem.Instance._health -= Random.Range(enemyDamage - 5, enemyDamage + 5);
        HealthSystem.Instance.LoseHp.Play();
    }


}
