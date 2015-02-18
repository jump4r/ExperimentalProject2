using UnityEngine;
using System.Collections;

public class ScrollScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	public float speed = 1f;
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.DownArrow))
		{
			GetComponent<RectTransform>().position -= Time.deltaTime * Vector3.up * speed;
		}

		if(Input.GetKey(KeyCode.UpArrow))
		{
			GetComponent<RectTransform>().position += Time.deltaTime * Vector3.up * speed;
		}

		GetComponent<RectTransform>().position += .5f * Vector3.up * speed * Input.GetAxis("Mouse ScrollWheel");

	}
}
