using UnityEngine;
using System.Collections;

public class ObstacleControl : MonoBehaviour {
	
	Rigidbody rb;

	void Start() {
		rb = GetComponent<Rigidbody>();
	}

	void OnMouseUp() {
		// ctrl click to select obstacles
		// if (Input.GetButton("Fire1")) {
		if (Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.LeftControl)) {
			Director.SelectObstacle(rb);
		}
	}
}
