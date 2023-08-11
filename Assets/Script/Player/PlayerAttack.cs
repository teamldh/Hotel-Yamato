using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject weapon;
    public Transform attackPoint;
    public float attackRange;
    public LayerMask enemyLayers;
    public Animator anim;

    private void Update() {

        if(PlayerControllerInputSystem.GetInstance().GetAttackInput()){
            Attack();
        }
    }

    private void Attack(){
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 aimDir = mousePosition - transform.position;
        aimDir.z = 0f;
        float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        weapon.transform.rotation = rotation;
        anim.SetTrigger("attack");
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void AttackEvent(){
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach(Collider2D enemy in hitEnemies){
            enemy.GetComponent<healthSystem>().TakeDamage(10);
        }
    }
}
