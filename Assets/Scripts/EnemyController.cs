using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public float hitDistance;
    public Transform groundDetection;
    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private SpriteRenderer enemyHealth;

    private EnemyState _enemyState;
    private Animator m_animator;
    private bool movingRight = true;

    private void Start()
    {
        _enemyState = EnemyState.running;
        m_animator = GetComponent<Animator>();
        m_animator.SetInteger("AnimState", 0);
    }


    private void Update()
    {
        CheckHealth();
        if (_enemyState == EnemyState.attacking)
        {
            if (m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >
                0.8f)
            {
                m_animator.SetTrigger("Attack");
                healthSystem.TakeDamage(5);
            } //If normalizedTime is 0 to 1 means animation is playing, if greater than 1 means finished
        }
        else if (_enemyState == EnemyState.dead)
        {
            StartCoroutine(CountdownToDestroy());
            m_animator.SetTrigger("Death");
        }
        else
        {
            EnemyPatrol();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.transform.tag.Equals("Player"))
        {
            m_animator.SetInteger("AnimState", 0);
            _enemyState = EnemyState.attacking;
            m_animator.SetTrigger("Attack");
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.transform.tag.Equals("Player"))
            _enemyState = EnemyState.running;
    }

    private void CheckHealth()
    {
        if (enemyHealth.size.x < 0.1) _enemyState = EnemyState.dead;
    }

    private void EnemyPatrol()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        var groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, hitDistance);
        if (groundInfo.collider == false)
        {
            if (movingRight) // flip
            {
                transform.eulerAngles = new Vector2(0, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector2(0, -180);
                movingRight = true;
            }
        }

        m_animator.SetInteger("AnimState", 2);
    }

    private IEnumerator CountdownToDestroy()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f); //wait 1 seconds
            Destroy(gameObject);
        }
    }

    private enum EnemyState
    {
        running,
        attacking,
        dead
    }
}