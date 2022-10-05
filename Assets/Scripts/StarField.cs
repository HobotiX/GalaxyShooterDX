using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarField : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1f;


    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -14.7f) {
            //transform.position = new Vector3(0, 17, 0);
            Destroy(this.gameObject);
        }
    }
}
