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
	private int skipToIndex = -1;

	private bool waitForInput = false;
	private List<GameObject> chatMessages = new List<GameObject>();

	private int supsicionScore = 0; // Used for the lose condition

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
		if (conversation[conversationIndex][0] == 'M' && conversation[conversationIndex].Length == 2) {
			//Debug.Log ("This is correct");
			DisableButtons();

			string[] content = conversation[conversationIndex+1].Split ('|');
			SendMessage ((string)content[1], (string)content[2], chatIcons[int.Parse (content[0])]);
			conversationIndex = conversationIndex + 2;
			Debug.Log (conversationIndex);
			return;
		}

		// We really don't want to do anything here yet;
		if (conversation[conversationIndex][0] == 'R' && conversation[conversationIndex].Length == 2) {
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
		string[] responseMessage = conversation [conversationIndex - messageIndex].Split ('|');
		msg.GetComponent<ChatMessage>().Initialize ("ball_is_life", responseMessage[0], chatIcons[0]);
		
		int choiceSuspicion = int.Parse (responseMessage [1]);
		supsicionScore += choiceSuspicion;
		Debug.Log ("Current Suspision level: " + choiceSuspicion);

		// Chat Skip, if we chose an element that caused us to skip further ahead
		// Unintended side effect i guess, we can also go backwards
		if (responseMessage.Length == 3) {
			skipToIndex = int.Parse (responseMessage[2]);
		}

		else {
			skipToIndex = -1;
		}

		PushChatUp ();
		chatMessages.Add (msg);
		DisableButtons ();

		Invoke ("IncreaseChatIndex", Random.Range (2.5f, 3.5f));
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
			message.GetComponent<RectTransform>().anchoredPosition = new Vector2(message.GetComponent<RectTransform>().anchoredPosition.x, message.GetComponent<RectTransform>().anchoredPosition.y + 100f);
		}
	}

	// Function we can invoke, hacky but idgaf
	private void IncreaseChatIndex() {
		if (skipToIndex != -1) {
			conversationIndex = skipToIndex;
		}

		else {
			conversationIndex++;
		}
	}
	// BUTTON MANAGER STUFF
	// THIS SHOULD BE A SEPARATE SCRIPT BUT IDGAF.
	private void EnableButtons() {
		//Debug.Log ("Enabling buttons");
		foreach (GameObject button in buttons) {
			Debug.Log ("Enabling Buttons: " + buttons.Length);
			button.SetActive (true);
			conversationIndex++;
			string[] responseText = conversation[conversationIndex].Split ('|');
			button.GetComponentInChildren<Text>().text = responseText[0];
		}
	}

	private void DisableButtons() {
		foreach (GameObject button in buttons) {
			button.SetActive (false);
		}
	}
}

