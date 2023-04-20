using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandling : MonoBehaviour
{
    // Speed of the character
    [SerializeField] float speed = 3f;
    // Start is called before the first frame update
    float danceTimer = 0f;
    [SerializeField] GameObject player;
    [SerializeField] SpriteRenderer sr;
    bool isDancing = false;
    bool scaling = false;
    bool rotating = false;

    //help from: https://stackoverflow.com/questions/46587150/scale-gameobject-over-time
    IEnumerator scaleOverTime(Transform objectToScale, Vector3 toScaleUp, Vector3 toScaleDown, float duration)
    {
        //Make sure there is only one instance of this function running
        if (isDancing)
        {
            yield break; ///exit if this is still running
        }
        isDancing = true;

        //Get the current scale of the object to be moved
        Vector3 startScaleSize = objectToScale.localScale;

        while (danceTimer < duration)
        {
            danceTimer += Time.deltaTime;
            objectToScale.localScale = Vector3.Lerp(startScaleSize, toScaleUp, danceTimer / duration);
            yield return null;
        }
        danceTimer = 0f;

        startScaleSize = objectToScale.localScale;

        while (danceTimer < duration)
        {
            danceTimer += Time.deltaTime;
            objectToScale.localScale = Vector3.Lerp(startScaleSize, toScaleDown, danceTimer / duration);
            yield return null;
        }
        danceTimer = 0f;
        isDancing = false;
    }

    // from https://stackoverflow.com/questions/37586407/rotate-gameobject-over-time
    IEnumerator rotateObject(GameObject gameObjectToMove, Quaternion newRot, Quaternion oldRot, float duration)
    {

        if (rotating)
        {
            yield break;
        }
        rotating = true;

        Quaternion currentRot = gameObjectToMove.transform.rotation;

        float counter = 0f;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            gameObjectToMove.transform.rotation = Quaternion.Lerp(currentRot, newRot, counter / duration);
            yield return null;
        }
        
        currentRot = gameObjectToMove.transform.rotation;
        counter = 0f;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            gameObjectToMove.transform.rotation = Quaternion.Lerp(currentRot, oldRot, counter / duration);
            yield return null;
        }

        rotating = false;
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isDancing) return;

        if (Input.GetKey(KeyCode.Alpha1))
        {
            //StartCoroutine(scaleOverTime(player.transform, new Vector3(1.5f, 1.5f, 1f), new Vector3(1f, 1f, 1f), 0.5f));
            //from https://stackoverflow.com/questions/37586407/rotate-gameobject-over-time
            Quaternion rotation2 = Quaternion.Euler(new Vector3(0f, 0f, 180));
            Quaternion oldRotation = player.transform.rotation;
            StartCoroutine(rotateObject(player, rotation2, oldRotation, 1f));
        }


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
