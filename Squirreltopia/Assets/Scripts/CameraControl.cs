using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    [SerializeField] public float speed;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        Vector2 delta = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        delta.Normalize();
        delta *= speed * Time.deltaTime;
        transform.position = new Vector3(
            transform.position.x + delta.x,
            transform.position.y + delta.y,
            transform.position.z
        );
        
    }
}
