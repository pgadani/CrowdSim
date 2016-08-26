using UnityEngine;
using System.Collections;

public class NazgulControl : MonoBehaviour {

	public int avoidanceRadius;

	Vector3 offset;
	NavMeshAgent agent;

	void Start () {
		agent = GetComponent<NavMeshAgent>();
		agent.radius = avoidanceRadius;
		agent.avoidancePriority = Director.AssignNazgulPriority();
	}
	
	void OnMouseUp() {
		// ctrl click to select agents
		// if (Input.GetButton("Fire1")) {
		if (Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.LeftControl)) {
			Director.SelectAgent(agent);
		}
	}
}
