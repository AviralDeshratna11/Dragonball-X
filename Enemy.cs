using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject fireballPrefab;
    [Header("Enemy Stats")]
    [SerializeField] int health = 100;
    [SerializeField] int scoreValue = 150;

    [Header("Shooting")]
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 1f;
    [SerializeField] float FireballMoveSpeed = 10f;
    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationOfExplosion;

    [Header("Sound Effects")]
    [SerializeField] AudioClip DeathSound;
    [SerializeField] AudioClip fireSound;

    [SerializeField] [Range(0,1)]float deathSoundVolume = 0.75f;
    
    
    


    private void OnTriggerEnter2D(Collider2D other)
    {

        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        ProcessHit(damageDealer);

    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        if (!damageDealer) { return; }
        damageDealer.Hit();
        if (health <= 0)
        {

            Die();
        }
    }

    private void Die()
    {
        FindObjectOfType<GameSession>().AddToScore(scoreValue);
        Destroy(gameObject);
        GameObject Explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        AudioSource.PlayClipAtPoint(DeathSound, Camera.main.transform.position, deathSoundVolume);
        Destroy(Explosion, durationOfExplosion);
    }


    // Start is called before the first frame update
    void Start()
    {
        shotCounter =UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);

    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
        
       
    }

     private void Fire()
    {
        GameObject Fireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity) as GameObject;
        AudioSource.PlayClipAtPoint(fireSound, Camera.main.transform.position, deathSoundVolume);
        Fireball.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -FireballMoveSpeed);
        
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if(shotCounter <= 0f)
        {
            Fire();
            shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

   
}
