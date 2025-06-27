using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        enemy.transform.position = enemy.startposition;
        Debug.Log(enemy.currenState);
    }
    public override void UpdateState(EnemyStateManager enemy){}

    public override void OnTrigger(EnemyStateManager enemy, Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enemy.Player = collision.gameObject;
            enemy.SwitchState(enemy.PatrolState);
        }
    }
    public override void OnTriggerE(EnemyStateManager enemy, Collider2D collision){}
}
