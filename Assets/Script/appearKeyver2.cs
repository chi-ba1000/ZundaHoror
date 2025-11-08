using UnityEngine;

public class appearKeyVer2 : MonoBehaviour
{
    [SerializeField] private GameObject keyPrefab, enemyPrefab, player;
    [SerializeField] private float keyX = 0, keyY = 0, keyZ = 0 ,enemyRangeX = 0 ,enemyRangeZ = 2;
    //daiza型の配列
    [SerializeField] private baseComp[] baseScript = new baseComp[5];
    private Vector3 keyPos, enemyPos, playerPos;
    private baseComp item1, item2, item3, item4, item5;
    [HideInInspector] public bool enemyC = false, keyC = false;
    void Start()
    {
        keyPos = new Vector3(keyX, keyY, keyZ);
        

        item1 = baseScript[0];
        item2 = baseScript[1];
        item3 = baseScript[2];
        item4 = baseScript[3];
        item5 = baseScript[4];
    }

    void Update()
    {
        playerPos = player.transform.position;
        enemyPos = new Vector3(playerPos.x + enemyRangeX, playerPos.y, playerPos.z + enemyRangeZ);


        if (keyC == false && item1.put && item2.put && item3.put && item4.put && item5.put && item1.item1C && item2.item2C && item3.item3C && item4.item4C && item5.item5C)
        {
            Instantiate(keyPrefab, keyPos, Quaternion.identity);
            keyC = true;
        }
        else if (enemyC == false && item1.put && item2.put && item3.put && item4.put && item5.put && (item1.item1C == false || item2.item2C == false || item3.item3C == false || item4.item4C == false || item5.item5C == false))
        {
            Instantiate(enemyPrefab, enemyPos, Quaternion.identity);
            enemyC = true;
        }


        if (item1.put == false || item2.put == false || item3.put== false || item4.put== false || item5.put== false)
        {
            enemyC = false;
        }          
    }
}
