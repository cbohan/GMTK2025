public enum CatImage
{ 
    Honey,
    Loop,
    Oreo,
    Apple,
}

public enum StatValue
{ 
    Low,
    Medium,
    High
}

public struct CatData
{
    public string Name;
    public CatImage Image;

    public int Level;
    public bool IsUnlocked;

    public StatValue Speed;
    public StatValue Acceleration;
    public StatValue Stamina;
}
