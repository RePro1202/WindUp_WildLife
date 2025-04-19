using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BlockSpawner : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;
    [SerializeField] TileBase obstacleBlock;
    [SerializeField] TileBase trickBlock;

    private int width = 18;
    private int height = 10;
    private int obstacleNum = 30;

    private void Start()
    {
        SpawnBlocks(width, height, obstacleNum);   
    }

    private void SpawnBlocks(int width, int height, int blockNum)
    {
        List<Vector3Int> availablePositions = new List<Vector3Int>();

        // 모든 좌표를 수집
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);
                availablePositions.Add(pos);
            }
        }

        // blockNum보다 위치 수가 작으면 에러 방지
        blockNum = Mathf.Min(blockNum, availablePositions.Count);

        for (int i = 0; i < blockNum; i++)
        {
            int randIndex = Random.Range(0, availablePositions.Count);
            Vector3Int pos = availablePositions[randIndex];
            availablePositions.RemoveAt(randIndex); // 중복 방지

            tilemap.SetTile(pos, obstacleBlock);
        }
    }


}
