﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperTower : Tower {

  protected override void Start () {
    base.Start();
    // Just show a small radius
    Vector3 radiusScale = new Vector3(1f, 1f, 1f);
    radiusSprite.localScale = radiusScale;
  }

  // Update is called once per frame
  protected override void Update () {
    base.Update();

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

    // Sniper bullets instantly hit the enemy
    e.TakeDamage(bulletDamage);
  }
}
