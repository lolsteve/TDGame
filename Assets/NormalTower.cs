using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalTower : Tower {

  // Update is called once per frame
  protected override void Update () {
    base.Update();

    // Don't do any shooting logic if not placed
    if (!placed)
      return;

    Enemy furthestEnemy = FindFurthestEnemy();

    if (furthestEnemy == null) {
      // No enemies
      return;
    }

    if (fireCooldownLeft <= 0) {
      fireCooldownLeft = fireCooldown;
      ShootAt(furthestEnemy);
    }
  }

  protected override void ShootAt(Enemy e) {
    base.ShootAt(e);

    // Create bullet
    GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, this.transform.position, turretTransform.rotation);
    Bullet b = bulletGO.GetComponent<Bullet>();
    b.dir = e.transform.position - this.transform.position;
    b.damage = bulletDamage;
    b.health = bulletHealth;
  }

}
