using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

  [System.Serializable]
    public class WaveComponent {
      public GameObject enemyPrefab;
      public int num;
      [System.NonSerialized]
        public int spawned = 0;
    }

  public WaveComponent[] waveComps;

  float spawnCD = 0.15f;
  float spawnCDleft = 0f;

  // Use this for initialization
  void Start () {

  }

  // Update is called once per frame
  void Update () {
    spawnCDleft -= Time.deltaTime;
    if (spawnCDleft <= 0) {
      spawnCDleft = spawnCD;
      bool didSpawn = false;

      foreach(WaveComponent wc in waveComps) {
        if(wc.spawned < wc.num) {
          Instantiate(wc.enemyPrefab, this.transform.position, this.transform.rotation);
          didSpawn = true;
          wc.spawned++;
          break;
        }
      }

      if(didSpawn == false) {
        Destroy(gameObject);
      }
    }
  }
}
