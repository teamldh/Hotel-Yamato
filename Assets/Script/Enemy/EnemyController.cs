using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float chaseRange = 10f;
    public float attackRange = 2f;
    private Transform player;
    public GameObject enemyBulletPrefab;
    public Transform firePoint;
    public float timeBetweenShots = 1f;
    private float lastShotTime;
    public float moveSpeed = 5f;
    private Vector2 direction;
    private float angle;
    private Rigidbody2D rb;
    private float distanceToPlayer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponentInParent<Rigidbody2D>();
    }

    void Update()
    {
        if(player != null){
            distanceToPlayer = Vector2.Distance(transform.position, player.position);
            if (distanceToPlayer <= chaseRange)
            {
                changeDirection();
                firePoint.transform.rotation = Quaternion.AngleAxis(angle, transform.forward);
            }
            else
            {
                direction = Vector2.zero;
            }
        }
    }

    private void FixedUpdate()
    {
        // Check if the player is outside the attack range
        if (distanceToPlayer > attackRange)
        {
            rb.MovePosition(direction * moveSpeed * Time.fixedDeltaTime + rb.position);
        }
        else
        {
            if (player != null && Time.time - lastShotTime >= timeBetweenShots)
            {
                AttackPlayer();
            }
        }
    }
    private void AttackPlayer()
    {
        GameObject bullet = Instantiate(enemyBulletPrefab, firePoint.position, firePoint.rotation);
        lastShotTime = Time.time;
        Debug.Log("ATTACK!");
    }

    private void changeDirection()
    {
        direction = (player.position - transform.position).normalized;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    }

    public void OnStop(bool stop)
    {

        rb.velocity = Vector2.zero;
        this.GetComponent<EnemyController>().enabled = stop;

    }
}
