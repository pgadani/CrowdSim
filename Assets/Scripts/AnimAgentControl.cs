﻿using UnityEngine;
using System.Collections;

public class AnimAgentControl : MonoBehaviour {

	private NavMeshAgent agent;
	private Animator animator;
	private int state;
	private static int IDLE = 0; // no target
	private static int WALK = 1;
	// private static int RUN = 2;
	private static float WALK_SPEED = 1.5f;
	private static float WALK_ANG = 101.3f;
	private static float RUN_SPEED = 5.6f;
	private static float RUN_ANG = 132.9f;

	private float maxAng;
	private float smoothAngle;
	private float angularSpeed;

	void Start() {
		animator = GetComponent<Animator>();
		agent = GetComponent<NavMeshAgent>();
		agent.updatePosition = false;
		agent.updateRotation = false;
		state = IDLE;
		smoothAngle = 0;
		maxAng = WALK_ANG;
	}
	
	void Update() {
		Vector2 velocity = new Vector2(agent.desiredVelocity.x, agent.desiredVelocity.z);		
		if (agent.remainingDistance < agent.radius) {
			animator.SetFloat("Speed", 0);
			animator.SetFloat("Direction", 0);
			animator.SetFloat("AngularSpeed", 0);
			// animator.SetBool("Move", false);
			smoothAngle = 0;
			angularSpeed = 0;
		}
		else if (Time.deltaTime > 0.01f && velocity.magnitude > 0.0f) {
			Vector2 orientation = new Vector2(transform.forward.x, transform.forward.z);
			orientation.Normalize();

			float forwardSpeed = Vector2.Dot(orientation, velocity);

			float angle = Vector2.Angle(orientation, velocity);
			// make the angle negative if necessary
			Vector3 cross = Vector3.Cross(orientation, velocity);
			if (cross.z > 0) {
				angle = -angle;
			}

			smoothAngle = Mathf.SmoothDampAngle(smoothAngle, angle, ref angularSpeed, 0.3f, maxAng); // 0.7f

			// animator.SetBool("Move", true);
			animator.SetFloat("Speed", forwardSpeed);
			animator.SetFloat("Direction", angle);
			animator.SetFloat("AngularSpeed", angularSpeed);
			print(angle+" "+angularSpeed);
		}

		AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
		if (state.IsName("Base.IdlePivot") || state.IsName("Base.PlantNTurnLeft") || state.IsName("Base.PlantNTurnRight")) {
			smoothAngle = 0;
			angularSpeed = 0;
		}

		agent.nextPosition =  transform.position + 0.6f*(agent.nextPosition - transform.position); // originally 0.9

		// if (agent.remainingDistance > agent.radius && Time.deltaTime > 1e-5f) {
		// 	Vector3 worldDelta = agent.nextPosition - transform.position;
		// 	float dx = Vector3.Dot(transform.right, worldDelta);
		// 	float dy = Vector3.Dot(transform.forward, worldDelta);
		// 	Vector2 delta = new Vector2(dx, dy);
		// 	float smooth = Mathf.Min(1.0f, Time.deltaTime/0.15f);
		// 	smoothDelta = Vector2.Lerp(smoothDelta, delta, smooth);
		// 	float forwardSpeed = smoothDelta.y / Time.deltaTime;
		// 	float angle = Mathf.Atan2(smoothDelta.y, smoothDelta.x) * Mathf.Rad2Deg;
		// 	// make the angle negative if necessary
		// 	// Vector3 cross = Vector3.Cross(transform.forward, smoothDelta);
		// 	// if (cross.z > 0) {
		// 	// 	angle = -angle;
		// 	// }
		// 	animator.SetFloat("Speed", forwardSpeed);
		// 	animator.SetFloat("Direction", angle);
		// 	animator.SetFloat("AngularSpeed", angle/0.7f);
		// }
		// else if (agent.remainingDistance < agent.radius) {
		// 	animator.SetFloat("Speed", 0);
		// 	animator.SetFloat("Direction", 0);
		// 	animator.SetFloat("AngularSpeed", 0);
		// }
	}

	void onAnimatorMove() {
		transform.position = agent.nextPosition;
	}

	public void ChangeState() {
		state = (state + 1) % 3;
		if (state == IDLE) {
			agent.ResetPath();
		}
		else if (state == WALK) {
			agent.speed = WALK_SPEED;
			maxAng = WALK_ANG;
		}
		else {
			agent.speed = RUN_SPEED;
			maxAng = RUN_ANG;
		}
	}

	public void SetDestination(Vector3 destination) {
		agent.SetDestination(destination);
	}

}
