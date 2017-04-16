using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

  GameObject pathGO;

  Transform targetPathNode;
  int pathNodeIndex = 0;

  public float speed = 2f;

  public int health = 1;

  public float distTraveled { get; private set; }

  // Use this for initialization
  void Start () {
    pathGO = GameObject.Find ("Path");
    distTraveled = 0;
  }

  // Update is called once per frame
  void Update () {
    if (targetPathNode == null) {
      GetNextPathNode ();
      if (targetPathNode == null) {
        // end of path?
        ReachedGoal();
        return;
      }
    }

    Vector2 dir = targetPathNode.position - this.transform.localPosition;

    float distThisFrame = speed * Time.deltaTime;
    distTraveled += distThisFrame;

    if (dir.magnitude <= distThisFrame) {
      // Reached target node
      targetPathNode = null;
    } else {
      // Move enemy
      this.transform.Translate( dir.normalized * distThisFrame, Space.World );
    }


  }

  void GetNextPathNode () {
    if (pathNodeIndex >= pathGO.transform.childCount) {
      ReachedGoal ();
    } else {
      targetPathNode = pathGO.transform.GetChild (pathNodeIndex++);
    }
  }

  void ReachedGoal () {
    Destroy (gameObject);
  }

  public void TakeDamage(int amount) {
    health -= amount;
    if (health <= 0) {
      Destroy(gameObject);
    }
  }

}
