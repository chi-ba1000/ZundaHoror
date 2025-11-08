using UnityEngine;

public class appearKey : MonoBehaviour
{
    [SerializeField] private GameObject keyPrefab, enemyPrefab, player;
    [SerializeField] private float keyX = 0, keyY = 0, keyZ = 0 ,enemyRangeX = 0 ,enemyRangeZ = 2;
    //daiza型の配列
    [SerializeField] private daiza[] baseScript = new daiza[5];
    private Vector3 keyPos, enemyPos, playerPos;
    private daiza black, blue, green, red, white;
    [HideInInspector] public bool enemyC = false, keyC = false;
    void Start()
    {
        keyPos = new Vector3(keyX, keyY, keyZ);
        

        black = baseScript[0];
        blue = baseScript[1];
        green = baseScript[2];
        red = baseScript[3];
        white = baseScript[4];
    }

    void Update()
    {
        playerPos = player.transform.position;
        enemyPos = new Vector3(playerPos.x + enemyRangeX, playerPos.y, playerPos.z + enemyRangeZ);


        if (keyC == false && black.put && blue.put && green.put && red.put && white.put && black.blackC && blue.blueC && green.greenC && red.redC && white.whiteC)
        {
            Instantiate(keyPrefab, keyPos, Quaternion.identity);
            keyC = true;
        }
        else if (enemyC == false && black.put && blue.put && green.put && red.put && white.put && (black.blackC == false || blue.blueC == false || green.greenC == false || red.redC == false || white.whiteC == false))
        {
            Instantiate(enemyPrefab, enemyPos, Quaternion.identity);
            enemyC = true;
        }


        if (black.put == false || blue.put == false || green.put== false || red.put== false || white.put== false)
        {
            enemyC = false;
        }          
    }
}
