using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * dtVelocity(_speed));

        if (transform.position.y < -6f) {
            float randoX = Random.Range(-8f, 8f);
            transform.position = new Vector3(randoX, 8, 0);
            //Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        switch (other.tag) {
            case "Player":
                Destroy(this.gameObject);
                Player player = other.transform.GetComponent<Player>();
                if (player != null) {
                    player.damage();
                }
                break;

            case "Laser":
                Destroy(other.gameObject);
                Destroy(this.gameObject);
                break;

            default:
                break;
        }
    }

    private float dtVelocity(float s) 
    {
        return (s * Time.deltaTime);
    }
}
