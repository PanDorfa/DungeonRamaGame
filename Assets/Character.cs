using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Made by David Malaky
[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class Character : MonoBehaviour , ICanTakeDamage {

    #region Variables
    //components
    [SerializeField] new protected Rigidbody2D rigidbody;
    [SerializeField] protected float movementSpeed;
    private Vector2 destination;
    
    [SerializeField] protected HashSet<CharacterStatus> StatusEffects;
    [SerializeField] protected int attackDamage;
    [SerializeField] protected float attackCooldown;
    [SerializeField] protected float attackRange;
    protected bool attackReady;

    [SerializeField] protected int healthMax;
    protected int healthCurrent;

    protected List<ICanTakeDamage> targets = new List<ICanTakeDamage>();

    protected Vector2 direction;
    protected bool inMove = false;
    #endregion

    // Methods here
    protected void Start() {
        healthCurrent = healthMax;
        destination = rigidbody.position;
    }

    protected void MoveTowards(Vector2 _target) {
        destination = _target;
        direction = ((destination - rigidbody.position) != Vector2.zero) ? (destination - rigidbody.position).normalized : direction;
    }

    Vector2 newPosition;
    private void FixedUpdate() {
        newPosition = Vector2.MoveTowards(rigidbody.position, destination, movementSpeed * Time.fixedDeltaTime);
        rigidbody.MovePosition(newPosition);
        inMove = !(newPosition == Vector2.zero);
    }

    public void TakeDamage(int _damage, GameObject _source) {
        
    }

    protected IEnumerator Attack() {
        yield return new WaitForFixedUpdate();
        for (int i = 0; i < targets.Count; i++) {
            targets[i].TakeDamage(attackDamage, gameObject);
        }
        yield return new WaitForSeconds(attackCooldown);
        attackReady = true;
    }
}
