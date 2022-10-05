using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astroid : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed = 3f;
    [SerializeField]
    private GameObject _explosionAnimation;
    private SpawnManager _spawnManager;

    private void Start() 
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        switch (other.tag) {
            case "Player":
                Instantiate(_explosionAnimation, transform.position, Quaternion.identity);
                //Destroy(other.gameObject);
                _spawnManager.StartSpawning();
                Destroy(this.gameObject);
                break;

            case "Laser":
                Instantiate(_explosionAnimation, transform.position, Quaternion.identity);
                Destroy(other.gameObject);
                _spawnManager.StartSpawning();
                Destroy(this.gameObject, 0.25f);
                break;

            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
    }
}
