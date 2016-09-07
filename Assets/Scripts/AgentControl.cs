using UnityEngine;
using System.Collections;

public class AgentControl : MonoBehaviour {
	
	NavMeshAgent agent;

	void Start() {
		agent = GetComponent<NavMeshAgent>();
		agent.avoidancePriority = Director.AssignAgentPriority();
	}

	void OnMouseUp() {
		// click to select agents
		Director.SelectAgent(agent);
	}
}
