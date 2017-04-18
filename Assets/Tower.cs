using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

  protected Transform turretTransform;
  public GameObject bulletPrefab;

  public float fireCooldown;
  protected float fireCooldownLeft = 0f;

  public float radius;
  protected CircleCollider2D rangeCollider;
  protected Transform radiusSprite;

  public int cost;

  public int bulletDamage = 1;
  public int bulletHealth = 1;

  protected List<Enemy> enemies;

  protected bool placed = false;

  // Use this for initialization
  protected virtual void Start () {
    turretTransform = transform.Find ("Turret");
    rangeCollider = GetComponent<CircleCollider2D>();
    rangeCollider.radius = radius;
    radiusSprite = transform.Find ("Radius");
    Vector3 radiusScale = new Vector3(radius*2, radius*2, 1);
    radiusSprite.localScale = radiusScale;
    enemies = new List<Enemy>();
  }

  // Update is called once per frame
  protected virtual void Update () {
    if (!placed) {
      // Get mouse position
      Vector3 pos = Input.mousePosition;
      pos.z = 10;
      pos = Camera.main.ScreenToWorldPoint(pos);
      this.transform.position = pos;

      // Place turret if we click
      if(Input.GetMouseButtonDown(0)) {
        placed = true; 
        radiusSprite.gameObject.SetActive(false);
      }
    }
    fireCooldownLeft -= Time.deltaTime;
  }
  
  protected virtual void ShootAt(Enemy e) {
    PointTurretAt(e);
  }

  protected void PointTurretAt(Enemy e) {
    // Find direction to enemy
    Vector3 dir = e.transform.position - this.transform.position;
    // Calculate angle to enemy
    Quaternion lookRot = Quaternion.LookRotation (dir, Vector3.up);

    // Turn turret to enemy
    float rotation = lookRot.eulerAngles.x + 90f;
    if (lookRot.eulerAngles.y != 270) {
      rotation = -lookRot.eulerAngles.x - 90f;
    }
    turretTransform.rotation = Quaternion.Euler (0, 0, rotation);
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
