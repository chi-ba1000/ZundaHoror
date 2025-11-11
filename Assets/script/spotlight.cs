using UnityEngine;

public class spotlight : MonoBehaviour
{
    public float MAXspotlightHP;
    public float batterypower;
    public GameObject lightobject;
    public float NOWspotlightHP;  
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        NOWspotlightHP = MAXspotlightHP;
    }

    // Update is called once per frame
    void Update()
    {
        if (NOWspotlightHP <= 0)
        {
            lightobject.SetActive(false);
        }
        else if (NOWspotlightHP > 0)
        {
            lightobject.SetActive(true);
            NOWspotlightHP--;
        }
    }
    public void Getbattery()
    {
        float recharge = Random.Range(batterypower - 10f, batterypower + 10f);
        NOWspotlightHP += recharge;
        Debug.Log(recharge);
    }
}
