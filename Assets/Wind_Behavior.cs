using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind_Behavior : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject parent;
    private int start_fan_direction;
    private bool swapped;

    public Color col;

    private MaterialAffector ma;
    private MaterialHolder mh;
    private List<MaterialProperty> propertyList;

    SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        parent = transform.parent.gameObject;
        start_fan_direction = parent.GetComponent<Basic_AI>().fan_direction;
        swapped = parent.GetComponent<Basic_AI>().swapped;
    }

    // Update is called once per frame
    void Update()
    {
        // NEED TO COLOR THE WIND OF THE FAN & ADD A FAN BLOWING BEHAVIOR ANIMATION
        if (parent)
        {
            if (parent.GetComponent<Basic_AI>().AI_type != 7 || swapped != parent.GetComponent<Basic_AI>().swapped)
            {
                if (gameObject != null)
                {
                    Destroy(this.gameObject);
                }
            }

            ma = parent.GetComponent<MaterialAffector>();
            mh = parent.GetComponent<MaterialHolder>();
            foreach (MaterialProperty material in mh.propertyList)
            {
                if (material.colorModifier != Color.magenta)
                {
                    col = material.colorModifier;
                    col.a = 0.42f;
                }
            }
        }
        spriteRenderer.color = col;
    }
    public void Destroy()
    {
        Destroy(this.gameObject);
    }
    }

