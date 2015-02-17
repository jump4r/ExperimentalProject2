using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public GameObject UIObjectTitle;
	public GameObject UIObjectDescription;
	public GameObject UIObjectSprite;

	// Use this for initialization
	void Start () {
		UIObjectTitle.SetActive (false);
		UIObjectDescription.SetActive (false);
		UIObjectSprite.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			DisableUI();
		}
	}

	public void EnableUI(string title, string desc, Sprite img) {
		UIObjectTitle.SetActive (true);
		UIObjectDescription.SetActive (true);
		UIObjectSprite.SetActive (true);

		Debug.Log (title);

		UIObjectTitle.GetComponent<Text> ().text = title;
		UIObjectDescription.GetComponent<Text> ().text = desc;
		UIObjectSprite.GetComponent<Image> ().sprite = img; 
	}

	private void DisableUI() {
		UIObjectTitle.SetActive (false);
		UIObjectDescription.SetActive (false);
		UIObjectSprite.SetActive (false);
	}

}
