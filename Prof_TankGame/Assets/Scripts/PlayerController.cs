using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Camera cam;
    Transform spawnPoint;

    public float speed;
    public GameObject bulletPrefab;
    public float bulletSpeed;
    public float bulletKnockBack;



    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = transform.Find("SpawnPoint");
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();

    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Physics.gravity = new Vector3(h, 0, v) * speed;

        transform.forward = getMousePosInWorld() - transform.position;

        if(Input.GetButtonDown("Fire1"))
        {
            GameObject g = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
            g.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
            GetComponent<Rigidbody>().AddForce(transform.forward * -bulletKnockBack);
        }

    }
    
    public Vector3 getMousePosInWorld()
    {
        RaycastHit hitInfo;
        Ray r = cam.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(r, out hitInfo))
        {
            Vector3 hitSpot = hitInfo.point;
            hitSpot.y = 1;
            return hitSpot;
        }
        return Vector3.zero;
    }
}
