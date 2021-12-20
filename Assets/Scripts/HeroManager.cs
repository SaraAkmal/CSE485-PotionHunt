using UnityEngine;

public class HeroManager : MonoBehaviour
{
    private GameObject enemy;

    private SpriteRenderer enemyHealth;
    private GameObject enemyHealthBar;
    private int hitPoint;
    private bool isTouchingEnemy;

    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && isTouchingEnemy)
        {
            UpdateHealthBar();
            print("Hit");
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.transform.tag.Equals("Enemy"))
        {
            isTouchingEnemy = true;
            enemy = other.gameObject;
            enemyHealthBar = other.gameObject.transform.Find("HealthBar").gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.transform.tag.Equals("Enemy"))
        {
            isTouchingEnemy = false;
            enemyHealthBar = null;
            enemy = null;
        }
    }


    private void UpdateHealthBar()
    {
        enemyHealth = enemyHealthBar.GetComponent<SpriteRenderer>();
        var sizeBounds = enemyHealth.size;
        if (sizeBounds.x <= 0.1f)
            return;
        sizeBounds.x -= 0.2f;
        enemyHealthBar.GetComponent<SpriteRenderer>().size = sizeBounds;
        enemy.GetComponent<Animator>().SetTrigger("Hurt");
    }
}