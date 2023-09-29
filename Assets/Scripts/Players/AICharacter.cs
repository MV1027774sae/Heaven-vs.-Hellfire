using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICharacter : MonoBehaviour
{
    #region Variables
    //our stored components
    StateManager states;
    public StateManager enStates;

    public float changeStateToTolerance = 3f; //how close is considered close combat

    public float normalRate = 1; //how fast will the AI decide state will cycle on the normal state
    private float nrmTimer;

    public float closeRate = 0.5f; //how will the AI decide state will will cycle on the close state
    private float clTimer;

    public float blockingRate = 1.5f; //for how long will the AI block
    private float blTimer;

    public float aiStateLife = 1; //how much time does it take to reset the AI state
    private float aiTimer;

    private bool initiateAI; //when it has an AI state to run
    private bool closeCombat; //if we are in close combat

    private bool gotRandom; //helps so that we are not updating our random variable every frame
    private float storeRandom; //stores our random float

    //blocking variables
    private bool checkForBlocking;
    private bool blocking;
    private float blockMultiplier; //hasn't been put to use yet

    //attack variables
    private bool randomizeAttacks;
    private int numberOfAttacks;
    private int curNumAttacks;

    //jump variables
    public float jumpRate = 1;
    private float jRate;
    private bool jump;
    private float jTimer;
    #endregion

    public AttackPatterns[] attackPatterns;

    //our AI states
    public enum AIState
    {
        closeState,
        normalState,
        resetAI
    }

    public AIState aiState;

    void Start()
    {
        states = GetComponent<StateManager>();
        //pc = GetComponent<PlayerControls>();
    }

    void Update()
    {
        //call our functions
        CheckDistance();
        States();
        AIAgent();
    }

    //holds our states
    void States()
    {
        //this switch decides which timer to run or not
        switch (aiState)
        {
            case AIState.closeState:
                CloseState();
                break;
            case AIState.normalState:
                NormalState();
                break;
            case AIState.resetAI:
                ResetAI();
                break;
        }

        //this functions are called independent of the AI decide cycle so that it's more responsive
        //Blocking();
        Jumping();
    }

    //this function manages the stuff that the agent has to do
    void AIAgent()
    {
        //if it has something to do, meaning that the AI cycle has made a full run
        if (initiateAI)
        {
            //start the reset AI process, note this is not instant
            //create a multiplier
            float multiplier = 0;

            //get our random value
            if (!gotRandom)
            {
                storeRandom = ReturnRandom();
                gotRandom = true;
            }

            //if we are not in close combat
            if (!closeCombat)
            {
                //we have a 30% larger chance of moving
                multiplier += 30;
            }
            else
            {
                //we have a 30% larger chance to attack
                multiplier -= 30;
            }

            //compare our random value with the added modifiers
            if (storeRandom + multiplier < 50)
            {
                Attack(); //...and either Attack
            }
            else
            {
                Movement();
            }
        }
    }

    void Attack()
    {
        //take a random value
        if (!gotRandom)
        {
            storeRandom = ReturnRandom();
            gotRandom = true;
        }

        //there's a 75% chance of doing a normal attack..
        //if (storeRandom < 75)
        //{
            //see how many attacks the AI will do...
            if (!randomizeAttacks)
            {
                //...byt getting a random int between 1 and 4, the 1 is because we are
                numberOfAttacks = (int)Random.Range(1, 4);
                randomizeAttacks = true;
            }

            //if we haven't attack more than the maximum times
            if (curNumAttacks <  numberOfAttacks)
            {
                //the decide at random which attack we want to do, the max number
                int attackNumber = Random.Range(0, attackPatterns.Length);

                StartCoroutine(OpenAttack(attackPatterns[attackNumber], 0));

                //(...and increment to the times we attack
                curNumAttacks++;
            }
        /*}
         * else //...or special one
         * {
         *  if (curNumAttacks < 1) //we want the special attack to happen only once
         *  {
         *      //states.SpecialAttack = truel
         *      curNumAttacks++;
         *  }
         *}
         */
    }

    void Movement()
    {
        //take a random value
        if (!gotRandom)
        {
            storeRandom = ReturnRandom();
            gotRandom = true;
        }

        //there's a 90% chance of moving close to the enemy
        if (storeRandom < 90)
        {
            if (enStates.transform.position.x < transform.position.x)
            {
                states.horizontal = -1;
            }
            else
            {
                states.horizontal = 1;
            }
        }
        else //...or away from them
        {
            if (enStates.transform.position.x < transform.position.x)
            {
                states.horizontal = 1;
            }
            else
            {
                states.horizontal = -1;
            }
        }

        //NOTE: you can create a modifier based on current health to manipulate the chances of these variables
    }

    //this function resets all our variables
    void ResetAI()
    {
        aiTimer += Time.deltaTime;

        if (aiTimer > aiStateLife)
        {
            initiateAI = false;
            states.horizontal = 0;
            states.vertical = 0;
            aiTimer = 0;

            gotRandom = false;

            //and also there's a chance of switching the AI state from normal to close state to make things more random
            storeRandom = ReturnRandom();
            if (storeRandom < 50)
            {
                aiState = AIState.normalState;
            }
            else
            {
                aiState = AIState.closeState;
            }

            curNumAttacks = 1;
            randomizeAttacks = false;
        }
    }
    //checks the distance from our position and the enemy and changes the state accordingly
    void CheckDistance()
    {
        float distance = Vector3.Distance(transform.position, enStates.transform.position);

        //compare it with our tolerance
        if (distance < changeStateToTolerance)
        {
            //if we are not in the process of resetting the AI, then change the state
            if (aiState != AIState.resetAI)
            {
                aiState = AIState.closeState;
            }

            //if we are close, then we are in close combat
            closeCombat = true;
        }
        else
        {
            //if we are not in the process of resetting the AI, the change the state
            if (aiState != AIState.resetAI)
            {
                aiState = AIState.normalState;
            }

            //if we were close to the enemy and then we start moving away...
            if (closeCombat)
            {
                //take a random value
                if (!gotRandom)
                {
                    storeRandom = ReturnRandom();
                    gotRandom = true;
                }

                //...and then there's a 60% chance of our agent following the enemy
                if (storeRandom < 60)
                {
                    Movement();
                }
            }

            //we probably are no longer in close combat
            closeCombat = false;
        }
    }
    //our blocking logic goes here
    void Blocking()
    {
        //if we are about to receive damage
        if (states.gettingHit)
        {
            //...get the random value
            if (!gotRandom)
            {
                storeRandom = ReturnRandom();
                gotRandom = true;
            }

            //there's a 50% chance of the AI blocking
            if (storeRandom < 50)
            {
                blocking = true;
                states.gettingHit = false;
                //states.blocking = true;
            }
        }

        //if we are blocking then start counting so that we fo not block forever
        if (blocking)
        {
            blTimer += Time.deltaTime;

            if (blTimer > blockingRate)
            {
                //states.blocking = false;
                blTimer = 0;
            }
        }
    }
    //the normal state AI decide state cycle
    void NormalState()
    {
        nrmTimer += Time.deltaTime;

        if (nrmTimer > normalRate)
        {
            initiateAI = true;
            nrmTimer = 0;
        }
    }
    //the close state AI decide state cycle
    void CloseState()
    {
        clTimer += Time.deltaTime;

        if (clTimer > closeRate)
        {
            clTimer = 0;
            initiateAI = true;
        }
    }
    //our jumping logic goes here
    void Jumping()
    {
        //if the player jumps, or we want to jump
        if (!enStates.onGround)
        {
            float ranValue = ReturnRandom();

            if (ranValue < 50)
            {
                jump = true;
            }
        }
        
        if (jump)
        {
            //then add to vertical input
            states.vertical = 1;
            jRate = ReturnRandom();
            jump = false; //we don't want to keep jumping
        }
        else
        {
            //we still needing to reset the vertical input otherwise it will be always jumping
            states.vertical = 0;
        }

        //our jump timer etermines on how many secs it will run a check if the AI wants to jump
        jTimer += Time.deltaTime;

        if (jTimer > jumpRate * 10)
        {
            //get a random value
            

            //then there's a 50% chance of jumping or not
            if (jRate < 50)
            {
                jump = true;
            }
            else
            {
                jump = false;
            }

            jTimer = 0;
        }
    }
    //a simple float thatreturns a random value, we use this a lot
    float ReturnRandom()
    {
        float retVal = Random.Range(0, 101);
        return retVal;
    }

    IEnumerator OpenAttack(AttackPatterns a, int i)
    {
        int index = i;
        float delay = a.attacks[index].delay;
        states.attackL = a.attacks[index].attack1;
        states.attackH = a.attacks[index].attack2;
        yield return new WaitForSeconds(delay);

        states.attackL = false;
        states.attackH = false;

        if (index < a.attacks.Length - 1)
        {
            index++;
            StartCoroutine(OpenAttack(a, index));
        }
    }

    [System.Serializable]
    public class AttackPatterns
    {
        public AttacksBase[] attacks;
    }

    [System.Serializable]
    public class AttacksBase
    {
        public bool attack1;
        public bool attack2;
        public float delay;
    }
}
