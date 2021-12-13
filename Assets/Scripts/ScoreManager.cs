using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private ParticleSystem particleEffect;
    private readonly List<GameObject> pickups = new List<GameObject>();
    private int potionScore;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Potion"))
            if (!pickups.Contains(other.gameObject))
            {
                pickups.Add(other.gameObject);
                Destroy(other.gameObject);
                print("trigger");
                potionScore++;
                scoreText.text = "Potions: " + potionScore + "/4";
                particleEffect.Play();
            }
    }
}