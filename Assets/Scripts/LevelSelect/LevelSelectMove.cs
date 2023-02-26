using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectMove : MonoBehaviour
{
    public float moveSpeed;
    Rigidbody2D mRigidBody;
    // Start is called before the first frame update
    void Start()
    {
        mRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float dirX = Input.GetAxisRaw("Horizontal");
        float dirY = Input.GetAxisRaw("Vertical");

        mRigidBody.velocity = new Vector3(moveSpeed * dirX, moveSpeed * dirY, 0);
    }
}
