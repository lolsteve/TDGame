using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

	Transform turretTransform;

	// Use this for initialization
	void Start () {
		turretTransform = transform.Find ("Turret");
	}
	
	// Update is called once per frame
	void Update () {
		Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>();

		Enemy nearestEnemy = null;
		float dist = Mathf.Infinity;

		foreach (Enemy e in enemies) {
			float d = Vector2.Distance (this.transform.position, e.transform.position);
			if (nearestEnemy == null || d < dist) {
				nearestEnemy = e;
				dist = d;
			}
		}

		if (nearestEnemy == null) {
			Debug.Log ("No enemies");
			return;
		}

		Vector3 dir = nearestEnemy.transform.position - this.transform.position;

		Debug.Log (dir);

		Quaternion lookRot = Quaternion.LookRotation (dir, Vector3.up);

		// This is weird
		if (lookRot.eulerAngles.y == 270) {
			turretTransform.rotation = Quaternion.Euler (0, 0, lookRot.eulerAngles.x + 90f);
		} else {
			turretTransform.rotation = Quaternion.Euler (0, 0, -lookRot.eulerAngles.x - 90f);
		}
	}
}
