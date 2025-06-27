using System.Collections.Generic;
using UnityEngine;
using System.Collections;
public class EnemyStateManager : MonoBehaviour
{
    public EnemyBaseState currenState;

    public EnemyIdleState IdleState = new EnemyIdleState();
    public EnemyPatrolState PatrolState = new EnemyPatrolState();
    public EnemyChaseState ChaseState = new EnemyChaseState();
    public EnemyAttackState AttackState = new EnemyAttackState();

    public GameObject Player, AttackIndicator, ProjectilePrefab;

    //this enemy goes from idle to patrol once the player enters its trigger collider and will go from patroll to idle
    //when the player exists its trigger collider. While inside the trigger collider, futher state shitfts are handled
    //by measuring the distance between the player and the enemy
   //PatrolRange is the distance at which the enemy will return to patrolling if it was chassing
   //chaserange is where it will go from patroling to chasing
   //attack range is where it will go from chasing to attacking
   //max attack range at which it will go from attacking to chasing
   //speed is how fast it moves when patroling, chasespeed is how fast it moves while chasing
   //atackinterval is how often it attacks, attackspeed is how long it takes for it to perform the attack
   //projectile speed is how fast the projectile it fires moves, timer is used to measure time for attackintervals
    public float PatrolRange, ChaseRange, AttackRange, MaxAttackRange, Speed, ChaseSpeed, AttackInterval, AttackSpeed, ProjectileSpeed, Timer;
    
    //patrolstatus determins weather its moving towards the start or patrolpostion
    //rotate determines weather it will rotate and attacking indicates weather it is currently performing an attack
    public bool patrolStatus,Rotate, Attacking;
    
    //posible start and partol positions are stored in this list and randomly selected one the script starts
    public List<Vector2> SpawnAndPatrolPositions = new List<Vector2>();
    
    public Vector2 startposition;//this is where the enemy will be moved to when the script starts
    public Vector2 patrolposition;
    public Vector2 direction;//this is the direction the enemy is currently facing
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currenState = IdleState;
        currenState.EnterState(this);
        int start= Random.Range(0,SpawnAndPatrolPositions.Count-1);
        startposition = SpawnAndPatrolPositions[start];
        SpawnAndPatrolPositions.RemoveAt(start);
        int patrol= Random.Range(0,SpawnAndPatrolPositions.Count-1);
        patrolposition = SpawnAndPatrolPositions[patrol];
    }

    // Update is called once per frame
    void Update()
    {
        currenState.UpdateState(this);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collision");
        currenState.OnTrigger(this, collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        currenState.OnTriggerE(this, collision);
    }

    public void SwitchState(EnemyBaseState state)
    {
        currenState = state;
        state.EnterState(this);
        Debug.Log(currenState);
    }


    public void BeginAttack()
    {
        StartCoroutine(AttackSquence());
    }
    
    //the ienumerator has to be placed instead of the attackstate script here since it needs to be in a MonoBehaviour 
    //script to work and the state scripts cant be MonoBehaviour
    private IEnumerator AttackSquence()
    {
        AttackIndicator.SetActive(true);
        Rotate = false;
        yield return new WaitForSeconds(AttackSpeed);
        var proj = Object.Instantiate(ProjectilePrefab, AttackIndicator.transform.position, Quaternion.identity);
        Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
        rb.AddForce(direction*ProjectileSpeed, ForceMode2D.Force);
        Rotate = true;
        AttackIndicator.SetActive(false);
        Attacking = false;
    }


}
