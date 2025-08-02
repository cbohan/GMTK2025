public static class InterSceneData
{
    public static CatData Honey = new CatData()
    {
        Name = "Honey Loops",
        Image = CatImage.Honey,
        Level = 1,
        
        Speed = StatValue.Medium,
        Stamina = StatValue.Medium,
        Acceleration = StatValue.Medium,

        Ult = UltType.Sticky_Honey,
    };

    public static CatData Loop = new CatData()
    {
        Name = "Brother Loops",
        Image = CatImage.Loop,
        Level = 0,

        Speed = StatValue.Medium,
        Stamina = StatValue.High,
        Acceleration = StatValue.Low,

        Ult = UltType.Chungus_Mode,
    };

    public static CatData Oreo = new CatData()
    {
        Name = "Oreos?",
        Image = CatImage.Oreo,
        Level = 0,

        Speed = StatValue.High,
        Stamina = StatValue.Medium,
        Acceleration = StatValue.Low,

        Ult = UltType.Sugar_Rush,
    };

    public static CatData Apple = new CatData()
    {
        Name = "Jacked Cat",
        Image = CatImage.Apple,
        Level = 0,

        Speed = StatValue.Medium,
        Stamina = StatValue.Low,
        Acceleration = StatValue.High,

        Ult = UltType.Apple_Jacked,
    };

    public static int PlayerRacePlacement;
    public static bool GameStarted;

    public static CatData PlayerCat = Honey;
    public static CatData[] AiCats = new CatData[]
    {
        Loop,
        Oreo,
        Apple,
    };    
}
