using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;
using DG.Tweening;
using UnityEngine.AI;

public class Bandit : MonoBehaviour
{

    public EnemyUI enemyui;
    public Animator enemyAnimator;
    public bool IsBanditAlive;
    NavMeshAgent navmeshagent;

    [Header("Wandering AI")]
    public float moveSpeed = 3f;
    public float rotSpeed = 100f;
    public bool isWandering = false;
    private bool isRotatingLeft = false;
    private bool isRotatingRight = false;
    private bool isWalking = false;
    [Header("Attack & Chase")]
    public float distanceByPlayer;
    public float minDistance;
    public float maxDistance;
    bool isAttackable;
    void Start()
    {
        navmeshagent = GetComponent<NavMeshAgent>();
        IsBanditAlive = true;
    }


    void Update()
    {
        HealthSystem();
        if (IsBanditAlive == true)
        {
            Wandering();
            Attacking();
            Chasing();
        }
        
    }

    void HealthSystem()
    {
        if (enemyui.health <= 0 && IsBanditAlive == true)
        {
            
            enemyui.Death();
            GuestManager.Instance.banditValue--;
            IsBanditAlive = false;
            isWandering = true;
            moveSpeed = 0;
            SpawnManager.Instance.currentBanditAmount--;
            Object.Destroy(gameObject, 6);
        }
    }
    void Attacking()
    {
        if (isAttackable == true)        {
            
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            gameObject.GetComponent<Animator>().Play("attack");            
            //GetComponent<Animator>().SetBool("attackable", true);
            if (distanceByPlayer + 1 >= minDistance)
            {
                isAttackable = false;
            }
        }
    }
    void Chasing()
    {
        distanceByPlayer = Vector3.Distance(transform.position, PlayerController.Instance.playerObject.transform.position);
        if (enemyui.underAttack == true)
        {
            //gameObject.transform.DOLookAt(PlayerController.Instance.playerObject.transform.position, 0.5f, AxisConstraint.Y);
            
            if (distanceByPlayer <= maxDistance &&isAttackable==false)
            {
                //transform.position += transform.forward * moveSpeed * Time.deltaTime;
                navmeshagent.destination = PlayerController.Instance.playerObject.transform.position;
                if (distanceByPlayer<=minDistance)
                {
                    isAttackable = true;
                }
                else
                {
                    GetComponent<Animator>().SetBool("attackable", false);
                    isAttackable = false;
                    GetComponent<Animator>().SetBool("walk", true);
                    //gameObject.GetComponent<Animator>().Play("waalk");
                    GetComponent<Rigidbody>().isKinematic = false;
                }
            }
            else
            {
                //Object.Destroy(gameObject, 1);
                //SpawnManager.Instance.currentBanditAmount--;
            }
        }
    }
    void Wandering()
    {
        if (IsBanditAlive == true && enemyui.underAttack == false)
        {
            if (isWandering == false)
            {
                StartCoroutine(Wander());
            }
            if (isRotatingRight == true)
            {
                gameObject.GetComponent<Animator>().Play("idle");
                GetComponent<Animator>().SetBool("walk", false);
                transform.Rotate(transform.up * Time.deltaTime * rotSpeed);
            }
            if (isRotatingLeft == true)
            {
                GetComponent<Animator>().SetBool("walk", false);
                gameObject.GetComponent<Animator>().Play("idle");
                transform.Rotate(transform.up * Time.deltaTime * -rotSpeed);
            }
            if (isWalking == true)
            {
                GetComponent<Animator>().SetBool("walk", true);
                //gameObject.GetComponent<Animator>().Play("waalk");
                transform.position += transform.forward * moveSpeed * Time.deltaTime;
            }
        }
    }
    IEnumerator Wander()
    {
        int rotTime = Random.Range(1, 3);
        int rotateWait = Random.Range(1, 4);
        int rotateLorR = Random.Range(1, 2);
        int walkWait = Random.Range(1, 5);
        int walkTime = Random.Range(1, 6);

        isWandering = true;

        yield return new WaitForSeconds(walkWait);
        isWalking = true;
        yield return new WaitForSeconds(walkTime);
        isWalking = false;
        yield return new WaitForSeconds(rotateWait);
        if (rotateLorR == 1)
        {
            isRotatingRight = true;
            yield return new WaitForSeconds(rotTime);
            isRotatingRight = false;
        }
        if (rotateLorR == 2)
        {
            isRotatingLeft = true;
            yield return new WaitForSeconds(rotTime);
            isRotatingLeft = false;
        }
        isWandering = false;
    }

}
