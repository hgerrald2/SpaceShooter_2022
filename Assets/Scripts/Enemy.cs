using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;

    private Player _player;
    private Animator _anim;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>();
        if (_player == null)
        {
            Debug.Log("the player is null");
        }
        _anim = GetComponent<Animator>();
        if (_anim == null)
        {
            Debug.Log("The animator is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        // When goes off the screen, respawn at top
        if (transform.position.y < -5f)
        {
            transform.position = new Vector3(Random.Range(-8f, 8f), 7, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player)
            {
                player.Damage();
            }
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _audioSource.Play();
            Destroy(this.gameObject, 2.8f);
        }
        else if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            // Add 10 to score
            if (_player)
            {
                _player.AddScore(10);
            }
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _audioSource.Play();
            Destroy(this.gameObject, 2.8f);
        }
    }
}
