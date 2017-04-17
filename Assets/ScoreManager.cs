using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

  public int money = 650;
  public int lives = 200;

  public Text moneyText;
  public Text livesText;

  // Use this for initialization
  void Start () {


  }

  // Update is called once per frame
  void Update () {
    moneyText.text = "Money: $"	+ money;
    livesText.text = "Lives: " + lives;
  }

  public void LoseLife(int l = 1) {
    lives -= l;
    if (lives <= 0) {
      GameOver();
    }
  }

  public void GameOver() {
    Debug.Log("Game Over");
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
  }
}
