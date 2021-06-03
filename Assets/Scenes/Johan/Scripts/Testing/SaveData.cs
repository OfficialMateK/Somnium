using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    private static SaveData _current;

    public static SaveData current
    {
        get
        {
            if (_current == null)
            {
                _current = new SaveData();
            }
            return _current;
        }
        set
        {
            if (value != null)
            {
                _current = value;
            }
        }
    }

    //public PlayerSaveData playerSaveData = new PlayerSaveData();
    public int levelNumber;
    public bool crystalCompleted;
    public bool collectionCompleted;
    public bool captureCompleted;

}
