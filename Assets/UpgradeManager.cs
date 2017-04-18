using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour {

  Tower selectedTower;
  public Button deselectButton;

  public void SelectTower(Tower t) {
    // Unhighlight old tower
    if (selectedTower != null) {
      selectedTower.DisableRadius();
    }
    // Highlight new tower
    selectedTower = t; 
    t.EnableRadius();
    // Enable deselect button
    deselectButton.gameObject.SetActive(true);
  }

  public void DeselectTower() {
    // Unhighlight old tower
    if (selectedTower != null) {
      selectedTower.DisableRadius();
    }
    selectedTower = null;
    deselectButton.gameObject.SetActive(false);
  }

}
