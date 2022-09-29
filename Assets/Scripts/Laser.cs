using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(
            0f,
            dtVelocity(_speed),
            0f));

        if (transform.position.y > 8f) {
            Destroy(this.gameObject);
        }
    }

    private float dtVelocity(float s) 
    {
        return (s * Time.deltaTime);
    }
}
