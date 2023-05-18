using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    [SerializeField] public float speed;

    [SerializeField] public float cameraBottom;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        float dx = Input.GetAxis("Horizontal");
        float dy = Input.GetAxis("Vertical");
        float mag = Mathf.Max(Mathf.Abs(dx), Mathf.Abs(dy)); // Apply input rise/fall tweening (dirty implementation but oh well)
        Vector2 delta = new Vector2(dx, dy);
        delta.Normalize();
        delta *= mag * speed * Time.deltaTime;
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x + delta.x, BuildManager.Instance.GetMaxCameraLeft(), BuildManager.Instance.GetMaxCameraRight()),
            Mathf.Clamp(transform.position.y + delta.y, cameraBottom, BuildManager.Instance.GetMaxCameraHeight()),
            transform.position.z
        );
        
    }
}
