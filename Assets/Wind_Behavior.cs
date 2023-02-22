using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind_Behavior : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject parent;
    private int start_fan_direction;
    private bool swapped;
    void Start()
    {
        parent = transform.parent.gameObject;
        start_fan_direction = parent.GetComponent<Basic_AI>().fan_direction;
        swapped = parent.GetComponent<Basic_AI>().swapped;

    }

    // Update is called once per frame
    void Update()
    {

        if (parent)
        {
            if (parent.GetComponent<Basic_AI>().AI_type != 7 || swapped != parent.GetComponent<Basic_AI>().swapped)
            {
                if (gameObject != null)
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }
    public void Destroy()
    {
        Destroy(this.gameObject);
    }
    }

