using UnityEngine;

[CreateAssetMenu(fileName = "ImageLookup", menuName = "Scriptable Objects/ImageLookup")]
public class ImageLookup : ScriptableObject
{
    public Texture HoneyLoopsRace;
    public Texture TEMP_ChonkerBoiRace;
    public Texture TEMP_DefinitelyACatRace;
    public Texture TEMP_GreenFrootLoopRace;

    public Texture GetRaceTexture(CatImage image)
    {
        switch (image)
        {
            case CatImage.HoneyLoops:
                return HoneyLoopsRace;
            case CatImage.TEMP_ChonkerBoi:
                return TEMP_ChonkerBoiRace;
            case CatImage.TEMP_DefinitelyACat:
                return TEMP_DefinitelyACatRace;
            case CatImage.TEMP_GreenFrootLoop:
            default:
                return TEMP_GreenFrootLoopRace;
        }
    }
}
