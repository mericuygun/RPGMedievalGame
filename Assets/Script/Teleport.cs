using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Teleport : MonoBehaviour
{
    public GameObject spawnLocation;
    public GameObject banditLocation;
    public GameObject zombieLocation;
    public GameObject goblinLocation;
    public GameObject TeleportScrollUI;
    public ParticleSystem teleportEffect;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            TeleportScrollUI.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
        }
    }
    public void TeleportToSpawn()
    {
        PlayerController.Instance.playerObject.transform.position = spawnLocation.transform.position;
        if (HealthSystem.Instance.IsAlive == false)
        {
            Cursor.lockState = CursorLockMode.Locked;
            DOTween.KillAll();
            teleportEffect.Play();
            HealthSystem.Instance.health = 100;
            HealthSystem.Instance._health = 100;                             
            Debug.Log("HOPA");            
            HealthSystem.Instance.deathScreenText.DOFade(0, 0);
            HealthSystem.Instance.deathScreenImage.DOFade(0, 0);
            HealthSystem.Instance.deathScreenBloodyImage.DOFade(0, 0);
            HealthSystem.Instance.restartGame.GetComponent<Image>().DOFade(0, 0);
            HealthSystem.Instance.restartGameText.DOFade(0, 0);
            HealthSystem.Instance.restartGame.gameObject.SetActive(false);
            HealthSystem.Instance.mainCanvas.gameObject.SetActive(true);
            HealthSystem.Instance.IsAlive = true;
            
        }
        TeleportScrollUI.gameObject.SetActive(false);
    }
    public void TeleportToBandit()
    {
        teleportEffect.Play();
        PlayerController.Instance.playerObject.transform.position = banditLocation.transform.position;
        TeleportScrollUI.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void TeleportToZombie()
    {
        teleportEffect.Play();
        PlayerController.Instance.playerObject.transform.position = zombieLocation.transform.position;
        TeleportScrollUI.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void TeleportToGoblin()
    {
        teleportEffect.Play();
        PlayerController.Instance.playerObject.transform.position = goblinLocation.transform.position;
        TeleportScrollUI.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void CloseTeleportScrollUI()
    {
        TeleportScrollUI.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
    
}
