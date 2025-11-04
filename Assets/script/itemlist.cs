using UnityEngine;

[CreateAssetMenu(fileName = "itemlist", menuName = "Scriptable Objects/itemlist")]
public class itemlist : ScriptableObject
{
    public string itemname;
    public string itemtype;
    public string itemid;
    public float effectvalue;
    public string description;
    private string test;

}
