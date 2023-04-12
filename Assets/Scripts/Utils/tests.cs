using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tests : MonoBehaviour
{
    public GameObject firstObjectPrefab;
    public GameObject secondObjectPrefab;
    public GameObject thirdObjectPrefab;
    public GameObject bombObjectPrefab;

    private Vector2 firstPos;
    private Vector2 secondPos;
    private Vector2 thirdPos;
    private Vector2 bombPos;

    private void Start()
    {
        SpawnObjects();
    }

    private void SpawnObjects()
    {
        bool validPositions = false;
        while (!validPositions)
        {
            // Generate random positions for all objects in polar coordinates
            float angle1 = Random.Range(0f, 360f);
            float distance1 = Random.Range(100f, 200f);
            firstPos = PolarToCartesian(angle1, distance1);

            float angle2 = Random.Range(0f, 360f);
            float distance2 = Random.Range(100f, 200f);
            secondPos = PolarToCartesian(angle2, distance2);

            float angle3 = Random.Range(0f, 360f);
            float distance3 = Random.Range(100f, 200f);
            thirdPos = PolarToCartesian(angle3, distance3);

            float angle4 = Random.Range(0f, 360f);
            float distance4 = Random.Range(0f, 300f);
            bombPos = PolarToCartesian(angle4, distance4);

            // Check if all positions are valid
            if (Vector2.Distance(firstPos, secondPos) >= 100f &&
                Vector2.Distance(secondPos, thirdPos) >= 100f &&
                Vector2.Distance(thirdPos, firstPos) >= 100f &&
                Vector2.Distance(thirdPos, bombPos) >= 200f &&
                Vector2.Distance(secondPos, bombPos) >= 200f &&
                Vector2.Distance(firstPos, bombPos) >= 200f)
            {
                validPositions = true;
            }
        }

        // Spawn objects at the generated positions
        Instantiate(firstObjectPrefab, firstPos, Quaternion.identity);
        Instantiate(secondObjectPrefab, secondPos, Quaternion.identity);
        Instantiate(thirdObjectPrefab, thirdPos, Quaternion.identity);
        Instantiate(bombObjectPrefab, bombPos, Quaternion.identity);
        Debug.Log($"Distance 1: {Vector2.Distance(firstPos, bombPos)}, Distance 2: {Vector2.Distance(secondPos, bombPos)} Distance 3: {Vector2.Distance(thirdPos, bombPos)}");
    }
   

    private Vector2 PolarToCartesian(float angle, float distance)
    {
        float x = distance * Mathf.Cos(angle * Mathf.Deg2Rad);
        float y = distance * Mathf.Sin(angle * Mathf.Deg2Rad);
        return new Vector2(x, y);
    }
}
