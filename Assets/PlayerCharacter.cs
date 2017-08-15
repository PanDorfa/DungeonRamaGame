using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character {
    // remove this
    public GameObject drop;

    public int[] Seeds = new int[] { 0, 1, 1 };
    public static PlayerCharacter current;
    [SerializeField] private Collider2D attackCollider;
    [HideInInspector] public bool CarryingWater {
        set { _carryingWater = value;
            drop.SetActive(value);
        }
        get { return _carryingWater; }
    }
    private bool _carryingWater;
    [SerializeField] private float wateringRange;

    private void WaterCrops() {
        RaycastHit2D[] _nearbyObjects = Physics2D.CircleCastAll(rigidbody.position, wateringRange, Vector2.zero);
        PlantCrop _currentCrop;
        for (int i = 0; i < _nearbyObjects.Length; i++) {
            if (_currentCrop = _nearbyObjects[i].transform.GetComponent<PlantCrop>()) {
                _currentCrop.WaterThePlant();
                print(_currentCrop.name + " watered");
                CarryingWater = false;
            }
        }
    }

    new private void Start() {
        current = this;
        base.Start();
        StartCoroutine(ListenForInput());
        attackReady = true; 
    }
    
    private IEnumerator ListenForInput() {
        while (true) {
            MoveTowards(rigidbody.position + new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
            if (Input.GetButton("Jump") && attackReady) {
                attackCollider.offset = new Vector2(Mathf.RoundToInt(direction.x), (Mathf.Abs(Mathf.RoundToInt(direction.x))==1)?0: Mathf.RoundToInt(direction.y));
                attackReady = false;
                StartCoroutine(Attack());
            }
            if (Input.GetKeyDown(KeyCode.F) && CarryingWater) {
                WaterCrops();
            }

            yield return null;
        }
    }

        ICanTakeDamage _newTarget;
    private void OnTriggerEnter2D(Collider2D collision) {
        if ((_newTarget = collision.GetComponent<ICanTakeDamage>()) != null) {
            targets.Add(_newTarget);
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if ((_newTarget = collision.GetComponent<ICanTakeDamage>()) != null) {
            targets.Remove(_newTarget);
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(rigidbody.position, wateringRange);
    }
}