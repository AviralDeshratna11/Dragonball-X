using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] float moveSpeed = 15f;
    [SerializeField] float padding = 1f;
    [SerializeField] int health = 2000;
    
    

    [Header("Projectile")]
    [SerializeField] GameObject dragonballprefab;
    [SerializeField] Sprite[]  gokuSprites;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.15f;
    [SerializeField] AudioClip FireballSFX;
    [SerializeField] AudioClip DeathSFX;
    float xMin;
    float xMax;
    float yMin;
    float yMax;
    [SerializeField] [Range(0, 1)] float SFXvolume = 0.25f;
    Coroutine firingCoroutine;
    
    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
    }

    

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);

    }
    public int GetHealth() { return health; }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            FindObjectOfType<LevelLoading>().LoadGameOver();
            AudioSource.PlayClipAtPoint(DeathSFX, Camera.main.transform.position, SFXvolume);
            Destroy(gameObject);
            
        }
    }


    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuosly());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
            GetComponent<SpriteRenderer>().sprite = gokuSprites[0];
            
        }
    }
    IEnumerator FireContinuosly()
    {
        while (true)
        {
            GetComponent<SpriteRenderer>().sprite = gokuSprites[1];
            GameObject dragonball = Instantiate(dragonballprefab, transform.position, Quaternion.identity) as GameObject;
            AudioSource.PlayClipAtPoint(FireballSFX, Camera.main.transform.position, SFXvolume);
            dragonball.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }
    

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var newXPos = Mathf.Clamp( transform.position.x + deltaX , xMin, xMax);
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newYPos = Mathf.Clamp( transform.position.y + deltaY , yMin, yMax);
        transform.position = new Vector2(newXPos, newYPos);
    }
    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;

    }
}
