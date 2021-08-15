using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainDamager : MonoBehaviour
{
	private TerrainData terrainData;
	private float[,] originalHeightmap;
	private float[,] heightmap;
	private float[,,] splatMaps;
	

	void Start()
	{
		var terrain = GetComponent<Terrain>();

		terrainData = Instantiate<TerrainData>(terrain.terrainData);

		terrain.terrainData = terrainData;

		heightmap = terrainData.GetHeights(0, 0, terrainData.heightmapResolution, terrainData.heightmapResolution);

		originalHeightmap = heightmap.Clone() as float[,];

		splatMaps = terrainData.GetAlphamaps(0, 0, terrainData.alphamapWidth, terrainData.alphamapHeight);

		TerrainCollider terrainCollider = GetComponent<TerrainCollider>();
		terrainCollider.terrainData = terrainData;
	}

	public void ApplyDamage(Vector3 position)
	{
		float severity = 1.0f;
		Vector3 terrainCell = position;

		terrainCell -= transform.position;

		terrainCell.x = (terrainCell.x * terrainData.heightmapResolution) / terrainData.size.x;
		terrainCell.z = (terrainCell.z * terrainData.heightmapResolution) / terrainData.size.z;

		float holeDepth = Random.Range(0.5f, 1.2f);
		float holeRadius = Random.Range(0.5f, 1.5f);

		holeDepth *= severity;
		holeRadius *= severity;
		holeDepth = -holeDepth;

		float baseAdjustment = holeDepth / terrainData.size.y;
		float maxHeightmapAdjustment = 1f / terrainData.size.y;

		int xMin = (int)(terrainCell.x - holeRadius);
		int xMax = (int)(terrainCell.x + holeRadius);
		int zMin = (int)(terrainCell.z - holeRadius);
		int zMax = (int)(terrainCell.z + holeRadius);

		int iHoleRadius = (int)holeRadius;
		if (iHoleRadius < 1) iHoleRadius = 1;

		int dz = -iHoleRadius;
		for (int z = zMin; z <= zMax; z++, dz++)
		{
			int dx = -iHoleRadius;
			for (int x = xMin; x < xMax; x++, dx++)
			{
				if (z >= 0 && z < heightmap.GetLength(0) && x >= 0 && x < heightmap.GetLength(1))
				{
					var heightSample = heightmap[z, x] + baseAdjustment;

					if (heightSample < originalHeightmap[z, x] - maxHeightmapAdjustment)
						heightSample = originalHeightmap[z, x] - maxHeightmapAdjustment;
					if (heightSample > originalHeightmap[z, x] + maxHeightmapAdjustment)
						heightSample = originalHeightmap[z, x] + maxHeightmapAdjustment;

					heightmap[z, x] = heightSample;

					if (z < splatMaps.GetLength(0) && x < splatMaps.GetLength(1))
					{
						for (int k = 0; k < splatMaps.GetLength(2); k++)
							splatMaps[z, x, k] = 0.5f;
					}
				}
			}
		}

		terrainData.SetHeights(0, 0, heightmap);
		terrainData.SetAlphamaps(0, 0, splatMaps);
	}
}