using UnityEngine;
using UnityEngine.UI;

public class CharacterCornerSprite : MonoBehaviour
{
    [SerializeField] private Sprite _spriteRadar;
    [SerializeField] private Sprite _spriteGun1;
    [SerializeField] private Sprite _spriteGun2;

    [SerializeField] private Image _image;
    private Sprite _currentSprite;
    
    public static CharacterCornerSprite Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SetSprite(1);
    }

    public void SetSprite(int input)
    {
        switch (input)
        {
            case 0:
                _currentSprite = _spriteRadar;
                break;
            case 1:
                _currentSprite = _spriteGun1;
                break;
            case 2:
                _currentSprite = _spriteGun2;
                break;
            default:
                Debug.LogError("No sprite");
                break;
        }
        _image.sprite = _currentSprite;
    }
}
