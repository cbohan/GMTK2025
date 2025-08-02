public static class InterSceneData
{
    public static CatData Honey = new CatData()
    {
        Name = "Honey Loops",
        Description = "Shakey hands? Try using bigger lines!",
        Image = CatImage.Honey,
        Level = 1,
        
        Speed = StatValue.Medium,
        Stamina = StatValue.Medium,
        Acceleration = StatValue.Medium,

        Ult = UltType.Sticky_Honey,
    };


        Ult = UltType.Chungus_Mode,
    public static CatData Oreo = new CatData()
    {
        Name = "Oreos?",
        Description = "Smaller lines but smaller boosts!",
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
        Description = "Big lines mean bigger boosts!",
        Image = CatImage.Apple,
        Level = 0,

        Speed = StatValue.Medium,
        Stamina = StatValue.Low,
        Acceleration = StatValue.High,

        Ult = UltType.Apple_Jacked,
    };

    public static CatData Loop = new CatData()
    {
        Name = "Brother Loops",
        Description = "Crush your opponents with the CHONK!",
        Image = CatImage.Loop,
        Level = 0,

        Speed = StatValue.Medium,
        Stamina = StatValue.High,
        Acceleration = StatValue.Low,
        
        Ult = UltType.Chungus_Mode,
    };

    public static int PlayerRacePlacement;
    
    public static bool GameStarted = false;

    public static CatData PlayerCat = Honey;
    public static CatData[] AiCats = new CatData[]
    {
        Oreo,
        Apple,
        Loop,
    };    
}
