using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperencieScript : MonoBehaviour
{
    public Image expImage;
    public float currentExperience, expToNextLevel;

    public static ExperencieScript instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }


    void Start()
    {
        expImage.fillAmount = currentExperience / expToNextLevel;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void expModifier(float experience)
    {

        currentExperience += experience;
        expImage.fillAmount = currentExperience / expToNextLevel;

        if (currentExperience >= expToNextLevel)
        {
            expToNextLevel = expToNextLevel * 1;
            currentExperience = 0;

            PlayerHealth.instance.maxHealth += 10f;

            print("subir nivel");
        }
        
        
        
    }
}
