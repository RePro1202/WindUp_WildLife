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

        // ��� ��ǥ�� ����
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);
                availablePositions.Add(pos);
            }
        }

        // blockNum���� ��ġ ���� ������ ���� ����
        blockNum = Mathf.Min(blockNum, availablePositions.Count);

        for (int i = 0; i < blockNum; i++)
        {
            int randIndex = Random.Range(0, availablePositions.Count);
            Vector3Int pos = availablePositions[randIndex];
            availablePositions.RemoveAt(randIndex); // �ߺ� ����

            tilemap.SetTile(pos, obstacleBlock);
        }
    }


}
