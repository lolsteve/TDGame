using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

  GameObject pathGO;
  SpriteRenderer sprite;
  SpriteRenderer ice;

  Transform targetPathNode;
  int pathNodeIndex = 0;

  public float speed = 2f;

  public int health = 1;

  public int moneyValue = 1;

  public float distTraveled { get; private set; }

  bool frozen = false;
  float freezeTimer = 0f;


  // Use this for initialization
  void Start () {
    pathGO = GameObject.Find ("Path");
    SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
    foreach(SpriteRenderer s in sprites) {
      switch(s.name) {
        case "Sprite":
          sprite = s;
          break;
        case "Ice":
          ice = s;
          break;
        default:
          Debug.Log("Unknown sprite name: " + sprite.name);
          break;
      }
    }
    distTraveled = 0;
  }

  // Update is called once per frame
  void Update () {
    if (targetPathNode == null) {
      GetNextPathNode ();
      if (targetPathNode == null) {
        // end of path
        return;
      }
    }

    if (frozen) {
      freezeTimer -= Time.deltaTime;
      if (freezeTimer <= 0) {
        frozen = false;
        ice.enabled = false;
      }
    } else {
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

  }

  void GetNextPathNode () {
    if (pathNodeIndex >= pathGO.transform.childCount) {
      targetPathNode = null;
      ReachedGoal ();
    } else {
      targetPathNode = pathGO.transform.GetChild (pathNodeIndex++);
    }
  }

  void ReachedGoal () {
    GameObject.FindObjectOfType<ScoreManager>().LoseLife(health);
    Destroy (gameObject);
  }

  public void TakeDamage(int amount) {
    health -= amount;
    GameObject.FindObjectOfType<ScoreManager>().money += moneyValue;
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

  public void freeze() {
    if (!frozen) {
      frozen = true;
      freezeTimer = 2f;
      ice.enabled = true;
    }
  }

}
