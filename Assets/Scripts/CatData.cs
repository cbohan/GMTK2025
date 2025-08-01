public enum CatImage
{ 
    HoneyLoops,
    TEMP_ChonkerBoi,
    TEMP_DefinitelyACat,
    TEMP_GreenFrootLoop,
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

    public StatValue Speed;
    public StatValue Acceleration;
    public StatValue Stamina;
}
