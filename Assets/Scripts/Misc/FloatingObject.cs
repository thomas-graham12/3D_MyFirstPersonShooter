using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    /// <summary>
    /// Lets object float up and down
    /// </summary>

    Vector3 startPos;
    [SerializeField] float maxHeight;
    [SerializeField] float speed;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(startPos.x, startPos.y + Mathf.Sin(Time.time * speed) * maxHeight, startPos.z);
    }
}
