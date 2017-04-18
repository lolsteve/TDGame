using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

  protected Transform turretTransform;
  public GameObject bulletPrefab;

  public float fireCooldown;
  protected float fireCooldownLeft = 0f;

  public float radius;
  protected Transform radiusSprite;

  public int cost;

  public int bulletDamage = 1;
  public int bulletHealth = 1;

  protected bool placed = false;

  // Use this for initialization
  protected virtual void Start () {
    turretTransform = transform.Find ("Turret");
    radiusSprite = transform.Find ("Radius");
    Vector3 radiusScale = new Vector3(radius*2, radius*2, 1);
    radiusSprite.localScale = radiusScale;
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
        DisableRadius();
      }
    }
    fireCooldownLeft -= Time.deltaTime;
  }

  protected Enemy FindFurthestEnemy() {
    // Create sphere collider of radius
    Enemy furthestEnemy = null;

    Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, radius);
    foreach(Collider2D c in cols) {
      if(c.gameObject.tag == "Enemy") {
        Enemy e = c.gameObject.GetComponent<Enemy>();
        // If we collided with an enemy, check if it's distanced traveled
        if (furthestEnemy == null || e.distTraveled > furthestEnemy.distTraveled) {
          furthestEnemy = e;
        }
      }
    }

    return furthestEnemy;
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

  public void EnableRadius() {
    radiusSprite.gameObject.SetActive(true);
  }

  public void DisableRadius() {
    radiusSprite.gameObject.SetActive(false);
  }

  void OnMouseDown() {
    if (!placed)
      return;
    UpgradeManager um = GameObject.FindObjectOfType<UpgradeManager>();
    um.SelectTower(this);
  }

}
