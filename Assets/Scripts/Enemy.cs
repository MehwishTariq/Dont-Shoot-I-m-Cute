using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;

    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;

    Vector3 targetPos;

    //[SerializeField] Color originalMonstorColor;
    //[SerializeField] Color newMonstorColor;

    [SerializeField] GameObject originalMonstorSprite;
    [SerializeField] GameObject newMonstorSprite;

    public bool isRealEnemy;

    private float timeToShowRealEnemy;
    public float timeInBetween = 3f;

    // private Animator anim;

    public void SetSpeed(float speed)
    { 
        moveSpeed = speed;  
    }

    private void Start()
    {
       // anim = GetComponent<Animator>();

        float randomX = Random.Range(minX, maxX);
        float randomZ = Random.Range(minZ, maxZ);

        targetPos = new Vector3(randomX, transform.position.y, randomZ);

        isRealEnemy = false;
        //gameObject.GetComponentInChildren<SpriteRenderer>().color = originalMonstorColor;

        newMonstorSprite.SetActive(false);
        originalMonstorSprite.SetActive(true);
    }


    // Update is called once per frame
    void Update()
    {
        EnemyPatrolling();

        timeInBetween = Random.Range(0.5f, 1f);

        if (Time.time >= timeToShowRealEnemy && !isRealEnemy && gameObject.CompareTag("Enemy"))
        {
            timeToShowRealEnemy = Time.time + timeInBetween;
            isRealEnemy = true;
        }

        // check if enemy is real enemy.
        if (isRealEnemy) //&& !Player.enemyDead)
        {
            StartCoroutine(TransmittingToMonstor());
        }

    }

    // logic for random transition of enemies
    IEnumerator TransmittingToMonstor()
    {
        //GetComponent<AudioSource>().PlayOneShot(shapeChange);
        newMonstorSprite.SetActive(true);
        originalMonstorSprite.SetActive(false);
     
        yield return new WaitForSeconds(3f);
        newMonstorSprite.SetActive(false);
        originalMonstorSprite.SetActive(true);
       
        isRealEnemy = false;
    }
    
    // random enemy patrol
    private void EnemyPatrolling()
    {
        if (transform.position != targetPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
        }

        else
        {
            float randomX = Random.Range(minX, maxX);
            float randomZ = Random.Range(minZ, maxZ);
            targetPos = new Vector3(randomX, transform.position.y, randomZ);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        float randomX = Random.Range(minX, maxX);
        float randomZ = Random.Range(minZ, maxZ);
        targetPos = new Vector3(randomX, transform.position.y, randomZ);
    }
}
