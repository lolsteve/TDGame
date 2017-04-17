using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

  Transform turretTransform;
  public GameObject bulletPrefab;

  public float fireCooldown = 0.5f;
  float fireCooldownLeft = 0f;

  public float radius = 1;
  CircleCollider2D rangeCollider;
  SpriteRenderer radiusSprite;

  public int cost = 160;

  List<Enemy> enemies;

  bool placed = false;

  // Use this for initialization
  void Start () {
    turretTransform = transform.Find ("Turret");
    rangeCollider = GetComponent<CircleCollider2D>();
    rangeCollider.radius = radius;
    radiusSprite = transform.Find ("Radius").GetComponent<SpriteRenderer>();
    enemies = new List<Enemy>();
  }

  // Update is called once per frame
  void Update () {
    if (!placed) {
      // Get mouse position
      Vector3 pos = Input.mousePosition;
      pos.z = 10;
      pos = Camera.main.ScreenToWorldPoint(pos);
      this.transform.position = pos;

      // Place turret if we click
      if(Input.GetMouseButtonDown(0)) {
        placed = true; 
        radiusSprite.enabled = false;
      }
      // Don't shoot or anything if we aren't placed yet
      return;
    }

    Enemy nearestEnemy = null;
    // Shoot at first enemy
    foreach (Enemy e in enemies) {
      if (nearestEnemy == null || e.distTraveled > nearestEnemy.distTraveled) {
        nearestEnemy = e;
      }
    }

    if (nearestEnemy == null) {
      // No enemies
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
