﻿using UnityEngine;
using System.Collections;

public class AgentControl : MonoBehaviour {
	
	NavMeshAgent agent;

	void Start() {
		agent = GetComponent<NavMeshAgent>();
		agent.avoidancePriority = Director.AssignAgentPriority();
	}

	void OnMouseUp() {
		// ctrl click to select agents
		if (Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.LeftControl)) {
			Director.SelectAgent(agent);
		}
	}
}
