using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npcs : MonoBehaviour
{
    bool isDancing = false;
    bool moving    = false;
    bool scaling   = false;
    bool rotating  = false;

    public bool squidgame = false;
    
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

    private IEnumerator DanceMoveDomaiBasic() {
        if (isDancing)
            yield break;
        isDancing = true;

        StartCoroutine(_danceMoveDomaiBasic(transform, new Vector3(0.5f, 2f, 1f), 0.2f));
        yield return new WaitForSeconds(0.6f);

        isDancing = false;      
    }

    private IEnumerator _danceMoveDomaiBasic(Transform objectToScale, Vector3 stretch, float split) {
        if (scaling)
            yield break;
        scaling = true;
        
        Vector3 returnScale = objectToScale.localScale;
        Vector3 stretchX = new Vector3(stretch.x, 0.5f, 1f);
        Vector3 stretchY = new Vector3(1f, stretch.y, 1f);

        float counter = 0f;
        while (counter < split) {
            counter += Time.deltaTime;
            objectToScale.localScale = Vector3.Lerp(returnScale, stretchX, counter / split);
            yield return null;
        }

        counter = 0f;
        Vector3 startScale = objectToScale.localScale;
        while (counter < split) {
            counter += Time.deltaTime;
            objectToScale.localScale = Vector3.Lerp(startScale, stretchY, counter / split);
            yield return null;
        }

        counter = 0f;
        startScale = objectToScale.localScale;
        while (counter < split) {
            counter += Time.deltaTime;
            objectToScale.localScale = Vector3.Lerp(startScale, returnScale, counter / split);
            yield return null;
        }

        scaling = false;
    }

    void ChooseDance()
    { 
        if (squidgame)
            Destroy(gameObject, 1f);
        string[] npcMoves = new string[]{ nameof(DanceMoveDomaiBasic), nameof(DanceMoveLinusBasic) };
        StartCoroutine(npcMoves[Random.Range(0, npcMoves.Length)]);
        Invoke(nameof(ChooseDance), Random.Range(0.5f, 2.5f));
    }

    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(ChooseDance), 2f);
    }

    // Update is called once per frame
    void Update(){}
}
