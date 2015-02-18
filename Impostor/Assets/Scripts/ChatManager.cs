using UnityEngine;
using System.Collections;

public class ChatManager : MonoBehaviour {

	public GameObject chatPrefab;
	public Vector3 chatInitialPosition;
	public GameObject canvas;

	public Sprite[] chatIcons;

	public TextAsset text;
	private string[] conversation;
	private int conversationIndex = 0;

	private bool waitForInput = false;

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
		Debug.Log ("Update Conversation: " + conversation[conversationIndex] + "lll");
		char[] charsToTrim = {' ','\n'};
		conversation[conversationIndex] = conversation[conversationIndex].TrimStart (charsToTrim);
		// Lol we're out of options
		if (conversationIndex >= conversation.Length) {
			// Debug.Log("This is wrong");
			return;
		}

		// Send a 'message' from the other person.
		if (conversation[conversationIndex][0] == 'M') {
			Debug.Log ("This is correct");
			conversationIndex++;
			string[] content = conversation[conversationIndex].Split ('|');
			SendMessage ((string)content[1], (string)content[2], chatIcons[int.Parse (content[0])]);
		}

		// We really don't want to do anything here yet;
		if (conversation[conversationIndex] == "RESPONSE") {
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

	public void SendMessage(string name, string message, Sprite image) {
		GameObject msg = Instantiate(chatPrefab, chatInitialPosition, Quaternion.identity) as GameObject;
		msg.transform.parent = canvas.transform;
		msg.GetComponent<ChatMessage>().Initialize(name, message, image);
	}
}
