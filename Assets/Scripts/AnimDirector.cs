using UnityEngine;
using System.Collections;

public class AnimDirector : MonoBehaviour {


	public Camera cam;

	private bool hasSelected;
	private Vector3 target;
	private AnimAgentControl selected;

	void Start() {
		hasSelected = false;
	}

	void Update() {
		// click to select agents and targets
		// clicking on the agent loops possible states: walking, running (2 clicks), and reseting the selection (removing the destination, 3 clicks)
		if (Input.GetMouseButtonDown(0)) {
			RaycastHit hit;
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit)) {
				Collider collider = hit.collider;
				//check if hit is a player or not with tag
				if (collider.CompareTag("Agent")) {
					hasSelected = true;
					selected = collider.gameObject.GetComponent<AnimAgentControl>();
					selected.ChangeState();
				}
				else {
					target = hit.point;
					if (hasSelected) {
						selected.SetDestination(target);
					}
				}
			}
		}
	}

}