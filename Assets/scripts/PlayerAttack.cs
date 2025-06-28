using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{

    public float AttackCoolDown,ProjectileSpeed;
    public GameObject ProjectilePrefab;
    private bool CanAttack=true;
    private Vector2 direction;
    
    public void attack(Vector2 attackDirection)
    {
        if (CanAttack)
        {
            CanAttack = false;
            direction = attackDirection;
            StartCoroutine(AttackSquence());
        }
    }

    private IEnumerator AttackSquence()
    {
        var proj = Object.Instantiate(ProjectilePrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
        rb.AddForce(direction*ProjectileSpeed, ForceMode2D.Force);
        yield return new WaitForSeconds(AttackCoolDown);
        CanAttack = true;
    }
    
}
