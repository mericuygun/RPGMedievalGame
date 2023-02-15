using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;

public class Skeleton : MonoBehaviour
{
    public float speedRigidBody;
    public float speed2;
    public float moveSpeed = 3f;
    public float rotSpeed = 100f;
    public bool isWandering = false;
    private bool isRotatingLeft = false;
    private bool isRotatingRight = false;
    private bool isWalking = false;
    public bool IsSkeletonAlive;
    public EnemyUI enemyui;
    [Header("Attack & Chase")]
    public float distanceByPlayer;
    public float minDistance;
    public float maxDistance;
    bool isAttackable;
    NavMeshAgent navmeshagent;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        navmeshagent = GetComponent<NavMeshAgent>();
        IsSkeletonAlive = true;
    }
    void Update()
    {
        speedRigidBody = rb.velocity.magnitude; // speedrigidbody ve speed2 için 0.3-0.4 deðerlerini görüyorlar yürürken hýzlarýna göre bir animasyon baþlatma yapýlabilir.
        speed2 = navmeshagent.velocity.magnitude;
        Wandering();
        HealthSystem();
        if (IsSkeletonAlive == true)
        {
            Wandering();
            Attacking();
            Chasing();
        }
    }
    void HealthSystem()
    {
        if (enemyui.health <= 0 && IsSkeletonAlive == true)
        {
            IsSkeletonAlive = false;
            GuestManager.Instance.skeletonValue--;
            isWandering = true;
            moveSpeed = 0;
            SpawnManager.Instance.currentSkeletonAmount--;
            enemyui.Death();
            Object.Destroy(gameObject, 6);
        }
    }
    void Attacking()
    {
        if (isAttackable == true)
        {

            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            GetComponent<Animator>().ResetTrigger("walk");
            gameObject.GetComponent<Animator>().Play("attack"); 
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
            if (distanceByPlayer <= maxDistance && isAttackable == false)
            {
                navmeshagent.destination = PlayerController.Instance.playerObject.transform.position;
                //transform.position += transform.forward * moveSpeed * Time.deltaTime;
                if (distanceByPlayer <= minDistance)
                {
                    isAttackable = true;
                }
                else
                {
                    //GetComponent<Animator>().SetBool("attackable", false);
                    isAttackable = false;                    
                    GetComponent<Animator>().SetTrigger("walk");
                    //GetComponent<Animator>().SetBool("walk", true);
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
        if (IsSkeletonAlive == true)
        {
            if (isWandering == false)
            {
                StartCoroutine(Wander());
            }
            if (isRotatingRight == true)
            {
                gameObject.GetComponent<Animator>().Play("idle");
                transform.Rotate(transform.up * Time.deltaTime * rotSpeed);
            }
            if (isRotatingLeft == true)
            {
                gameObject.GetComponent<Animator>().Play("idle");
                transform.Rotate(transform.up * Time.deltaTime * -rotSpeed);
            }
            if (isWalking == true)
            {
                gameObject.GetComponent<Animator>().Play("waalk");
                transform.position += transform.forward * moveSpeed * Time.deltaTime;
            }
        }
    }
    IEnumerator Wander()
    {
        int rotTime = Random.Range(1, 3);
        int rotateWait = Random.Range(4, 7);
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
