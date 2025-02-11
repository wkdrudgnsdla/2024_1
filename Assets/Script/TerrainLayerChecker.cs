using UnityEngine;

public class MultiTerrainChecker : MonoBehaviour
{

    public int layerIndex;
    void Update()
    {
        // ��ü�� ��ġ�� �� �Ʒ��� �����ϴ� Terrain�� �����ɴϴ�.
        Terrain terrainUnderneath = GetTerrainUnderneath(transform.position);
        if (terrainUnderneath != null)
        {
            layerIndex = GetTerrainLayerIndex(transform.position, terrainUnderneath);
            //Debug.Log($"��ü�� ��� �ִ� Terrain: {terrainUnderneath.name} / �������� Layer �ε���: {layerIndex}");
        }
        else
        {
            Debug.Log("��ü �Ʒ��� Terrain�� �����ϴ�.");
        }
    }

    /// <summary>
    /// �־��� ���� ��ǥ�� ���ԵǴ� Terrain�� ��ȯ�մϴ�.
    /// ���� Terrain�� ���� ���, ��ǥ�� ���ԵǴ� ù ��° Terrain�� ��ȯ�մϴ�.
    /// </summary>
    /// <param name="worldPos">���� ��ǥ</param>
    /// <returns>�ش� ��ǥ�� �����ϴ� Terrain, ������ null</returns>
    Terrain GetTerrainUnderneath(Vector3 worldPos)
    {
        // �� ���� ��� Ȱ��ȭ�� Terrain���� �����ɴϴ�.
        Terrain[] terrains = Terrain.activeTerrains;
        foreach (Terrain terrain in terrains)
        {
            Vector3 terrainPos = terrain.transform.position;
            Vector3 terrainSize = terrain.terrainData.size;

            // ���� ��ǥ�� Terrain�� ���� ���� �ִ��� üũ�մϴ�.
            if (worldPos.x >= terrainPos.x && worldPos.x <= terrainPos.x + terrainSize.x &&
                worldPos.z >= terrainPos.z && worldPos.z <= terrainPos.z + terrainSize.z)
            {
                return terrain;
            }
        }
        return null;
    }

    /// <summary>
    /// ������ Terrain���� worldPos ��ġ�� ���ĸ� �����͸� ��ȸ�Ͽ�,
    /// ���� ū ����ġ�� ���� Terrain Layer �ε����� ��ȯ�մϴ�.
    /// </summary>
    /// <param name="worldPos">���� ��ǥ</param>
    /// <param name="terrain">üũ�� Terrain</param>
    /// <returns>�ش� ��ġ�� �������� Terrain Layer �ε���</returns>
    int GetTerrainLayerIndex(Vector3 worldPos, Terrain terrain)
    {
        TerrainData terrainData = terrain.terrainData;

        // Terrain�� ���� ��ǥ�� ��ȯ�մϴ�.
        Vector3 terrainLocalPos = worldPos - terrain.transform.position;

        // TerrainData�� ����� �������� 0~1 ������ ����ȭ�� ��ǥ�� ����մϴ�.
        float normalizedX = Mathf.InverseLerp(0, terrainData.size.x, terrainLocalPos.x);
        float normalizedZ = Mathf.InverseLerp(0, terrainData.size.z, terrainLocalPos.z);

        // ���ĸ� �ػ󵵿� �°� ��ǥ�� ��ȯ�մϴ�.
        int mapX = Mathf.RoundToInt(normalizedX * (terrainData.alphamapWidth - 1));
        int mapZ = Mathf.RoundToInt(normalizedZ * (terrainData.alphamapHeight - 1));

        // �ش� ��ǥ�� ���ĸ� �����͸� �����ɴϴ�.
        float[,,] splatmapData = terrainData.GetAlphamaps(mapX, mapZ, 1, 1);

        // ���� ū ����ġ�� ���� ���̾� �ε����� ����մϴ�.
        int dominantLayer = 0;
        float maxWeight = 0f;
        int numLayers = terrainData.alphamapLayers;
        for (int i = 0; i < numLayers; i++)
        {
            float weight = splatmapData[0, 0, i];
            if (weight > maxWeight)
            {
                maxWeight = weight;
                dominantLayer = i;
            }
        }
        return dominantLayer;
    }
}
