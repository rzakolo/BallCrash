using UnityEngine;

public class SaveManager
{
    public int Record { get; private set; }
    public SaveManager()
    {
        if (!PlayerPrefs.HasKey("record"))
        {
            PlayerPrefs.SetInt("record", 0);
        }
        else
        {
            Record = PlayerPrefs.GetInt("record");
        }
    }
    public void SetRecord(int record)
    {
        if (record > this.Record)
        {
            PlayerPrefs.SetInt("record", record);
        }
    }
}
