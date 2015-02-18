using UnityEngine;
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
			letterCount += 1;
			if(letterCount > baseText.Length)
			{
				letterCount = baseText.Length;
			}

			text.text = baseText.Substring(0, letterCount);
			timer = 0f;

			if(lettersToType > 0)
			{
				lettersToType -= 1;
			}
		}
	}

	void AddPercent(float amount)
	{
		lettersToType += (int)(baseText.Length * amount);
	}
}
