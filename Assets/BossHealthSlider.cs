using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthSlider : MonoBehaviour
{
    EnemyHealth bossHealth;
    Slider slider;

    void OnEnable()
    {
        slider = GetComponent<Slider>();
    }

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        if (isActiveAndEnabled)
        {
            if (bossHealth != null)
            {
                slider.value = healthToPercent(bossHealth.currentHealth);
            }
            else
            {
                var boss = GameObject.Find("Boss(Clone)");
                if (boss != null)
                {
                    slider.gameObject.SetActive(true);
                    bossHealth = boss.GetComponent<EnemyHealth>();
                }
                else
                {
                    slider.gameObject.SetActive(false);
                }
            }
        }
    }

    float healthToPercent(float health)
    {
        return 100 * health / bossHealth.startingHealth;
    }
}
