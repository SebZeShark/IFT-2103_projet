using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bouletDeCannon : MonoBehaviour
{
    int time;
    // Start is called before the first frame update
    void Start()
    {
        time = 500;
    }

    // Update is called once per frame
    void Update()
    {
        time -= 1;
        if (time <= 0)
            Destroy(gameObject);
    }
}
