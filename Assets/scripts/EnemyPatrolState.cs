using UnityEngine;

public class EnemyPatrolState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        enemy.patrolStatus = true;
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        Vector2 target = enemy.patrolStatus ? enemy.patrolposition : enemy.startposition;

        enemy.transform.position = Vector2.MoveTowards(
            enemy.transform.position, target, enemy.Speed * Time.deltaTime
            );
        
        Vector2 direction = (target - (Vector2)enemy.transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        enemy.transform.rotation = Quaternion.Euler(0, 0, angle-90);
        
        if (Vector2.Distance(enemy.transform.position, target) < 0.1f)
        {
            enemy.patrolStatus = !enemy.patrolStatus;
        }
        
        var playerDistance = Vector2.Distance(enemy.transform.position, enemy.Player.transform.position);
        if (playerDistance<enemy.ChaseRange)
        {
            enemy.SwitchState(enemy.ChaseState);
        }
      
    }
    public override void OnTrigger(EnemyStateManager enemy, Collider2D collision){}

    public override void OnTriggerE(EnemyStateManager enemy, Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enemy.SwitchState(enemy.IdleState);
        }
    }
}
