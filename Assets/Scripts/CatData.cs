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

public enum UltType
{ 
    Sticky_Honey, // All other racers are momentarily stuck in honey for half a second
    Sugar_Rush, // Lurches forward and if she passes another cat she destroys it
    Apple_Jacked, // Short super boost
    Chungus_Mode, // Get big, slightly increase in speed, crush cats in front of you 
}

public struct CatData
{
    public string Name;
    public CatImage Image;

    public int Level;
    public bool IsUnlocked;

    public string Description;

    public StatValue Speed;
    public StatValue Acceleration;
    public StatValue Stamina;
    public UltType Ult;
}
