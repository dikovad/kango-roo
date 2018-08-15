using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour {

	[SerializeField]
	private float moveSpeed = 4f;
	[SerializeField]
	private float rotationSpeed = 20f;

	private Camera mainCamera;
	private Vector3 forward, right;
	private float HorizongalKey => Input.GetAxis("HorizontalKey");
	private float VerticalKey => Input.GetAxis("VerticalKey");
	
	void Start () {

		mainCamera = FindObjectOfType<Camera>();

		forward = Camera.main.transform.forward;
		forward.y = 0;
		forward = Vector3.Normalize(forward);

		right = Quaternion.Euler(new Vector3(0,90,0)) * forward;
	}
	
	void Update () {
		Rotate();
		if(Input.anyKey) {
			Move();
		}
	}
	
	void Rotate() {
		var playerPlane = new Plane(Vector3.up, transform.position);
		var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
		float hitDist = 0.0f;

		if(playerPlane.Raycast(ray, out hitDist)) {
			var targetPoint = ray.GetPoint(hitDist);
			var targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
			targetRotation.x = 0;
			targetRotation.z = 0;

			transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
		}
	}
	void Move() {
		var direction = new Vector3(HorizongalKey, 0, VerticalKey);
		var rightMovement = right * moveSpeed * Time.deltaTime * HorizongalKey;
		var upMovement = forward * moveSpeed * Time.deltaTime * VerticalKey;

		transform.position += rightMovement;
		transform.position += upMovement;
	}
}
