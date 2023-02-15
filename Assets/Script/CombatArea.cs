using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;


public class CombatArea : MonoBehaviour
{
    public string areaName;
    public Canvas AreaInfoCanvas;
    public Image areaInfo;
    public TextMeshProUGUI areaNameInfo;
    void Start()
    {
        areaInfo.DOFade(0, 0f);
        areaNameInfo.DOFade(0, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (HealthSystem.Instance.IsAlive == false)
        {
            areaNameInfo.DOFade(0, 0f);
            areaInfo.DOFade(0, 0f);
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            AreaInfoCanvas.gameObject.SetActive(true);
            areaNameInfo.text = areaName;            
            areaInfo.DOFade(1, 3f).SetLoops(2, LoopType.Yoyo);
            areaNameInfo.DOFade(1, 3f).SetLoops(2, LoopType.Yoyo);
            PlayerController.Instance.GetComponent<Rigidbody>().mass = 100000;
            

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            areaNameInfo.DOFade(0, 3f);
            areaInfo.DOFade(0, 3f);
            PlayerController.Instance.GetComponent<Rigidbody>().mass = 120;
        }
    }

    void TurnOffCanvas()
    {
        AreaInfoCanvas.gameObject.SetActive(false);
    }
}
