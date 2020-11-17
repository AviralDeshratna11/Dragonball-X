using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{

    [SerializeField] GameObject enemyprefab;
    [SerializeField] GameObject pathprefab;
    [SerializeField] float timeBetweenSpawns = 1f;
    [SerializeField] float spawnRandomFactor = 0.3f;
    [SerializeField] int numberOfEnemies = 5;
    [SerializeField] float moveSpeed = 2f;
    public GameObject GetEnemyPrefab() { return enemyprefab; } 
    public List<Transform> GetWaypoints()
    
    
    {
        var waveWaypoints = new List<Transform>();
        foreach(Transform child in pathprefab.transform)
        {
            waveWaypoints.Add(child);

        }
        return waveWaypoints; 
    }
    public float GetTimeBetweenSpawns(){ return timeBetweenSpawns; }
    public float GetSpawnRandomFactor() { return spawnRandomFactor; }
    public int GetNumberOfEnemies() { return numberOfEnemies; }
    public float GetMoveSpeed() { return moveSpeed; }




}
