using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Director : MonoBehaviour {

	public Camera cam;
	public int speed;

	public static int devilPriority = 10;
	public static int nazgulPriority = 0;
	public static int agentPriority = 20;

	private static bool hasTarget;
	private static Vector3 target;
	private static List<NavMeshAgent> selected;
	private static bool movedObstacles; 
	private static List<Rigidbody> obstacles;

	void Start() {
		hasTarget = false;
		selected = new List<NavMeshAgent>();
		movedObstacles = false;
		obstacles = new List<Rigidbody>();
	}

	void Update() {
		// click without pressing ctrl to select target location
		if (Input.GetMouseButtonDown(0) && !Input.GetButton("Fire1")) {
			RaycastHit hit;
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit)) {
				target = hit.point;
				hasTarget = true;
			}
		}
		if (hasTarget) {
			foreach (NavMeshAgent a in selected) {
				a.destination = target;
			}
		}
	}

	void FixedUpdate() {
		if (obstacles.Count>0) {
			float x = Input.GetAxis("Horizontal");
			float y = Input.GetAxis("Vertical");
			if (x!=0 || y!=0) {
				movedObstacles = true;
				Vector3 movement = new Vector3(x,0,y);
				foreach (Rigidbody rb in obstacles) {
					rb.MovePosition(rb.transform.position - movement * speed * Time.deltaTime);				
				}
			}
		}
	}

	public static int AssignDevilPriority() {
		devilPriority++;
		return devilPriority;
	}

	public static int AssignNazgulPriority() {
		nazgulPriority++;
		return nazgulPriority;
	}

	public static int AssignAgentPriority() {
		agentPriority++;
		return agentPriority;
	}

	public static void SelectAgent(NavMeshAgent agent) {
		if (hasTarget) {
			hasTarget = false;
			// foreach (NavMeshAgent a in selected) {
			// 	a.Stop();
			// }
			selected = new List<NavMeshAgent>();
		}
		selected.Add(agent);
	}

	public static void SelectObstacle(Rigidbody rb) {
		if (movedObstacles) {
			movedObstacles = false;
			obstacles = new List<Rigidbody>();
		}
		obstacles.Add(rb);
	}

}