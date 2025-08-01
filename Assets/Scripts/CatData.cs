public enum CatImage
{ 
    HoneyLoops,
    TEMP_ChonkerBoi,
    TEMP_DefinitelyACat,
    TEMP_GreenFrootLoop,
}

public struct CatData
{
    public string Name;
    public CatImage Image;

    public float TrotSpeedMult;
    public float MaxSpeed;
    public float SlowdownRate;
    public float Acceleration;
}
