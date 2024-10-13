using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{

    [SerializeField] private Sprite[] targetSprites;
    [SerializeField] private BoxCollider2D cd;
    [SerializeField] private GameObject targetPrefab;
    [SerializeField] private float cooldown;
    public float timer; 

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0) {
            timer = cooldown;

            GameObject newTarget = Instantiate(targetPrefab);

            float randomX = Random.Range(cd.bounds.min.x, cd.bounds.max.x);
            newTarget.transform.position = new Vector2(randomX, transform.position.y);

            newTarget.GetComponent<SpriteRenderer>().sprite = targetSprites[Random.Range(0, targetSprites.Length)];
        }        
    }
}
