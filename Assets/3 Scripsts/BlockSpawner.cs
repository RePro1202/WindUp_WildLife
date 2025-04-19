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

    // ��ֹ�, �Ǵ� Ʈ�� ��� ���� ����
    private void SpawnBlocks(int width, int height, int blockNum)
    {
        for (int i = 0; i < blockNum; i++)
        {
            int randX = Random.Range(0, width);
            int randY = Random.Range(0, height);
            Vector3Int pos = new Vector3Int(randX, randY, 0);

            // �ߺ� ���� 
            if (tilemap.GetTile(pos))
            {
                tilemap.SetTile(pos, obstacleBlock);
            }
            else
            {
                i--; // ���������� �ٽ� �õ�
            }
        }
    }

}
