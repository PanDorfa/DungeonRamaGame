using System.Collections.Generic;
using UnityEngine;

// Made by David Malaky
public class PlantSpot : MonoBehaviour {
    [SerializeField] GameObject canvas;
    public bool occupied = false;
    #region Variables

    #endregion

    // Methods here
    public void PlaceSeed(GameObject prefab) {
        if (!occupied) {
            Instantiate(prefab, transform);
            occupied = true;
            canvas.SetActive(false);
        }
    }

    private void OnMouseDown() {
        if (!occupied) canvas.SetActive(true);
    }
}
