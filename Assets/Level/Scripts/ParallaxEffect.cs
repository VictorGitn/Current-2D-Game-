using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] float parallaxFactor;
    [SerializeField] GameObject cam;
    [SerializeField] float pixelsPerUnit;

    private float length, startpos;

    void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        float temp = cam.transform.position.x * (1 - parallaxFactor);

        float distance = cam.transform.position.x * parallaxFactor;
        Vector3 newPosition = new Vector3(startpos + distance, transform.position.y, transform.position.z);
        transform.position = newPosition;
        if (temp > startpos + (length / 2)) startpos += length;
        else if (temp < startpos - (length / 2)) startpos -= length;
    }

}
