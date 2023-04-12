using UnityEngine;

public class PerlinNoise : MonoBehaviour
{
    [SerializeField] private float _seed;
    
    public float[,] noiseMap;

    public void GeneratePerlinNoise(int Width, int Height, float scale)
    {
        noiseMap = new float[Width, Height];
        if (_seed == 0)
            _seed = Random.Range(1, 100000);

        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                float XPos = x / scale;
                float YPos = y / scale;

                float noiseValue = Mathf.PerlinNoise(XPos + _seed / 3.4f, YPos + _seed / 7.1f);
                noiseMap[x, y] = noiseValue;
            }
        }
    }
}
