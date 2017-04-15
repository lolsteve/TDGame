using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

  public float speed = 10f;
  public Vector2 dir;

	// Use this for initialization
	void Start () {
    dir = dir.normalized;
	}
	
	// Update is called once per frame
	void Update () {
    float distThisFrame = speed * Time.deltaTime;

    this.transform.Translate( dir * distThisFrame, Space.World );
	}

  void OnTriggerEnter2D(Collider2D coll) {
    Debug.Log("hit!");
    if (coll.gameObject.tag == "Enemy") {
      Destroy(gameObject);
    }
  }
}
