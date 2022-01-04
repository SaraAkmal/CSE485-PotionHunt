using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeroManager : MonoBehaviour
{
    [SerializeField] private HealthSystem healthSystem;
    private GameObject enemy;

    private SpriteRenderer enemyHealth;
    private GameObject enemyHealthBar;
    private int hitPoint;
    private bool isPoisned;
    private bool isTouchingEnemy;
    private IEnumerator manaCoroutine;
    private PlayerState playerState;

    private void Start()
    {
        playerState = PlayerState.alive;
    }

    // Update is called once per frame
    private void Update()
    {
        CheckHealth();
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

        if (other.gameObject.transform.name.Equals("Knives")) healthSystem.TakeDamage(100);
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.transform.tag.Equals("Poison"))
        {
            isPoisned = true;
            manaCoroutine = UseManaCoroutine(1.2f);
            StartCoroutine(manaCoroutine);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.transform.tag.Equals("Poison"))
        {
            isPoisned = false;
            print("EXITED");
        }
    }

    private IEnumerator UseManaCoroutine(float waitTime)
    {
        while (isPoisned)
        {
            if (healthSystem.manaPoint <= 0)
                healthSystem.TakeDamage(10);

            else
                healthSystem.UseMana(2);

            yield return new WaitForSeconds(waitTime);
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
    }

    private void CheckHealth()
    {
        if (healthSystem.hitPoint < 0.1 && playerState == PlayerState.alive)
        {
            gameObject.GetComponent<Animator>().SetTrigger("Death");
            playerState = PlayerState.dead;
            StartCoroutine(CountdownToReload());
        }
    }

    private IEnumerator CountdownToReload()
    {
        yield return new WaitForSeconds(1.2f); //wait 1 seconds
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private enum PlayerState
    {
        running,
        alive,
        attacking,
        dead
    }
}