public static class InterSceneData
{
    public static CatData HoneyLoops = new CatData()
    {
        Name = "Honey Loops",
        Image = CatImage.HoneyLoops,

        Speed = StatValue.Medium,
        Stamina = StatValue.Medium,
        Acceleration = StatValue.Medium,
    };

    public static CatData ChonkerBoi = new CatData()
    {
        Name = "Chonker Boi",
        Image = CatImage.TEMP_ChonkerBoi,

        Speed = StatValue.Medium,
        Stamina = StatValue.High,
        Acceleration = StatValue.Low,
    };

    public static CatData DefinitelyACat = new CatData()
    {
        Name = "Definitely A Cat",
        Image = CatImage.TEMP_DefinitelyACat,

        Speed = StatValue.High,
        Stamina = StatValue.Medium,
        Acceleration = StatValue.Low,
    };

    public static CatData GreenFrootLoop = new CatData()
    {
        Name = "Green Froot Loop",
        Image = CatImage.TEMP_GreenFrootLoop,

        Speed = StatValue.Medium,
        Stamina = StatValue.Low,
        Acceleration = StatValue.High,
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
