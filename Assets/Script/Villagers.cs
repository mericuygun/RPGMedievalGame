using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Villagers : MonoBehaviour
{
    public GameObject[] walkpoints;
    public int selectedPoint;
    public float speed;
    public float waitTime;
    public NavMeshAgent navmeshagent;    
    Animator animator;    
    public bool arrived;
    public float timer;
    public bool timerReset;


    void Start()
    {

        StartCoroutine(WaitNGo());
        arrived = false;
        navmeshagent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        Route();
    }

    // Update is called once per frame
    void Update()
    {
        timer += 1 * Time.deltaTime;
        speed = navmeshagent.velocity.magnitude;
        animator.SetFloat("walkSpeed", speed);
    }
    void Route()
    {
        selectedPoint = Random.Range(0, walkpoints.Length);
        navmeshagent.destination = walkpoints[selectedPoint].transform.position;
    }
    IEnumerator WaitNGo()
    {
        while (true)
        {
            waitTime = Random.Range(23, 42);
            yield return new WaitForSeconds(waitTime);
            Route();
            timer = 0;
        }
    }
}
