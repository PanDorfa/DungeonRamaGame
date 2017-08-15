using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Made by David Malaky
public class SeedPlacerButton : MonoBehaviour, IPointerClickHandler {
    public GameObject SeedToSpawn;
    public PlantSpot ActiveSpot;
    #region Variables
    private void Start() {
        transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = SeedToSpawn.name;
        ActiveSpot = transform.parent.parent.parent.GetComponent<PlantSpot>();
    }
    #endregion

    // Methods here
    public void OnPointerClick(PointerEventData eventData) {
        ActiveSpot.PlaceSeed(SeedToSpawn);
    }
}

public interface ICanTakeDamage {
    void TakeDamage(int _damage, GameObject _source);
}