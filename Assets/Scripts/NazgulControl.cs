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
		// click to select agents
		Director.SelectAgent(agent);
	}
}
