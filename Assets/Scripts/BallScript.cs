using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public new Rigidbody2D rigidbody {get; private set;}
     public AudioClip hitSoundPaddleOrBreak; 
     public AudioClip hitSoundBounds; 
    public AudioClip hitDownBounds;
    private AudioSource audioSource;
    private bool gameStarted = false;
    public float speed = 400f; 
    private void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
         Invoke(nameof(StartGame), 2f);
    }
    private void Start()
    {
        ResetBall();
    }
      private void StartGame()
    {
        gameStarted = true;
    }
    private void RandomizeDirection()
    {
        Vector2 force = Vector2.zero;
        force.x = Random.Range(-1f , 1f);
        force.y = -1f;
        this.rigidbody.AddForce(force.normalized * this.speed);

    }
    public void ResetBall()
    {
        this.transform.position = Vector2.zero;
        this.rigidbody.velocity = Vector2.zero;
        Invoke(nameof(RandomizeDirection), 2f);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
         if(other.gameObject.name == "Bounds Down")
        {
            FindObjectOfType<GameManage>().LoseLife();
            PlayHitSound(hitDownBounds);
        }
        
        if (gameStarted && other.gameObject.name == "Paddle" || other.gameObject.name == "Brick")
        {
            PlayHitSound(hitSoundPaddleOrBreak);
        }
        if (gameStarted && other.gameObject.name == "Bounds Up" || other.gameObject.name == "Bounds Left"|| other.gameObject.name == "Bounds Right")
        {
            PlayHitSound(hitSoundBounds);
        }
        
    }
    private void PlayHitSound(AudioClip soundToPlay)
    {
        if (hitSoundPaddleOrBreak != null && audioSource != null)
        {
            audioSource.PlayOneShot(soundToPlay); 
        }
    }

}
