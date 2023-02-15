using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class SpawnManager : Singleton<SpawnManager>
{
    [Header("BANDIT SETTINGS")]
    public GameObject[] banditSpawnLocations;
    public GameObject banditObject;
    public int currentBanditAmount;
    public int maxBanditAmount;
    public int bRandomNumber;
    bool BanditCanSpawn;

    [Space]
    [Header("SKELETON SETTINGS")]
    public GameObject[] skeletonSpawnLocations;
    public GameObject skeletonObject;
    public int currentSkeletonAmount;
    public int maxSkeletonAmount;
    public int sRandomNumber;
    bool SkeletonCanSpawn;
    [Space]
    [Header("ORC SETTINGS")]
    public GameObject[] orcSpawnLocations;
    public GameObject orcObject;
    public int currentOrcAmount;
    public int maxOrcAmount;
    public int oRandomNumber;
    bool OrcCanSpawn;

    void Start()
    {
        StartCoroutine(SpawnerTimer());
    }

    // Update is called once per frame
    void Update()
    {
        Bandits();
        Skeletons();
        Orcs();
    }
    void SpawnerChoice()
    {
        bRandomNumber = Random.Range(0, 4);
        GameObject banditClone = Instantiate(banditObject, banditSpawnLocations[bRandomNumber].transform.position, banditSpawnLocations[bRandomNumber].transform.rotation);

    }
    IEnumerator SpawnerTimer()
    {
        
        WaitForSeconds waitTime = new WaitForSeconds(5);
        while (true)
        {
            BanditCanSpawn = true;   
            SkeletonCanSpawn=true;
            OrcCanSpawn = true;
            yield return waitTime;
        }
    }
    void Bandits()
    {
        if (currentBanditAmount < maxBanditAmount && BanditCanSpawn == true)
        {            
            SpawnerChoice();
            currentBanditAmount++;
            BanditCanSpawn = false;
        }
    }
    void Skeletons()
    {
        if (currentSkeletonAmount < maxSkeletonAmount && SkeletonCanSpawn == true)
        {            
            sRandomNumber = Random.Range(0, 4);
            GameObject SkeletonClone = Instantiate(skeletonObject, skeletonSpawnLocations[sRandomNumber].transform.position, skeletonSpawnLocations[sRandomNumber].transform.rotation);            
            currentSkeletonAmount++;
            SkeletonCanSpawn = false;
        }
    }
    void Orcs()
    {
        if (currentOrcAmount < maxOrcAmount && OrcCanSpawn == true)
        {            
            oRandomNumber = Random.Range(0, 3);
            GameObject OrcClone = Instantiate(orcObject, orcSpawnLocations[oRandomNumber].transform.position, orcSpawnLocations[oRandomNumber].transform.rotation);
            currentOrcAmount++;
            OrcCanSpawn = false;
        }
    }
}
