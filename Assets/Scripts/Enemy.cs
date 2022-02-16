using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathVFX;
    [SerializeField] Transform parent;
    [SerializeField] int scorePerHit = 10;

    ScoreBoard scoreBoard;

    private void Start() 
    {
        this.scoreBoard = FindObjectOfType<ScoreBoard>();    
    }

    private void OnParticleCollision(GameObject other) 
    {
        ProcessHit();
        KillEnemy();
    }

    private void KillEnemy()
    {
        GameObject vfx = Instantiate(deathVFX, this.transform.position, Quaternion.identity);
        vfx.transform.parent = this.parent;
        vfx.AddComponent<SelfDestruct>();
        Destroy(this.gameObject);    
    }

    private void ProcessHit()
    {
        this.scoreBoard.IncreaseScore(this.scorePerHit);
    }
}
