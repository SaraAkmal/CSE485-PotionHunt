using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        {
            if (gameObject.name.Equals("DownPathCollider"))
            {
                if (other.gameObject.tag.Equals("Player")) SceneManager.LoadScene("DownPathScene");
            }
            else if (gameObject.name.Equals("LeftPathCollider"))
            {
                if (other.gameObject.tag.Equals("Player")) SceneManager.LoadScene("LeftPath");
            }
            else if (gameObject.name.Equals("MainSceneCollider"))
            {
                if (other.gameObject.tag.Equals("Player")) SceneManager.LoadScene("MainScene");
            }
            else if (gameObject.name.Equals("UpperPathCollider"))
                        {
                            if (other.gameObject.tag.Equals("Player")) SceneManager.LoadScene("UpperPath");
                        }
            else if (gameObject.name.Equals("MazePathCollider"))
            {
                if (other.gameObject.tag.Equals("Player")) SceneManager.LoadScene("MazeScene");
            }
        }
    }
}