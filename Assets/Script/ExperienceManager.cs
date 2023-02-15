using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Pixelplacement;
using DG.Tweening;

public class ExperienceManager : Singleton<ExperienceManager>
{
    public Slider expSlider;
    public TextMeshProUGUI levelText;
    public float experienceValue;
    public int level=0;
    public int nextLevel;
    public float reqExp;
    public float currentExp;
    public GameObject levelUpEffect;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LevelUp();
        ExpBar();
    }
    void LevelUp()
    {
        if (currentExp >= reqExp)
        {
            levelUpEffect.GetComponent<ParticleSystem>().Play();
            level++;
            nextLevel = level + 1;
            currentExp = 0;
            reqExp = 50 * (level + 1);
        }
          
        
    }
    void ExpBar()
    {
        levelText.text = "Level :" + level;
        expSlider.DOValue(currentExp, 0.5f);
        //expSlider.value = currentExp;
        expSlider.maxValue = reqExp;
    }
    public void GainExp(float gainedExp)
    {
        currentExp += gainedExp;
    }
}
