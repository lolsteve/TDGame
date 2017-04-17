using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour {

  // Use this for initialization
  void Start () {

  }

  // Update is called once per frame
  void Update () {

  }

  public void BuildTower(GameObject prefab) {
    ScoreManager sm = GameObject.FindObjectOfType<ScoreManager>();
    Tower t = prefab.GetComponent<Tower>();
    if (sm.money >= t.cost) {
      sm.money -= t.cost;
      Instantiate(prefab, new Vector3(0,0,0), new Quaternion(0,0,0,0));
    }
  }
}
