using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        enemy.transform.position = enemy.startposition;
        Debug.Log(enemy.currenState);
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        var playerDistance = Vector2.Distance(enemy.transform.position, enemy.Player.transform.position);
        if (playerDistance<enemy.PatrolRange)
        {
            enemy.SwitchState(enemy.PatrolState);
        }
        if (playerDistance>enemy.IdleRange)
        {
            enemy.SwitchState(enemy.IdleState);
        }
    }


 
}
