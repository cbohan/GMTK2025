public static class InterSceneData
{
    public static CatData Honey = new CatData()
    {
        Name = "Honey Loops",
        Image = CatImage.Honey,
        Level = 1,
        IsUnlocked = true,
        
        Speed = StatValue.Medium,
        Stamina = StatValue.Medium,
        Acceleration = StatValue.Medium,
    };

    public static CatData Loop = new CatData()
    {
        Name = "Brother Loops",
        Image = CatImage.Loop,
        Level = 0,
        IsUnlocked = false,

        Speed = StatValue.Medium,
        Stamina = StatValue.High,
        Acceleration = StatValue.Low,
    };

    public static CatData Oreo = new CatData()
    {
        Name = "Oreos?",
        Image = CatImage.Oreo,
        Level = 0,
        IsUnlocked = false,

        Speed = StatValue.High,
        Stamina = StatValue.Medium,
        Acceleration = StatValue.Low,
    };

    public static CatData Apple = new CatData()
    {
        Name = "Jacked Cat",
        Image = CatImage.Apple,
        Level = 0,
        IsUnlocked = false,

        Speed = StatValue.Medium,
        Stamina = StatValue.Low,
        Acceleration = StatValue.High,
    };

    public static int PlayerRacePlacement;

    public static CatData PlayerCat = Honey;
    public static CatData[] AiCats = new CatData[]
    {
        Loop,
        Oreo,
        Apple,
    };    
}
