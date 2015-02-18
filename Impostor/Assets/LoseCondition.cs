﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoseCondition : MonoBehaviour {

	Text text;
	string baseText;
	 
	int letterCount = 0;

	public float timePerLetter = 3f;

	public float minTimePerLetter = .2f;

	float timer = 0f;

	int lettersToType = 0;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text>();
		baseText = text.text;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		if(timer > timePerLetter || (timer > minTimePerLetter && lettersToType > 0))
		{
			letterCount += (int)Mathf.Sign(lettersToType);

			if(letterCount > baseText.Length)
			{
				letterCount = baseText.Length;
			}

			if(letterCount < 0)
			{
				letterCount = 0;
			}

			text.text = baseText.Substring(0, letterCount);
			timer = 0f;

			if(Mathf.Abs(lettersToType) > 0)
			{
				lettersToType -= (int)Mathf.Sign(lettersToType);
			}
		}
	}

	public void AddPercent(float amount)
	{
		lettersToType += (int)(baseText.Length * amount);
	}
}
