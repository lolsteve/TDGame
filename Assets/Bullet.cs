using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

  public float speed = 10f;
  public int damage = 1;
  public Vector2 dir;
  bool colliding = false;
  int health = 1;

  // Use this for initialization
  void Start () {
    dir = dir.normalized;
  }

  // Update is called once per frame
  void Update () {
    colliding = false;
    float distThisFrame = speed * Time.deltaTime;

    this.transform.Translate( dir * distThisFrame, Space.World );
  }

  void OnTriggerEnter2D(Collider2D coll) {
    switch (coll.gameObject.tag)
    {
      case "Enemy":
        if (!colliding) {
          colliding = true;
          coll.gameObject.GetComponent<Enemy>().TakeDamage(damage);
          if (--health <= 0) {
            Destroy(gameObject);
          }
        }
        break;
      case "BulletWall":
        Destroy(gameObject);
        break;
      default:
        Debug.Log("Unknown collision " + coll.gameObject.tag);
        break;
    }
  }

}
