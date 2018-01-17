using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBullet : Bullet {

	protected void OnTriggerEnter2D(Collider2D coll) {
		switch (coll.gameObject.tag)
		{
		  case "Enemy":
			if (!colliding) {
			  colliding = true;
			  coll.gameObject.GetComponent<Enemy>().freeze();
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
