using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class Sword : MonoBehaviour
{
    public GameObject hitEffect;
    int damage;
    
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            int randomDamage = Random.Range(15, 30);
            damage = randomDamage + (ExperienceManager.Instance.level * 5);
            hitEffect.GetComponent<ParticleSystem>().Play();            
            other.gameObject.GetComponent<EnemyUI>().GetDamage(damage);


        }
    }
    
}
