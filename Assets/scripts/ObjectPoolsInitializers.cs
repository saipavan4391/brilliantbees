using UnityEngine;
using System.Collections;
public class ObjectPoolsInitializers : MonoBehaviour
{
	public GameObject[] Obstacles;
	public int obstaclePoolSize = 1;
	public static ObjectPoolsInitializers Instance;
	void Awake()
	{
		//InitializeObjectPools();
		Instance = this;
		InitializeObjectPools();
	}
	private void InitializeObjectPools()

	{
		ObjectPoolManager.Instance.CreatePool(PoolType.Obstacle, GetPool(Obstacles[0], obstaclePoolSize));
		ObjectPoolManager.Instance.CreatePool(PoolType.Obstacle1, GetPool(Obstacles[1], obstaclePoolSize));
		ObjectPoolManager.Instance.CreatePool(PoolType.Obstacle2, GetPool(Obstacles[2], obstaclePoolSize));
		ObjectPoolManager.Instance.CreatePool(PoolType.Obstacle3, GetPool(Obstacles[3], obstaclePoolSize));
		ObjectPoolManager.Instance.CreatePool(PoolType.Obstacle1, GetPool(Obstacles[4], obstaclePoolSize));
		ObjectPoolManager.Instance.CreatePool(PoolType.Obstacle5, GetPool(Obstacles[5], obstaclePoolSize));
		ObjectPoolManager.Instance.CreatePool(PoolType.Obstacle6, GetPool(Obstacles[6], obstaclePoolSize));
		ObjectPoolManager.Instance.CreatePool(PoolType.ObstaclewithHoney, GetPool(Obstacles[7], obstaclePoolSize));
		ObjectPoolManager.Instance.CreatePool(PoolType.ObstaclewithHoney1, GetPool(Obstacles[8], obstaclePoolSize));
		ObjectPoolManager.Instance.CreatePool(PoolType.ObstaclewithHoney2, GetPool(Obstacles[9], obstaclePoolSize));





	}
	public ObjectPool GetPool(GameObject poolGameObject, int poolSize)
	{
		ObjectPool pool = new ObjectPool(poolGameObject, poolSize, (go) =>
			{
			});
		return pool;
	}
}
public enum PoolType
{
	Obstacle,
	Obstacle1,
	Obstacle2,
	Obstacle3,
	Obstacle4,
	Obstacle5,
	Obstacle6,
	//with honey
	ObstaclewithHoney,
	ObstaclewithHoney1,
	ObstaclewithHoney2



}
