using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private bool mousePressLimit = false;
	private Camera camera;
	private GameObject dragging;

	private Vector3 dragOffset;

	// Use this for initialization
	void Start () {
		camera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera>();
		dragging = null;
	}
	
	// Update is called once per frame
	void Update () {
		// Get Mouse button for selected objecft
		if (Input.GetMouseButtonDown (0)) {
			if (!mousePressLimit) {
				FireRaycast();
			}
			mousePressLimit = true;
		}
		
		// Reset Mouse Click
		if (Input.GetMouseButtonUp (0)) {
			mousePressLimit = false;
			dragging = null;
			Screen.showCursor = true;
		}

		// Drag Object
		if (dragging != null) {
			DragObject();
		}
	}

	private void FireRaycast() {
		RaycastHit2D rHit = Physics2D.Raycast(new Vector2(camera.ScreenToWorldPoint(Input.mousePosition).x,camera.ScreenToWorldPoint(Input.mousePosition).y), Vector2.zero, 0f);
		if (rHit.collider != null) {
			Debug.Log("Object Hit: " + rHit.collider.name);
			// If we hit an interactable object, we activate the UI
			rHit.collider.gameObject.GetComponent<Interactable>().OpenDescriptionWindow();
			dragging = rHit.collider.gameObject;
			dragOffset = dragging.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
			Screen.showCursor = false;

		}
	}

	private void DragObject() {
		Vector3 curScreenPoint = new Vector3 (Input.mousePosition.x / 341.5f, Input.mousePosition.y / 249.5f, 0);
		Vector3 curPosition = Camera.main.ScreenToWorldPoint (curScreenPoint) + dragOffset;
		dragging.transform.position = curScreenPoint;
	}
}
