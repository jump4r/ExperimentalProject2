using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChatMessage : MonoBehaviour {
	public GameObject chatMessage;
	public GameObject chatName;
	public GameObject chatImage;

	// private ChatManager chatManager;
	private int numCharacters;

	// Use this for initialization
	void Start () {
		RectTransform rt = GetComponent<RectTransform>();
		rt.anchoredPosition = new Vector2(-32f, -171f);
		Debug.Log("Rect Position: " + rt.anchoredPosition);
	}

	public void Initialize(string cn, string cm, Sprite ci) {
		chatName.GetComponent<Text>().text = cn;
		chatMessage.GetComponent<Text>().text = cm;
		chatImage.GetComponent<Image>().sprite = ci;

		numCharacters = chatMessage.GetComponent<Text>().text.Length;
	}
	
	// Update is called once per frame
	void Update () {
		//transform.Translate (Vector3.up);
	}
}
