using UnityEngine;

public class MultiTerrainChecker : MonoBehaviour
{

    public int layerIndex;
    void Update()
    {
        // 객체가 위치한 곳 아래에 존재하는 Terrain을 가져옵니다.
        Terrain terrainUnderneath = GetTerrainUnderneath(transform.position);
        if (terrainUnderneath != null)
        {
            layerIndex = GetTerrainLayerIndex(transform.position, terrainUnderneath);
            //Debug.Log($"객체가 밟고 있는 Terrain: {terrainUnderneath.name} / 지배적인 Layer 인덱스: {layerIndex}");
        }
        else
        {
            Debug.Log("객체 아래에 Terrain이 없습니다.");
        }
    }

    /// <summary>
    /// 주어진 월드 좌표가 포함되는 Terrain을 반환합니다.
    /// 여러 Terrain이 있을 경우, 좌표가 포함되는 첫 번째 Terrain을 반환합니다.
    /// </summary>
    /// <param name="worldPos">월드 좌표</param>
    /// <returns>해당 좌표를 포함하는 Terrain, 없으면 null</returns>
    Terrain GetTerrainUnderneath(Vector3 worldPos)
    {
        // 씬 내의 모든 활성화된 Terrain들을 가져옵니다.
        Terrain[] terrains = Terrain.activeTerrains;
        foreach (Terrain terrain in terrains)
        {
            Vector3 terrainPos = terrain.transform.position;
            Vector3 terrainSize = terrain.terrainData.size;

            // 월드 좌표가 Terrain의 영역 내에 있는지 체크합니다.
            if (worldPos.x >= terrainPos.x && worldPos.x <= terrainPos.x + terrainSize.x &&
                worldPos.z >= terrainPos.z && worldPos.z <= terrainPos.z + terrainSize.z)
            {
                return terrain;
            }
        }
        return null;
    }

    /// <summary>
    /// 지정된 Terrain에서 worldPos 위치의 알파맵 데이터를 조회하여,
    /// 가장 큰 가중치를 가진 Terrain Layer 인덱스를 반환합니다.
    /// </summary>
    /// <param name="worldPos">월드 좌표</param>
    /// <param name="terrain">체크할 Terrain</param>
    /// <returns>해당 위치의 지배적인 Terrain Layer 인덱스</returns>
    int GetTerrainLayerIndex(Vector3 worldPos, Terrain terrain)
    {
        TerrainData terrainData = terrain.terrainData;

        // Terrain의 로컬 좌표로 변환합니다.
        Vector3 terrainLocalPos = worldPos - terrain.transform.position;

        // TerrainData의 사이즈를 기준으로 0~1 범위의 정규화된 좌표를 계산합니다.
        float normalizedX = Mathf.InverseLerp(0, terrainData.size.x, terrainLocalPos.x);
        float normalizedZ = Mathf.InverseLerp(0, terrainData.size.z, terrainLocalPos.z);

        // 알파맵 해상도에 맞게 좌표를 변환합니다.
        int mapX = Mathf.RoundToInt(normalizedX * (terrainData.alphamapWidth - 1));
        int mapZ = Mathf.RoundToInt(normalizedZ * (terrainData.alphamapHeight - 1));

        // 해당 좌표의 알파맵 데이터를 가져옵니다.
        float[,,] splatmapData = terrainData.GetAlphamaps(mapX, mapZ, 1, 1);

        // 가장 큰 가중치를 가진 레이어 인덱스를 계산합니다.
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
