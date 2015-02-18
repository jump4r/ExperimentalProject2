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

		text.text = baseText.Substring(0, letterCount);
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

		if(letterCount == baseText.Length)
		{
			disableOnLose.SetActive(false);
			CameraZoomOnLose();
			SendMessage();
		}

		if(win)
		{
			disableOnLose.SetActive(false);
			CameraZoomOnLose();
			text.text = "i had fun chatting with you on myface earlier. see u later";
			SendMessage();
		}
	}

	public bool win = true;


	public float sendMessageTime = 20f;
	float sendMessageCount;

	public float delay = 2f;
	float delayCount = 0f;

	public SpriteRenderer spriteRenderer;
	public Sprite pressedButtonSprite;
	public Sprite defaultSprite;
	public GameObject disableOnLose;

	void SendMessage()
	{
		delayCount += Time.deltaTime;
		if(delayCount > delay)
		{
			spriteRenderer.sprite = pressedButtonSprite;

			if(sendMessageCount > sendMessageTime)
			{
				spriteRenderer.sprite  = defaultSprite;
				text.text = "Message Sent to Lola";
			}

			sendMessageCount += Time.deltaTime;
		}
	}

	public float zoomAmountOnLose = 3f;

	void CameraZoomOnLose()
	{
		float z = Camera.main.transform.position.z;

		Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, this.transform.position, 2f * Time.deltaTime);
		Vector3 pos = Camera.main.transform.position;
		Camera.main.transform.position = new Vector3(pos.x, pos.y, z);
		Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, zoomAmountOnLose, 2f * Time.deltaTime);
	}

	public void AddPercent(float amount)
	{
		lettersToType += (int)(baseText.Length * amount);
	}
}
