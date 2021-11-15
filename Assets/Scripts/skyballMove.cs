using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skyballMove : MonoBehaviour
{
    int a = 1;
    int b = 1;
    private void Update()
    {
        if (transform.position.y < 55.0f)
        {
            a = 1;
        }
        else if (transform.position.y >= 65.0f)
        {
            a = -1;
        }

        if (transform.position.x >= 250.0f)
        {
            b = 1;
        }
        else if (transform.position.x < 100.0f)
        {
            b = -1;
        }
        transform.Translate(Vector3.up * 1.0f * Time.deltaTime * a);
        transform.Translate(Vector3.left * 1.0f * Time.deltaTime * b);
        transform.rotation = Quaternion.Euler(0, 180 * b, 0);
    }
}
