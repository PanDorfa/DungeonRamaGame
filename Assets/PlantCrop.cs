using System;
using System.Collections;
using UnityEngine;
public class PlantCrop : MonoBehaviour, ICanTakeDamage {
    [SerializeField] private float UpdateDelay = 1;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject adultPlant;
    [SerializeField] private float growingRateWateringModifier;
    [SerializeField] private float growingRateDayModifier;
    [SerializeField] private float growingRateNightModifier;
    public float GrowingTimeRemaining;

    [SerializeField] private Vector2 waterLevelOptimal;
    private float waterLevelCurrent;

    private void Start() {
        waterLevelCurrent = waterLevelOptimal.y;
        StartCoroutine(Grow());
    }

    public void WaterThePlant() {
        waterLevelCurrent += 60;
    }

    public bool IsHealthy {
        get { return (waterLevelOptimal.x <= waterLevelCurrent && waterLevelCurrent <= waterLevelOptimal.y); }
    }

    IEnumerator Grow() {
        WaitForSeconds _wait = new WaitForSeconds(UpdateDelay);
        float _growingRate;
        while (GrowingTimeRemaining>0) {
            _growingRate = 1;
            // water check
            if (!(waterLevelOptimal.x <= waterLevelCurrent && waterLevelCurrent <= waterLevelOptimal.y)) {
                if (waterLevelCurrent <= 0 || waterLevelCurrent >= waterLevelOptimal.y * 3) {
                    KillPlant();
                    yield break;
                }
                _growingRate *= growingRateWateringModifier;
            }
            // day & night check
            waterLevelCurrent -= UpdateDelay;
            GrowingTimeRemaining -= _growingRate;
            yield return _wait;
        }
        SpawnAdult();
    }

    private void SpawnAdult() {
        spriteRenderer.color = Color.blue;
    }

    private void KillPlant() {
        spriteRenderer.color = Color.red;
    }

    public void TakeDamage(int _damage, GameObject _source) {
        transform.parent.GetComponent<PlantSpot>().occupied = false;
        Destroy(gameObject);
    }

    private void OnMouseDown() {
        print("Plant is okay: " + IsHealthy);
    }


}