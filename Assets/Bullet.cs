using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

  public float speed = 10f;
  public int damage { get; set; }
  public Vector2 dir;
  public bool colliding = false;
  public int health { get; set; }
  public Rigidbody2D rb;

  // Use this for initialization
  protected void Start () {
    dir = dir.normalized;
    rb = GetComponent<Rigidbody2D>();
  }

  // Update is called once per frame
  void Update () {
    colliding = false;
    float distThisFrame = speed * Time.deltaTime;

    //this.transform.Translate( dir * distThisFrame, Space.World );
    rb.MovePosition(rb.position + dir * distThisFrame);
  }

  protected virtual void OnTriggerEnter2D(Collider2D coll) {
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
        break;
    }
  }

}
