using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind_Behavior : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject parent;
    private int start_fan_direction;
    void Start()
    {
        parent = transform.parent.gameObject;
        start_fan_direction = parent.GetComponent<Basic_AI>().fan_direction;

    }

    // Update is called once per frame
    void Update()
    {

        if (parent)
        {
            if (parent.GetComponent<Basic_AI>().AI_type != 7 || parent.GetComponent<Basic_AI>().fan_direction != start_fan_direction)
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

