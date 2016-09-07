using UnityEngine;
using System.Collections;

public class ObstacleControl : MonoBehaviour {
	
	Rigidbody rb;

	void Start() {
		rb = GetComponent<Rigidbody>();
	}

	void OnMouseUp() {
		// click to select obstacles
		Director.SelectObstacle(rb);
	}
}
