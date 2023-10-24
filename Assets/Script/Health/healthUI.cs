using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthUI : MonoBehaviour
{
    public Slider slider;
	public healthSystem healthSystem;

	private void Start() {
		healthSystem = GetComponent<healthSystem>();
		SetMaxHealth(healthSystem.maxHealth);
	}
	private void Update() {
		SetHealth(healthSystem.currentHealth);
	}

	public void SetMaxHealth(float health)
	{
		slider.maxValue = health;
		slider.value = health;
	}

    public void SetHealth(float health)
	{
		slider.value = health;
	}

}
