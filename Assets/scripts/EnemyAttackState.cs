using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
  

    public override void EnterState(EnemyStateManager enemy)
    {
        enemy.Timer = 0;
        enemy.Rotate = true;
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        
        if (enemy.Rotate)
        {   
            enemy.direction = enemy.Player.transform.position - enemy.transform.position;
            float angle = Mathf.Atan2(enemy.direction.y, enemy.direction.x) * Mathf.Rad2Deg;
            enemy.transform.rotation = Quaternion.Euler(0f, 0f, angle-90);
        }

        
        var playerDistance = Vector2.Distance(enemy.transform.position, enemy.Player.transform.position);
        if (playerDistance > enemy.MaxAttackRange)
        {
            enemy.SwitchState(enemy.ChaseState);
        }


        if (!enemy.Attacking)
        {
            enemy.Timer += Time.deltaTime;
        }
        
        if ( enemy.Timer  >= enemy.AttackInterval)
        {
            enemy.Timer = 0;
            enemy.Attacking = true;
            enemy.BeginAttack();
        }
    }
    public override void OnTrigger(EnemyStateManager enemy, Collider2D collision){}
    public override void OnTriggerE(EnemyStateManager enemy, Collider2D collision){}




}
