using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragPlayerController : MonoBehaviour
{
    Camera cam;
    Transform spawnPoint;
    LineRenderer myLine;

    public float speed;
    public GameObject bulletPrefab;
    public float bulletSpeed;
    public float bulletKnockBack;

    private Vector3 startTouchPos;

    //public float shakeThreshHold;
    // Start is called before the first frame update
    void Start()
    {
        myLine = GetComponent<LineRenderer>();
        //myLine.SetPosition(0, new Vector3(-5, 1, 0));
        //myLine.SetPosition(0, new Vector3(5, 1, 5));
        spawnPoint = transform.Find("SpawnPoint");
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();

    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.acceleration.x;
        float v = Input.acceleration.y;
        Physics.gravity = new Vector3(h, 0, v) * speed;

        //transform.forward = getScreenPosInWorld(Input.mousePosition) - transform.position;
        myLine.enabled = false;
        if (Input.touches.Length > 0)
        {
            Touch t1 = Input.touches[0];
            if (t1.phase == TouchPhase.Began)
            {
                startTouchPos = t1.position;
            }
            else if (t1.phase == TouchPhase.Moved)
            {
                myLine.SetPosition(0, getScreenPosInWorld(startTouchPos));
                myLine.SetPosition(1, getScreenPosInWorld(t1.position));
                myLine.enabled = true;
                transform.forward = myLine.GetPosition(1) - myLine.GetPosition(0);
            }
            else if(t1.phase == TouchPhase.Ended)
            {
                GameObject g = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
                g.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
                GetComponent<Rigidbody>().AddForce(transform.forward * -bulletKnockBack);
            }

            //transform.forward = getScreenPosInWorld(Input.touches[0].position) - transform.position;
        }

    }
    
    public Vector3 getScreenPosInWorld(Vector3 screenPos)
    {
        RaycastHit hitInfo;
        Ray r = cam.ScreenPointToRay(screenPos);
        if(Physics.Raycast(r, out hitInfo))
        {
            Vector3 hitSpot = hitInfo.point;
            hitSpot.y = 1;
            return hitSpot;
        }
        return Vector3.zero;
    }
}
