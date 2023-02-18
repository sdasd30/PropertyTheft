using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind_Behavior : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject parent;
    void Start()
    {
        parent = transform.parent.gameObject; 
    }

    // Update is called once per frame
    void Update()
    {
        
        if (parent) {
            if (parent.GetComponent<Basic_AI>().AI_type != 7)
            {
                if (gameObject != null)
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }
    }

