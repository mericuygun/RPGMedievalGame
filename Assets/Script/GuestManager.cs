using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Pixelplacement;


public class GuestManager : Singleton<GuestManager>
{
    public TextMeshProUGUI GuestLog1;
    public TextMeshProUGUI GuestLog2;
    public TextMeshProUGUI GuestLog3;
    public int banditValue;
    public int skeletonValue;
    public int orcValue;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Guest1();
        Guest2();
        Guest3();

    }
    void Guest1()
    {
        if (banditValue == 0)
        {
            GuestLog1.text = "1. Done!";
        }
        else
        {
            GuestLog1.text = "1. Kill " + banditValue + " Bandit";
        }
    }
    void Guest2()
    {
        if (skeletonValue == 0)
        {
            GuestLog2.text = "2. Done!";
        }
        else
        {
            GuestLog2.text = "2. Kill " + skeletonValue + " Skeleton";
        }
    }
    void Guest3()
    {
        if (orcValue == 0)
        {
            GuestLog3.text = "3. Done!";
        }
        else
        {
            GuestLog3.text = "3. Kill " + orcValue + " Orc";
        }
    }


}
