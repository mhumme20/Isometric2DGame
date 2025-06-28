using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy){}

    public override void UpdateState(EnemyStateManager enemy)
    {
        enemy.direction = enemy.Player.transform.position - enemy.transform.position;
        float angle = Mathf.Atan2(enemy.direction.y, enemy.direction.x) * Mathf.Rad2Deg;
        enemy.transform.rotation = Quaternion.Euler(0f, 0f, angle-90);
        
        enemy.transform.position = Vector2.MoveTowards(
            enemy.transform.position, enemy.Player.transform.position, 
            enemy.ChaseSpeed * Time.deltaTime
        );
        var playerDistance = Vector2.Distance(enemy.transform.position, enemy.Player.transform.position);
        if (playerDistance < enemy.AttackRange)
        {
            enemy.SwitchState(enemy.AttackState);
        }

        if (playerDistance > enemy.PatrolRange)
        {
            enemy.SwitchState(enemy.PatrolState);
        }
    }
  
}
