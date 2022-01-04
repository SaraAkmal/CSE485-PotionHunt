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

        if (other.gameObject.tag.Equals("Key"))
            if (!pickups.Contains(other.gameObject))
            {
                pickups.Add(other.gameObject);
                other.gameObject.SetActive(false);
                other.gameObject.transform.SetParent(gameObject.transform);
            }

        if (other.gameObject.tag.Equals("Door"))
            if (IsContainingKey(pickups))
            {
                print("OpenDoor");
                Destroy(other.gameObject);
            }
    }

    private bool IsContainingKey(List<GameObject> pickups)
    {
        for (var i = 0; i < pickups.Count; i++)
            if (pickups[i].CompareTag("Key"))
                return true;

        return false;
    }
}