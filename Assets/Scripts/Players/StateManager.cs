using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StateManager : MonoBehaviour
{
    public float health = 100;
    public float energy = 100;
    
    //[SerializeField] private float hitInvulTimeL = 0.1f;
    //[SerializeField] private float hitInvulTimeM = 0.15f;
    //[SerializeField] private float hitInvulTimeH = 0.2f;
    [SerializeField] private float hitPushBack = 1f;
    [SerializeField] private float hitVerticalLaunch = 0.5f;

    [Header("Character Control and Movement")]
    public float horizontal;
    public float vertical;
    public bool attackL;
    public bool attackM;
    public bool attackH;
    public bool attackS;
    public bool crouch;
    public bool guard;

    public bool blocking;
    public float blockChip = 0.15f;

    public bool canAttack;
    public bool gettingHit;
    public bool currentlyAttacking;

    public bool dontMove;
    public bool onGround;
    public bool lookRight;

    [Header("Level UI")]
    public Slider healthSlider;
    public TextMeshProUGUI comboCounter;
    public Slider energySlider;


    [Header("Sprite Render")]
    [SerializeField] private SpriteRenderer sRenderer;
    [SerializeField] private GameObject hitboxHolder;

    [HideInInspector]
    public HandleDamageColliders handleDC;
    [HideInInspector]
    public HandleAnimations handleAnim;
    [HideInInspector]
    public HandleMovement handleMovement;
    AudioManager audioManager;
    [HideInInspector]
    public CombotHitDisplay comboHitSript;

    [Header("Grab Reference from others")]
    public GameObject[] movementColliders;
    private ParticleSystem blood;

    void Start()
    {
        handleDC = GetComponent<HandleDamageColliders>();
        handleAnim = GetComponent<HandleAnimations>();
        handleMovement = GetComponent<HandleMovement>();
        audioManager = GetComponent<AudioManager>();
        sRenderer = GetComponentInChildren<SpriteRenderer>();
        blood = GetComponentInChildren<ParticleSystem>();
        comboHitSript = GetComponent<CombotHitDisplay>();
    }

    private void FixedUpdate()
    {
        if (!currentlyAttacking)
        {
            sRenderer.flipX = lookRight;

            //if (!lookRight)
            //    hitboxHolder.transform.localScale = new Vector3(-1, 1, 1);
            //else if (lookRight)
            //    hitboxHolder.transform.localScale = new Vector3(1, 1, 1);
        }

        onGround = isOnGround();

        if (healthSlider != null)
        {
            healthSlider.value = health * 0.01f;
            energySlider.value = energy * 0.01f;
        }

        if (health <= 0)
        {
            if (LevelManager.GetInstance().countdown)
            {
                LevelManager.GetInstance().EndTurnFunction();

                handleAnim.anim.Play("Dead");
            }
        }

        if(energy >= 100)
        {
            energy = 100;
        }
        else if(energy <= 0)
        {
            energy = 0;
        }

        if(gettingHit == true)
        {
            comboCounter.enabled = true;
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
        guard = false;
        blocking = false;
    }

    public void CloseMovementCollider(int index)
    {
        movementColliders[index].SetActive(false);
    }

    public void OpenMovementCollider(int index)
    {
        movementColliders[index].SetActive(true);
    }

    public void TakeDamage(float damage, HandleDamageColliders.DamageType damageType, float hitStun)
    {
        if (!gettingHit && !guard)
        {
            comboHitSript.numberOfHit += 1;
            energy += 15;
            health -= damage;
            gettingHit = true;

            switch (damageType)
            {
                case HandleDamageColliders.DamageType.light:
                    StartCoroutine(CloseImmortality(hitStun));
                    break;
                case HandleDamageColliders.DamageType.medium:
                    StartCoroutine(CloseImmortality(hitStun));
                    break;
                case HandleDamageColliders.DamageType.heavy:
                    handleMovement.AddVelocityOnCharacter(
                        ((!lookRight) ? Vector3.right * hitPushBack : Vector3.right * -hitPushBack) + Vector3.up, hitVerticalLaunch);
                    StartCoroutine(CloseImmortality(hitStun));
                    break;
                case HandleDamageColliders.DamageType.projectile:
                    StartCoroutine(CloseImmortality(hitStun));
                    break;
            }

            if (blood != null)
            {
                blood.Emit(30);
            }

            audioManager.PlayGruntSFX();
        }
        else if (!gettingHit && guard)
        {
            blocking = true;
            health -= (damage * blockChip);
            
            switch (damageType)
            {
                case HandleDamageColliders.DamageType.light:
                    StartCoroutine(CloseImmortality(hitStun));
                    break;
                case HandleDamageColliders.DamageType.medium:
                    StartCoroutine(CloseImmortality(hitStun));
                    break;
                case HandleDamageColliders.DamageType.heavy:
                    StartCoroutine(CloseImmortality(hitStun));
                    break;
                case HandleDamageColliders.DamageType.projectile:
                    StartCoroutine(CloseImmortality(hitStun));
                    break;
            }
        }
    }

    IEnumerator CloseImmortality (float timer)
    {
        dontMove = true;
        //gameObject.GetComponentInChildren<Animator>().speed = timer;
        yield return new WaitForSeconds(timer);
        //gameObject.GetComponentInChildren<Animator>().speed = 1;
        gettingHit = false;
        blocking = false;
        dontMove = false;
    }
}
