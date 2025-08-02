using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CatSelection : MonoBehaviour
{
    public CatData Data;
    public CatData[] AiDatas;

    [SerializeField] private RawImage _image;
    [SerializeField] private ImageLookup _imageLookup;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _speed;
    [SerializeField] private TMP_Text _acceleration;
    [SerializeField] private TMP_Text _stamina;
    [SerializeField] private Button _selectButton;

    public void Init(CatData catData, CatData[] aiCats)
    {
        Data = catData;
        AiDatas = aiCats;

        _image.texture = _imageLookup.GetNonRaceTexture(Data.Image);
        _name.text = catData.Name;
        _speed.text = $"Speed: {catData.Speed}";
        _acceleration.text = $"Acceleration: {catData.Acceleration}";
        _stamina.text = $"Stamina: {catData.Stamina}";

        if (catData.Level == 0)
        {
            _image.color = Color.black;
            _name.text = "???????";
            _speed.text = "Speed: ???";
            _acceleration.text = "Acceleration: ???";
            _stamina.text = "Stamina: ???";
            _selectButton.interactable = false;
        }
    }

    public void Click()
    {
        InterSceneData.PlayerCat = Data;
        InterSceneData.AiCats = AiDatas;

        SceneManager.LoadScene("RacingScene");
    }
}
