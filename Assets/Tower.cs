using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

  Transform turretTransform;
  public GameObject bulletPrefab;

  public float fireCooldown = 0.5f;
  float fireCooldownLeft = 0f;

  List<Enemy> enemies;

  // Use this for initialization
  void Start () {
    turretTransform = transform.Find ("Turret");
    enemies = new List<Enemy>();
  }

  // Update is called once per frame
  void Update () {
    Enemy nearestEnemy = null;
    /* shoot at nearest enemy
    float dist = Mathf.Infinity;

    foreach (Enemy e in enemies) {
      float d = Vector2.Distance (this.transform.position, e.transform.position);
      if (nearestEnemy == null || d < dist) {
        nearestEnemy = e;
        dist = d;
      }
    }*/

    // Shoot at first enemy
    foreach (Enemy e in enemies) {
      if (nearestEnemy == null || e.distTraveled > nearestEnemy.distTraveled) {
          nearestEnemy = e;
      }
    }

    if (nearestEnemy == null) {
      //Debug.Log ("No enemies");
      return;
    }

    Vector3 dir = nearestEnemy.transform.position - this.transform.position;

    Quaternion lookRot = Quaternion.LookRotation (dir, Vector3.up);

    // This is weird
    float rotation = lookRot.eulerAngles.x + 90f;
    if (lookRot.eulerAngles.y != 270) {
      rotation = -lookRot.eulerAngles.x - 90f;
    }
    turretTransform.rotation = Quaternion.Euler (0, 0, rotation);

    fireCooldownLeft -= Time.deltaTime;
    if (fireCooldownLeft <= 0) {
      fireCooldownLeft = fireCooldown;
      Shoot(new Vector2(dir.x, dir.y), rotation);
    }
  }

  void Shoot(Vector2 dir, float rotation) {
    GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, this.transform.position, Quaternion.Euler (0, 0, rotation));

    Bullet b = bulletGO.GetComponent<Bullet>();
    b.dir = dir;
  }

  void OnTriggerEnter2D(Collider2D coll) {
    if (coll.gameObject.tag == "Enemy") {
      enemies.Add(coll.gameObject.GetComponent<Enemy>());
    } 
  }

  void OnTriggerExit2D(Collider2D coll) {
    if (coll.gameObject.tag == "Enemy") {
      enemies.Remove(coll.gameObject.GetComponent<Enemy>());
    } 
  
  }

}
