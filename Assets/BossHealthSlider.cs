using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthSlider : MonoBehaviour
{

    EnemyHealth bossHealth;
    Slider slider;

    void Awake()
    {
        bossHealth = GameObject.Find("Boss").GetComponent<EnemyHealth>();
        slider = GetComponent<Slider>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Boss Health: " + bossHealth.currentHealth);
        slider.value = healthToPercent(bossHealth.currentHealth);
    }

    float healthToPercent(float health)
    {
        return 100 * health / bossHealth.startingHealth;
    }
}
