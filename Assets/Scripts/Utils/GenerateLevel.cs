using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GenerateLevel : MonoBehaviour
{
    [SerializeField] private int _width;
    [SerializeField] private int _height;
    
    [SerializeField] private GameObject _tile;

    [SerializeField] private Transform _parent;
    
    [SerializeField] private PerlinNoise _perlinNoise;
    [SerializeField] private EventsManager _eventsManager;

    [SerializeField] private Transform _grassParent;
    [SerializeField] private Transform _riverParent;
    [SerializeField] private Transform _forestParent;
    [SerializeField] private Transform _houseParent;
    [SerializeField] private Transform _personParent;
    [SerializeField] private Transform _eventParent;
    
    [SerializeField] private GameObject[] _tilePrefab;
    [SerializeField] private GameObject _riverPrefab;
    [SerializeField] private GameObject _forestPrefab;
    [SerializeField] private GameObject _housePrefab;
    [SerializeField] private GameObject _personPrefab;
    [SerializeField] private GameObject _eventPrefab;

    [HideInInspector] public Vector2 lastPersonPos;
    public Action _personSpawned;

    private int _worldWidth = 310;
    private int _worldHeight = 310;

    private float _riverThreshold = 0.85f;
    private float _forestThreshold = 0.3f;
    private float _houseThreshold = 0.45f;
    private float _eventThreshold = 0.4f;

    private float _personSpawnDistance = 10f;

    public void GenerateTiles()
    {
        List<Vector3> housePositions = new List<Vector3>();
        List<Vector3> personPositions = new List<Vector3>();
        
        // int maxHouses = Mathf.CeilToInt((float)(_width * _height) / 100f); // 1 house per 25 tiles
        int maxHouses = 50;
        int maxPersons = 10; // maximum of 20 persons

        int houseTilesCounter = 0;
        int personsTilesCounter = 0;
        int eventsTilesCounter = 0;

        int riverCounter = 0;
        int forestCounter = 0;
        int grassCounter = 0;
        int housesCounter = 0;
        int personesCounter = 0;

        for (int x = 0; x < _width; x += 5)
        {
            for (int y = 0; y < _height; y += 5)
            {
                float noiseValue = _perlinNoise.noiseMap[x, y];
                Vector3 tilePosition = new Vector3(x, y, 0f);
                
                if (noiseValue < _forestThreshold)
                {
                    GameObject forestTile = Instantiate(_forestPrefab, tilePosition, Quaternion.identity, _forestParent);
                    forestTile.transform.localScale = Vector3.one * 5;
                    forestCounter++;
                }
                else if (noiseValue < _eventThreshold)
                {
                    eventsTilesCounter++;
                    if (eventsTilesCounter >= 50 && Random.value <= 0.5f)
                    {
                        eventsTilesCounter = 0;
                        GameObject eventTile = Instantiate(_eventPrefab, tilePosition, Quaternion.identity, _eventParent);
                        eventTile.transform.localScale = Vector3.one * 5;
                        _eventsManager.eventsPosition.Add(tilePosition);
                        _eventsManager.listGrown?.Invoke();
                        int randomTile = Random.Range(0, _tilePrefab.Length);
                        if (randomTile <= 6)
                            randomTile = Random.Range(0, _tilePrefab.Length);
                        if (randomTile == 3)
                            randomTile = Random.Range(0, _tilePrefab.Length);
                        GameObject grassTile = Instantiate(_tilePrefab[randomTile], tilePosition, Quaternion.identity, _grassParent);
                        grassTile.transform.localScale = Vector3.one * 5;
                        grassCounter++;
                    }
                    else
                    {
                        int randomTile = Random.Range(0, _tilePrefab.Length);
                        if (randomTile <= 6)
                            randomTile = Random.Range(0, _tilePrefab.Length);
                        if (randomTile == 3)
                            randomTile = Random.Range(0, _tilePrefab.Length);
                        GameObject grassTile = Instantiate(_tilePrefab[randomTile], tilePosition, Quaternion.identity, _grassParent);
                        grassTile.transform.localScale = Vector3.one * 5;
                        grassCounter++;
                    }
                }
                else if (noiseValue < _houseThreshold && housePositions.Count < maxHouses)
                {
                    houseTilesCounter++;
                    personsTilesCounter++;
                    if (houseTilesCounter >= 20)
                    {
                        houseTilesCounter = 0;
                        //bool isNearRiver = CheckIsNearRiver(tilePosition, 3f);
                        //Vector3 housePosition = isNearRiver ? GetPositionAwayFromRiver(tilePosition) : tilePosition;
                        Vector3 housePosition = tilePosition;
                        //Debug.Log($"House # {housesCounter} is near river: {isNearRiver}");
        
                        if (!housePositions.Contains(housePosition))
                        {
                            GameObject houseTile = Instantiate(_housePrefab, housePosition, Quaternion.identity, _houseParent);
                            houseTile.transform.localScale = Vector3.one * 5;
        
                            housePositions.Add(housePosition);

                            housesCounter++;
                            if (personsTilesCounter >= 20)
                            {
                                personsTilesCounter = 0;
                                if (personPositions.Count < maxPersons && Random.value < 0.5f)
                                {
                                    Vector3 personPosition = GetRandomPositionNearby(housePosition, _personSpawnDistance);
                                    personPosition.z = -10f;
        
                                    if (!personPositions.Contains(personPosition))
                                    {
                                        Instantiate(_personPrefab, personPosition, Quaternion.identity, _personParent);
                                        personPositions.Add(personPosition);
                                        personesCounter++;
                                        lastPersonPos = personPosition;
                                        _personSpawned?.Invoke();
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        int randomTile = Random.Range(0, _tilePrefab.Length);
                        if (randomTile <= 6)
                            randomTile = Random.Range(0, _tilePrefab.Length);
                        if (randomTile == 3)
                            randomTile = Random.Range(0, _tilePrefab.Length);
                        GameObject grassTile = Instantiate(_tilePrefab[randomTile], tilePosition, Quaternion.identity, _grassParent);
                        grassTile.transform.localScale = Vector3.one * 5;
                        grassCounter++;
                    }
                }
                else if (noiseValue < _riverThreshold && noiseValue + 0.4f < _riverThreshold)
                {
                    GameObject riverTile = Instantiate(_riverPrefab, tilePosition, Quaternion.identity, _riverParent);
                    riverTile.transform.localScale = Vector3.one * 5;
                    riverCounter++;
                }
                else
                {
                    int randomTile = Random.Range(0, _tilePrefab.Length);
                    if (randomTile <= 6)
                        randomTile = Random.Range(0, _tilePrefab.Length);
                    if (randomTile == 3)
                        randomTile = Random.Range(0, _tilePrefab.Length);
                    GameObject grassTile = Instantiate(_tilePrefab[randomTile], tilePosition, Quaternion.identity, _grassParent);
                    grassTile.transform.localScale = Vector3.one * 5;
                    grassCounter++;
                }
            }
        }
        
        Debug.Log($"Houses: {housesCounter}, Persons: {personesCounter}, Forests: {forestCounter}, Rivers: {riverCounter}, Grasses: {grassCounter}");
        
    }

    private Vector3 GetRandomPositionNearby(Vector3 center, float distance)
    {
        Vector3 randomDirection = Random.insideUnitSphere * distance;
        randomDirection.y = 0f;
        return center + randomDirection;
    }
}