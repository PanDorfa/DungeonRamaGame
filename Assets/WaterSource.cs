using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Made by David Malaky
public class WaterSource : MonoBehaviour {

    #region Variables

    #endregion

    // Methods here
    private void OnTriggerStay2D(Collider2D collision) {
        PlayerCharacter.current.CarryingWater = true;
        print("water aquired");
    } 

}