using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Pixelplacement;
using TMPro;

public class HealthSystem : Singleton<HealthSystem>
{
    public float health;
    public float fakeHealth;
    public GameObject playerObject;
    public float _health;
    public Slider playerHpSlider;
    public Slider playerFakeHpSlider;
    public Slider potCd;
    public bool IsAlive;
    public Image deathScreenImage;
    public Image deathScreenBloodyImage;
    public TextMeshProUGUI deathScreenText;
    public Button restartGame;
    public TextMeshProUGUI restartGameText;
    public GameObject v3spawn;
    public GameObject healEffect;
    public float potTimer;
    public ParticleSystem LoseHp;
    public Canvas mainCanvas;
    void Start()
    {
        gameObject.transform.position = v3spawn.transform.position;
        _health = 100;
        IsAlive = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (potTimer <= 5)
        {
            potTimer += 1 * Time.deltaTime;
        }
        potCd.value = 5 - potTimer;

        health = _health;

        Potions();
        playerFakeHpSlider.DOValue(health, 1);
        playerHpSlider.value = health;
        if (_health > 100 && IsAlive == true)
        {
            _health = 100;

        }
        else if (_health <= 0 && IsAlive == true)
        {
            Cursor.lockState = CursorLockMode.Confined;
            mainCanvas.gameObject.SetActive(false);
            restartGame.gameObject.SetActive(true);
            _health = 0;
            Debug.Log("YOU DIED");
            //deathScreenObj.gameObject.SetActive(true);
            deathScreenImage.DOFade(1, 1f);
            deathScreenBloodyImage.DOFade(1, 1f);
            deathScreenText.DOFade(1, 2f).OnComplete(RestartButton);
            
            IsAlive = false;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Fire")
        {
            _health -= Random.Range(7, 11);
            Debug.Log("Ah! yandým.");
        }
    }
    void Potions()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && potTimer >= 5)
        {
            _health += 20;
            healEffect.GetComponent<ParticleSystem>().Play();
            potTimer = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _health -= 20;
        }
    }
    void RestartButton()
    {
        restartGame.GetComponent<Image>().DOFade(1, 1f);
        restartGameText.DOFade(1, 1f);
    }


}
