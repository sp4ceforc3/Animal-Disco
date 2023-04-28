using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHandling : MonoBehaviour
{
    // Speed of the character
    [SerializeField] float speed = 3f;
    [SerializeField] GameObject player;
    [SerializeField] SpriteRenderer sr;
    [SerializeField] GameObject discoLightPrefab;
    [SerializeField] Transform discoLightContainer;
    [SerializeField] Transform npcsContainer;
    [SerializeField] GameObject moshPitPrefab;

    Camera mainCam;
    SpriteRenderer backgroundSR;
    GameObject tmpContainer;
    bool isDancing = false;
    bool moving    = false;
    bool scaling   = false;
    bool rotating  = false;
    string input   = "";
    float speedMod = 1f;
    bool ninjamode = false;  
    bool dogemode  = false;  
    bool doMoshpit = false;  
    bool squidgame = false;

    IEnumerator moveOverTime(Transform objectToMove, Vector3 newPos, float duration)
    {
        if (moving)
        {
            yield return new WaitForSeconds(duration);
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
        counter = 0f;
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
        StartCoroutine(scaleOverTime(player.transform, scaleUp, player.transform.localScale, duration));

        yield return new WaitForSeconds(duration);
        
        isDancing = false;      
    }

    IEnumerator DanceMoveLinusAdvanced()
    {
        if (isDancing)
        {
            yield break;
        }
        isDancing = true;

        StartSpecialMove();

        float duration = 0.5f;
        
        int movesCounter = 0;
        Vector3 origPos = player.transform.position;
        while (movesCounter < 10)
        {
            Vector3 startPos = player.transform.position;
            Vector3 newPos = Random.insideUnitCircle.normalized;
            newPos += transform.position;
            newPos.z = player.transform.position.z;

            //float angle = Mathf.Atan2(newPos.y, newPos.x) * Mathf.Rad2Deg;
            //player.transform.rotation = Quaternion.Euler(0f, 0f, angle);


            Vector3 scaleUp = new Vector3(Random.Range(0.5f, 3f), Random.Range(0.5f, 3f), 1f);
            StartCoroutine(moveOverTime(player.transform, newPos, duration));
            StartCoroutine(scaleOverTime(player.transform, scaleUp, player.transform.localScale, duration));
            player.transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));

            yield return new WaitForSeconds(duration); 
            movesCounter++;
        }
        player.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        StartCoroutine(moveOverTime(player.transform, origPos, 1f));
        yield return new WaitForSeconds(1f);
        StopSpecialMove();
        isDancing = false;
    }

    // private IEnumerator DanceMoveDomaiAdvanced() {
    //     if (isDancing)
    //         yield break;
    //     isDancing = true;

    //     StartCoroutine(_danceMoveDomaiAdvanced(player));

    //     yield return new WaitForSeconds(3f);
    //     isDancing = false;  
    // }

    //private IEnumerator _danceMoveDomaiAdvanced(GameObject player) {}

    void AddDiscoLight()
    {
        Vector3 bottomLeft = mainCam.ViewportToWorldPoint(Vector3.zero);
        Vector3 topRight = mainCam.ViewportToWorldPoint(Vector3.one);

        Vector3 spawnPos = new Vector3(Random.Range(bottomLeft.x, topRight.x), Random.Range(bottomLeft.y, topRight.y), 0f);
        
        GameObject newDiscoLight = Instantiate(discoLightPrefab, spawnPos, Quaternion.identity, discoLightContainer);
        //newDiscoLight.AddComponent<DiscoLight>();
    }

    void StartSpecialMove()
    {
        //Black Background
        backgroundSR.color = Color.black;

        // TODO: Stop Disco Ligths
        foreach (Transform child in discoLightContainer)
        {
            DiscoLight script = child.GetComponent<DiscoLight>();
            script.stopColorChange = true;
        } 
    }

    void StopSpecialMove()
    {
        //Normal Backgroundcolor
        backgroundSR.color = Color.white;

        //Restart Disco Ligths
        foreach (Transform child in discoLightContainer)
        {
            DiscoLight script = child.GetComponent<DiscoLight>();
            script.stopColorChange = false;
        } 
    }
    void Start()
    {
         mainCam = Camera.main;
         backgroundSR = mainCam.GetComponentInChildren<SpriteRenderer>();
         tmpContainer = new GameObject();
    }

    void CreateMoshpit()
    {        
        for (int j = 1; j <= 4; j++)
        {
            for(int i = 0; i < 15*j*j; i++)
            {
                Vector3 playerPosition = player.transform.position;
                Vector3 spawnPos = Random.insideUnitCircle.normalized * (j+1.25f);
                spawnPos += playerPosition;
                GameObject newMoshpitMember = Instantiate(moshPitPrefab, spawnPos, Quaternion.identity, tmpContainer.transform);
            }
        }
        
        StartCoroutine(MoveMoshpit());
    }

    IEnumerator MoveMoshpit()
    {
        foreach(Transform child in tmpContainer.transform)
        {
            StartCoroutine(moveOverTime(child, player.transform.position, 0.25f));
        }
        yield return new WaitForSeconds(0.5f);
        RemoveMoshpit();
    }

    void RemoveMoshpit()
    {
        foreach(Transform child in tmpContainer.transform)
        {
             Destroy(child.gameObject);
        }
    }

    private void _squidGameRed(bool redLigth) {

        if (redLigth) 
            squidgame = true;
        else 
            squidgame = false;

        foreach (Transform child in discoLightContainer) {
            DiscoLight ligth = child.GetComponent<DiscoLight>();
            ligth.stopColorChange = false;
            if (redLigth)
                ligth.ChangeColor(Color.red);
            else 
                ligth.ChangeColor(Color.green);
            ligth.stopColorChange = true;
        } 

        foreach (Transform child in npcsContainer) {
            Npcs npc = child.GetComponent<Npcs>();
            if (redLigth) 
                npc.squidgame = true;
            else 
                npc.squidgame = false;
        }
    }

    private void SquidGame() {
        if (Random.value < 0.5f)
            _squidGameRed(true);
        else
            _squidGameRed(false);
    }

    // Update is called once per frame
    void Update()
    {        
        if (isDancing) return;

        input = input + Input.inputString;

        if(input.Contains("NINJA")) {
            ninjamode = !ninjamode;
            if(ninjamode)
            {
                speedMod = 0.5f;
                var currentColor = sr.color;
                currentColor.a -= 0.5f;
                sr.color = currentColor;
            }else
            {
                speedMod = 1f;
                var currentColor = sr.color;
                currentColor.a += 0.5f;
                sr.color = currentColor;
            }
            input = string.Empty;            
        }
        else if(input.Contains("DOGE")) {
            dogemode = !dogemode;

            if(dogemode) {
                foreach (Transform child in npcsContainer) {
                    Npcs npc = child.GetComponent<Npcs>();
                    npc.sr.sprite = Resources.Load<Sprite>("A2/Doge");
                }
            } 
            else {                
                foreach (Transform child in npcsContainer) {
                    Npcs npc = child.GetComponent<Npcs>();
                    npc.sr.sprite = npc.normalLook;
                }
            }

            input = string.Empty; 
        }
        else if (input.Contains("SQUIDGAME")) {
            InvokeRepeating(nameof(SquidGame), 0f, 1.5f);
            input = string.Empty; 
        }
        else if(input.Contains("MOSHPIT")) {
            doMoshpit = !doMoshpit;
            if (doMoshpit)
            {
                InvokeRepeating("CreateMoshpit", 0.5f, 1f);
            } else
            {
                CancelInvoke();
            }
            
            input = string.Empty;
        }

        if (input.Length > 30)
            input = input.Substring(9, input.Length-10);
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
            StartCoroutine(DanceMoveLinusBasic());
        if (Input.GetKeyDown(KeyCode.Alpha4))
            StartCoroutine(DanceMoveLinusAdvanced());
        //Dummy/Test Move at the moment
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //StartCoroutine(scaleOverTime(player.transform, new Vector3(1.5f, 1.5f, 1f), new Vector3(1f, 1f, 1f), 0.5f));
            //from https://stackoverflow.com/questions/37586407/rotate-gameobject-over-time
            Quaternion rotation2 = Quaternion.Euler(new Vector3(0f, 0f, 180));
            Quaternion oldRotation = player.transform.rotation;
            StartCoroutine(rotateObject(player, rotation2, oldRotation, 1f));
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            AddDiscoLight();
        }
        Vector3 moveVector = Vector3.zero;
        
        if (Input.GetKey(KeyCode.W)) {
            if (squidgame)
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            moveVector.y = 1;
        }
        if (Input.GetKey(KeyCode.A)) {
            if (squidgame)
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            moveVector.x = -1;
        }
        if (Input.GetKey(KeyCode.S)) {
            if (squidgame)
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            moveVector.y = -1;
        }
        if (Input.GetKey(KeyCode.D)) {
            if (squidgame)
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            moveVector.x = 1;
        } 

        // Normalize vector, so that magnitude for diagonal movement is also 1
        moveVector.Normalize();

        // TODO: this can be used for the NINJA Cheat later
        moveVector = moveVector * speedMod;

         // Frame rate independent movement
        transform.position += Time.deltaTime * speed * moveVector;
    }
}
