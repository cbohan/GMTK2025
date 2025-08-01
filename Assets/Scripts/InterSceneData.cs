public static class InterSceneData
{
    private static CatData HoneyLoops = new CatData()
    {
        Name = "Honey Loops",
        Image = CatImage.HoneyLoops,

        TrotSpeedMult = .5f,
        MaxSpeed = 5f,
        SlowdownRate = .1f,
        Acceleration = 2f,
    };

    private static CatData ChonkerBoi = new CatData()
    {
        Name = "Chonker Boi",
        Image = CatImage.TEMP_ChonkerBoi,

        TrotSpeedMult = .5f,
        MaxSpeed = 5f,
        SlowdownRate = .1f,
        Acceleration = 2f,
    };

    private static CatData DefinitelyACat = new CatData()
    {
        Name = "Definitely A Cat",
        Image = CatImage.TEMP_DefinitelyACat,

        TrotSpeedMult = .5f,
        MaxSpeed = 5f,
        SlowdownRate = .1f,
        Acceleration = 2f,
    };

    private static CatData GreenFrootLoop = new CatData()
    {
        Name = "Green Froot Loop",
        Image = CatImage.TEMP_GreenFrootLoop,

        TrotSpeedMult = .5f,
        MaxSpeed = 5f,
        SlowdownRate = .1f,
        Acceleration = 2f,
    };

    public static int PlayerRacePlacement;

    public static CatData PlayerCat = HoneyLoops;
    public static CatData[] AiCats = new CatData[]
    {
        ChonkerBoi,
        DefinitelyACat,
        GreenFrootLoop,
    };
    
}
