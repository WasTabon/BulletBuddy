using UnityEngine;
using Random = UnityEngine.Random;

public class WorldCreator : MonoBehaviour
{
    [SerializeField] private int _width;
    [SerializeField] private int _height;
    [SerializeField] private float _scale;
    
    [SerializeField] private  GameObject _firstObjectPrefab;
    [SerializeField] private  GameObject _secondObjectPrefab;
    [SerializeField] private  GameObject _thirdObjectPrefab;
    [SerializeField] private  GameObject _bombObjectPrefab;

    [SerializeField] private GameObject _allyBomb1;
    [SerializeField] private GameObject _allyBomb2;
    [SerializeField] private GameObject _allyBomb3;

    [SerializeField] private GameObject[] _allies;
    
    [SerializeField] private PerlinNoise _perlinNoise;
    [SerializeField] private GenerateLevel _generateLevel;
    [SerializeField] private NoiseTexture _noiseTexture;
    [SerializeField] private EventsManager _eventsManager;

    private int _allybombSpawned = 0;
    
    private Vector2 firstPos;
    private Vector2 secondPos;
    private Vector2 thirdPos;
    private Vector2 bombPos;


    private void Start()
    {
        _perlinNoise.GeneratePerlinNoise(_width, _height, _scale);

        _noiseTexture._texture2D = new Texture2D(_width, _height);
        _noiseTexture.GenerateTexture(_width, _height, _scale);

        _generateLevel._personSpawned += SpawnPersons;
        _eventsManager.listGrown += SpawnEvents;
        
        _generateLevel.GenerateTiles();
        
        SpawnCluesAndBomb();
    }

    private void SpawnCluesAndBomb()
    {
            // Define ranges for the distances
        float[] distances = { Random.Range(100f, 200f), Random.Range(100f, 200f), Random.Range(100f, 200f), Random.Range(200f, 300f) };

        // Define ranges for the angles
        float[] angles = { Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f) };

        // Generate positions in polar coordinates
        Vector2[] positions = new Vector2[4];
        for (int i = 0; i < 4; i++)
        {
            positions[i] = PolarToCartesian(angles[i], distances[i]);
        }

        // Check if all positions are valid
        bool isValid = false;
        do
        {
            isValid = true;

            // Check if any position is invalid
            for (int i = 0; i < 4; i++)
            {
                if (positions[i].x < 0f || positions[i].y < 0f)
                {
                    isValid = false;
                    break;
                }

                for (int j = i + 1; j < 4; j++)
                {
                    if (Vector2.Distance(positions[i], positions[j]) < 100f)
                    {
                        isValid = false;
                        break;
                    }
                }
            }

            // Generate new positions if any position is invalid
            if (!isValid)
            {
                for (int i = 0; i < 4; i++)
                {
                    distances[i] = Random.Range(100f, 200f);
                    angles[i] = Random.Range(0f, 360f);
                    positions[i] = PolarToCartesian(angles[i], distances[i]);
                }
            }

        } while (!isValid);

        // Spawn objects at the generated positions
        Instantiate(_firstObjectPrefab, positions[0], Quaternion.identity);
        Instantiate(_secondObjectPrefab, positions[1], Quaternion.identity);
        Instantiate(_thirdObjectPrefab, positions[2], Quaternion.identity);
        Instantiate(_bombObjectPrefab, positions[3], Quaternion.identity);

        Debug.Log($"Distance 1: {Vector2.Distance(positions[0], positions[3])}, Distance 2: {Vector2.Distance(positions[1], positions[3])} Distance 3: {Vector2.Distance(positions[2], positions[3])}");
    }

    private Vector2 PolarToCartesian(float angle, float distance)
    {
        float x = distance * Mathf.Cos(angle * Mathf.Deg2Rad);
        float y = distance * Mathf.Sin(angle * Mathf.Deg2Rad);
        return new Vector2(x, y);
    }
    
    public void SpawnPersons()
    {
        if (_allybombSpawned == 0)
        {
            Instantiate(_allyBomb1, _generateLevel.lastPersonPos, Quaternion.identity);
            _allybombSpawned++;
        }
        else if (_allybombSpawned == 1)
        {
            Instantiate(_allyBomb2, _generateLevel.lastPersonPos, Quaternion.identity);
            _allybombSpawned++;
        }
        else if (_allybombSpawned == 2)
        {
            Instantiate(_allyBomb3, _generateLevel.lastPersonPos, Quaternion.identity);
            _allybombSpawned++;
        }
        else
        {
             //Instantiate(_allies[Random.Range(0, _allies.Length)], _generateLevel.lastPersonPos, Quaternion.identity);
             Instantiate(_allies[0], _generateLevel.lastPersonPos, Quaternion.identity);
        }
    }

    private void SpawnEvents()
    {
        
    }
}
