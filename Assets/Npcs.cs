using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npcs : MonoBehaviour
{
    bool isDancing = false;
    bool moving = false;
    bool scaling = false;
    bool rotating = false;
     IEnumerator moveOverTime(Transform objectToMove, Vector3 newPos, float duration)
    {
        if (moving)
        {
            yield break;
        }
        moving = true;

        float counter = 0;
        Vector3 startPos = objectToMove.position;

        while(counter < duration)
        {
            counter += Time.deltaTime;
            objectToMove.position = Vector3.Lerp(startPos, newPos, counter/duration);
            yield return null;
        }

        moving = false;

    }
    //help from: https://stackoverflow.com/questions/46587150/scale-gameobject-over-time

    IEnumerator scaleOverTime(Transform objectToScale, Vector3 toScaleUp, Vector3 toScaleDown, float duration)
    {
        //Make sure there is only one instance of this function running
        if (scaling)
        {
            yield break; ///exit if this is still running
        }
        scaling = true;

        //Get the current scale of the object to be moved
        Vector3 startScaleSize = objectToScale.localScale;
        float halveDuration = duration/2;
        float counter = 0f;
        while (counter < halveDuration)
        {
            counter += Time.deltaTime;
            objectToScale.localScale = Vector3.Lerp(startScaleSize, toScaleUp, counter / halveDuration);
            yield return null;
        }
        counter = 0f;

        startScaleSize = objectToScale.localScale;

        while (counter < halveDuration)
        {
            counter += Time.deltaTime;
            objectToScale.localScale = Vector3.Lerp(startScaleSize, toScaleDown, counter / halveDuration);
            yield return null;
        }
        scaling = false;
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

    IEnumerator DanceMoveLinusBasic()
    {        //Make sure there is only one instance of this function running
        if (isDancing)
        {
            yield break; ///exit if this is still running
        }
        isDancing = true;
        float duration = 1f;
        Vector3 scaleUp = new Vector3(1.5f, 1.5f, 1f);
        StartCoroutine(scaleOverTime(transform, scaleUp, transform.localScale, duration));

        yield return new WaitForSeconds(duration);
        
        isDancing = false;      
    }

    void ChooseDance()
    {
        // TODO: Replace with Coroutine which randomly chooses a basic dance move
        // at the moment the other moves are missing for proper implementation
        StartCoroutine(DanceMoveLinusBasic());

    }
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(ChooseDance), 2f, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
