using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour {
    //public Gradient gradient;
    public Slider slider;

    public void SetMaxHealth(float health){
            slider.maxValue = health;
            slider.value = health;
            //healthBar.color = gradient.Evaluate(1f); 
    }


    public void SetHealth(int health){
        slider.value = health;
    }

}
