using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour {

	public GameObject chatPrefab;
	public Vector3 chatInitialPosition;
	public GameObject canvas;

	public Sprite[] chatIcons;
	public GameObject[] buttons;

	public TextAsset text;
	private string[] conversation;
	private int conversationIndex = 0;

	private bool waitForInput = false;
	private List<GameObject> chatMessages = new List<GameObject>();

	// Use this for initialization
	void Start () {
		ParseConversation();
		//message.GetComponent<ChatMessage>().Initialize (
	}

	private void ParseConversation() {
		conversation = text.text.Split ('\n');
		//Debug.Log(conversation[1]);
	}

	private void UpdateConversation() {
		Debug.Log ("Update Conversation: " + conversation[conversationIndex]);
		Debug.Log ("Current Conv Index: " + conversationIndex);
		char[] charsToTrim = {' ','\n'};
		conversation[conversationIndex] = conversation[conversationIndex].TrimStart (charsToTrim);
		// Lol we're out of options
		if (conversationIndex >= conversation.Length) {
			Debug.Log("This is wrong");
			return;
		}

		// Send a 'message' from the other person.
		if (conversation[conversationIndex][0] == 'M') {
			//Debug.Log ("This is correct");
			//DisableButtons();

			string[] content = conversation[conversationIndex+1].Split ('|');
			SendMessage ((string)content[1], (string)content[2], chatIcons[int.Parse (content[0])]);
			conversationIndex = conversationIndex + 2;
			Debug.Log (conversationIndex);
			return;
		}

		// We really don't want to do anything here yet;
		if (conversation[conversationIndex][0] == 'R') {
			EnableButtons ();
			return;
		}

		// And we definately don't want to touch this. 
	   	else {
			return;
       	}
	}
	
	// Update is called once per frame
	void Update () {
		UpdateConversation ();
	}
	public void SendPlayerMessage(int messageIndex) {
		Debug.Log ("Sending Player Message");
		GameObject msg = Instantiate (chatPrefab, chatInitialPosition, Quaternion.identity) as GameObject;
		msg.transform.parent = canvas.transform;
		msg.GetComponent<ChatMessage>().Initialize ("Super Nerd", conversation[conversationIndex-messageIndex], chatIcons[0]);
		PushChatUp ();
		chatMessages.Add (msg);
		Invoke ("IncreaseChatIndex", 3f);
	}

	public void SendMessage(string name, string message, Sprite image) {
		GameObject msg = Instantiate(chatPrefab, chatInitialPosition, Quaternion.identity) as GameObject;
		msg.transform.parent = canvas.transform;
		msg.GetComponent<ChatMessage>().Initialize(name, message, image);
		PushChatUp ();
		chatMessages.Add (msg);
	}
	
	private void PushChatUp() {
		foreach (GameObject message in chatMessages) {
			message.GetComponent<RectTransform>().anchoredPosition = new Vector2(message.GetComponent<RectTransform>().anchoredPosition.x, message.GetComponent<RectTransform>().anchoredPosition.y + 70f);
		}
	}

	// Function we can invoke, hacky but idgaf
	private void IncreaseChatIndex() {
		conversationIndex++;
	}
	// BUTTON MANAGER STUFF
	// THIS SHOULD BE A SEPARATE SCRIPT BUT IDGAF.
	private void EnableButtons() {
		//Debug.Log ("Enabling buttons");
		foreach (GameObject button in buttons) {
			Debug.Log ("Enabling Buttons: " + buttons.Length);
			button.SetActive (true);
			conversationIndex++;
			button.GetComponentInChildren<Text>().text = conversation[conversationIndex];
		}
	}

	private void DisableButtons() {
		foreach (GameObject button in buttons) {
			button.SetActive (false);
		}
	}
}

