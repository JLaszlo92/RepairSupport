[System.Serializable]
public class HeaderInfo
{
    public string sceneName;
    public string author;
    public string timestamp;
}

[System.Serializable]
public class MarkData
{
    public string name;
    public float[] StartPosition;
    public float[] StartRotation;
    public float[] StartScale;
    public string MovementType;
    public float MovementSpeed;
    public float[] EndPosition;
}

[System.Serializable]
public class SceneWrapper
{
    public HeaderInfo header;
    public MarkData[] marks;
}

[System.Serializable]
public class SceneConfig
{
    public string[] sceneList;
    public int currentIndex;
    public string[] completed;
}
