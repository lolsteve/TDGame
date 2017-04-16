using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

  GameObject pathGO;
  SpriteRenderer sprite;

  Transform targetPathNode;
  int pathNodeIndex = 0;

  public float speed = 2f;

  public int health = 1;

  public float distTraveled { get; private set; }

  // Use this for initialization
  void Start () {
    pathGO = GameObject.Find ("Path");
    sprite = GetComponentInChildren<SpriteRenderer>();
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
      return;
    }
    UpdateLevel();
  }

  void UpdateLevel() {
    switch (health) {
      case 1:
        speed = 1.5f;
        sprite.color = new Color(1f, 0f, 0f, 1f); // Red
        break;
      case 2:
        speed = 2f;
        sprite.color = new Color(0f, 0f, 1f, 1f); // Blue
        break;
      case 3:
        speed = 2.5f;
        sprite.color = new Color(0f, 1f, 0f, 1f); // Green
        break;
      default:
        speed = 1.5f;
        sprite.color = new Color(1f, 0f, 0f, 1f); // Red
        break;
    }
  }

}
