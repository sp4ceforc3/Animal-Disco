using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandling : MonoBehaviour
{
    // Speed of the character
    [SerializeField] float speed = 3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveVector = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) moveVector.y = 1;
        if (Input.GetKey(KeyCode.A)) moveVector.x = -1;
        if (Input.GetKey(KeyCode.S)) moveVector.y = -1;
        if (Input.GetKey(KeyCode.D)) moveVector.x = 1;

        // Normalize vector, so that magnitude for diagonal movement is also 1
        moveVector.Normalize();

        // TODO: this can be used for the NINJA Cheat later
        //moveVector = moveVector * speedMod[indexSpeedMod];

         // Frame rate independent movement
        transform.position += Time.deltaTime * speed * moveVector;
    }
}
