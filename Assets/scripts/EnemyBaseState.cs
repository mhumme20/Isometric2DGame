using UnityEngine;


public abstract class EnemyBaseState
{
    public abstract void EnterState(EnemyStateManager enemy);
    public abstract void UpdateState(EnemyStateManager enemy);
    public abstract void OnTrigger(EnemyStateManager enemy, Collider2D collision);
    public abstract void OnTriggerE(EnemyStateManager enemy, Collider2D collision);
}
