using UnityEngine;
using System.Collections;

public class DevilControl : MonoBehaviour {

	public int avoidanceRadius;
	public int pathRadius;

	Vector3 offset;
	NavMeshAgent agent;

	void Start() {
		agent = GetComponent<NavMeshAgent>();
		agent.radius = avoidanceRadius;
		agent.avoidancePriority = Director.AssignDevilPriority();
		offset = new Vector3(0f,(float)agent.height/2,0f);
		agent.destination = agent.transform.position - offset;
		StartCoroutine("Movement");
	}
	
	// void Update() {
	// 	// randomized movement
	// 	Vector3 adjustedPosition = agent.transform.position - offset;
	// 	if ((adjustedPosition - agent.destination).magnitude <= avoidanceRadius) {
	// 		Vector3 randomPoint = adjustedPosition + Random.insideUnitSphere * pathRadius;
	// 		NavMeshHit hit;
	// 		if (NavMesh.SamplePosition(randomPoint, out hit, (float)pathRadius, NavMesh.AllAreas)) {
	// 			agent.destination = hit.position;
	// 		}
	// 	}
	// }

	IEnumerator Movement() {
		for(;;) {
			for (int counter = 0; counter<10; counter++) {
				Vector3 adjustedPosition = agent.transform.position - offset;
				if (adjustedPosition == agent.destination) {
					ChooseRandomLocation(adjustedPosition);
					counter = 0;
				}
				yield return new WaitForSeconds(0.25f);
			}
			ChooseRandomLocation(agent.transform.position - offset);
		}
	}

	void ChooseRandomLocation(Vector3 adjustedPosition) {
		Vector3 randomPoint = adjustedPosition + Random.insideUnitSphere * pathRadius;
		NavMeshHit hit;
		if (NavMesh.SamplePosition(randomPoint, out hit, (float)pathRadius, NavMesh.AllAreas)) {
			agent.destination = hit.position;
		}
	}
}
