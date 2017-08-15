using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Made by David Malaky
public class SeedPlacerCanvas : MonoBehaviour, IPointerExitHandler {

    [SerializeField] GameObject buttonPrefab;
    public GameObject[] SEED;


    #region Variables

    #endregion

    // Methods here
    private void OnDisable() {
        for (int i = 0; i < transform.childCount; i++) {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    private void OnEnable() {
        for (int i = 0; i < PlayerCharacter.current.Seeds.Length; i++) {
            if (PlayerCharacter.current.Seeds[i] > 0)
                Instantiate(buttonPrefab, transform).GetComponent<SeedPlacerButton>(). SeedToSpawn = SEED[i] ;
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        gameObject.SetActive(false);
    }
}