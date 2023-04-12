using UnityEngine;

public class NoiseTexture : MonoBehaviour
{
     [SerializeField] private int _width;
    [SerializeField] private int _height;
    [SerializeField] private float _scale;
    
    [SerializeField] private PerlinNoise _perlinNoise;
    [SerializeField] private GenerateLevel _generateLevel;

    [HideInInspector] public Texture2D _texture2D;

    private void Start()
    {
       // _texture2D = new Texture2D(_width, _height);
        
        //_perlinNoise.GeneratePerlinNoise(_width, _height, _scale);
        //GenerateTexture(_width, _height, _scale);

        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = _texture2D;
        
        //_generateLevel.GenerateWorld();
    }

    public void GenerateTexture(int Width, int Height, float Scale)
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                _texture2D.SetPixel(x, y, new Color(_perlinNoise.noiseMap[x, y], _perlinNoise.noiseMap[x, y], _perlinNoise.noiseMap[x, y]));
            }
        }
        _texture2D.Apply();
    }
}
