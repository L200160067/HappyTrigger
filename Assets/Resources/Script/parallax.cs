using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax : MonoBehaviour
{
    private float lenght, startposX;
    private float height, startposY;
    public GameObject cam;
    public float parallaxEffect;
    // Start is called before the first frame update
    void Start()
    {
        startposX = transform.position.x;
        lenght = GetComponent<SpriteRenderer>().bounds.size.x;

        startposY = transform.position.y;
        height = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    private void FixedUpdate()
    {
        float dist = (cam.transform.position.x * parallaxEffect);
        float distY = (cam.transform.position.y * parallaxEffect);

        transform.position = new Vector3(startposX + dist, startposY + distY, transform.position.z);


    }
}
