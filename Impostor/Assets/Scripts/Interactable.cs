using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour {
	

	private bool mousePressLimit = false;
	private UIManager UIManager;

	// Object Descriptor
	public string objectName;
	public string objectDescription;
	private Sprite spr;

	// Use this for initialization
	void Start () {
		UIManager = GameObject.Find ("UIManager").GetComponent<UIManager> ();
		spr = GetComponent<SpriteRenderer> ().sprite;
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void OpenDescriptionWindow() {
		UIManager.EnableUI(objectName, objectDescription, spr);
	}
}
