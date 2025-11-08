[System.Serializable]
public class Item
{
    //このスクリプトはどこにもアタッチしなくていい
    //このクラスによりインスタンスを作成
    public string Name;
    public int Id;
    //インスタンスの中身を以下のコンストラクタで整備
    public Item(string newName, int newId)
    {
        this.Name = newName;
        this.Id = newId;     
    }
}
