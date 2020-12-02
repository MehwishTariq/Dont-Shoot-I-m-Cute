using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private string enemy = "Enemy";

    private Transform enemyTransform;

    public GameObject bulletPrefab , GameoverPanel, LevelCompletePanel;

    [SerializeField] GameObject explosionFx;

    public int realMonstorScore;
    public Text realMonstorScoreText;

    public static bool enemyDead;

    bool disableControl;
 
    public int enemyCount = 0;
    public MenuManager instance;

    Enemy sprite;
    public void Start()
    {
        
        disableControl = false;
    }
    // Update is called once per frame
    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

   
        if(enemyTransform != null)
        {
            enemyTransform = null;
        }

        if(Physics.Raycast(ray, out hit))
        {
            var toShoot = hit.transform;

            //detecting which enemy to shoot enemy or cute.. if enemy then kill him, score++/ if cute then score--
            if (toShoot.CompareTag(enemy) && Input.GetMouseButtonDown(0) && !disableControl)
            {
                GameObject go = Instantiate(explosionFx, hit.transform.position, Quaternion.identity);
                hit.transform.GetComponentInChildren<Animator>().SetBool("Death",true);
                AudioManager.instance.Play("Shoot");
                Destroy(go, 1f);
                
                hit.transform.GetComponent<Enemy>().SetSpeed(0f);
                AudioManager.instance.Play("Death");
                Destroy(hit.transform.gameObject, 1f);

                
                //hit.transform.GetComponent<Enemy>().transform.GetChild(0).gameObject.SetActive(false);
            
                realMonstorScore+=10;
                realMonstorScoreText.text = realMonstorScore.ToString("0");
                realMonstorScoreText.GetComponent<Animator>().SetTrigger("AddScore");
                //enemyDead = true;
                //GetComponent<AudioSource>().PlayOneShot(dead);
                
                enemyCount--;

                if (enemyCount == 0)
                {
                    disableControl = true;
                    instance.LoadMenu(LevelCompletePanel);
                } 
            } 

            else if(toShoot.CompareTag("CuteMonstor") && Input.GetMouseButtonDown(0) && !disableControl)
            {
                GameObject go = Instantiate(explosionFx, hit.transform.position, Quaternion.identity);
                //GetComponent<AudioSource>().PlayOneShot(shoot);
                AudioManager.instance.Play("Death");
                hit.transform.gameObject.SetActive(false);
                realMonstorScore--;
                disableControl = true;

                if (disableControl)
                {
                    instance.LoadMenu(GameoverPanel);
                }
            }

        }

     /* 
        if (Input.GetMouseButtonDown(0))
        {
            var projectile = Instantiate(bulletPrefab, Camera.main.transform.position, Quaternion.identity);
            projectile.transform.LookAt(hit.point);
            projectile.GetComponent<Rigidbody>().velocity = projectile.transform.forward * 1000f;
        }*/
    }
}
