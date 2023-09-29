using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateManager : MonoBehaviour
{
    public float health = 100;
    
    [SerializeField] private float hitInvulTimeL = 0.01f;
    [SerializeField] private float hitInvulTimeH = 0.01f;
    [SerializeField] private float heavyKnockbackX = 1f;
    [SerializeField] private float heavyKnockbackY = 0.5f;

    public float horizontal;
    public float vertical;
    public bool attackL;
    public bool attackM;
    public bool attackH;
    public bool attackS;
    public bool crouch;

    public bool canAttack;
    public bool gettingHit;
    public bool currentlyAttacking;

    public bool dontMove;
    public bool onGround;
    public bool lookRight;

    public Slider healthSlider;
    private SpriteRenderer sRenderer;

    [HideInInspector]
    public HandleDamageColliders handleDC;
    [HideInInspector]
    public HandleAnimations handleAnim;
    [HideInInspector]
    public HandleMovement handleMovement;

    public GameObject[] movementColliders;

    private ParticleSystem blood;

    void Start()
    {
        handleDC = GetComponent<HandleDamageColliders>();
        handleAnim = GetComponent<HandleAnimations>();
        handleMovement = GetComponent<HandleMovement>();
        sRenderer = GetComponentInChildren<SpriteRenderer>();
        blood = GetComponentInChildren<ParticleSystem>();
    }

    private void FixedUpdate()
    {
        sRenderer.flipX = lookRight;

        onGround = isOnGround();

        if (healthSlider != null)
        {
            healthSlider.value = health * 0.01f;
        }

        if (health <= 0)
        {
            if (LevelManager.GetInstance().countdown)
            {
                LevelManager.GetInstance().EndTurnFunction();

                handleAnim.anim.Play("Dead");
            }
        }
    }

    private bool isOnGround()
    {
        bool retVal = false;

        LayerMask layer = ~(1 << gameObject.layer | 1 << 3);
        retVal = Physics2D.Raycast(transform.position, -Vector2.up, 0.1f, layer);

        return retVal;
    }

    public void ResetStateInputs()
    {
        horizontal = 0;
        vertical = 0;
        attackL = false;
        attackM = false;
        attackH = false;
        attackS = false;
        crouch = false;
        gettingHit = false;
        currentlyAttacking = false;
        dontMove = false;
    }

    public void CloseMovementCollider(int index)
    {
        movementColliders[index].SetActive(false);
    }

    public void OpenMovementCollider(int index)
    {
        movementColliders[index].SetActive(true);
    }

    public void TakeDamage(float damage, HandleDamageColliders.DamageType damageType)
    {
        if (!gettingHit)
        {
            switch (damageType)
            {
                case HandleDamageColliders.DamageType.light:
                    StartCoroutine(CloseImmortality(hitInvulTimeL));
                    break;
                case HandleDamageColliders.DamageType.heavy:
                    handleMovement.AddVelocityOnCharacter(
                        ((!lookRight) ? Vector3.right * heavyKnockbackX : Vector3.right * -heavyKnockbackX) + Vector3.up, heavyKnockbackY);
                    StartCoroutine(CloseImmortality(hitInvulTimeH));
                    break;
            }

            if (blood != null)
            {
                blood.Emit(30);
            }

            health -= damage;
            gettingHit = true;
        }
    }

    IEnumerator CloseImmortality (float timer)
    {
        yield return new WaitForSeconds(timer);
        gettingHit = false;
    }
}
