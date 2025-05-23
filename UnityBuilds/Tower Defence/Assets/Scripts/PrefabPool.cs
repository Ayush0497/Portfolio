using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PrefabPool : MonoBehaviour
{
    internal int totalEnemies;
    private void Awake()
    {
        InitializeProjectiles();
        InitializeEnemyShips();
        InitializePlayerShips();
        InitializeBlooms();
    }

    #region Projectiles
    public int numPlayerProjectilesInScene;
    public Transform projectilePrefab;
    protected Transform[] projectilePool = new Transform[0];
    public void InitializeProjectiles()
    {
        if (projectilePool!=null && projectilePool.Length == 0)
        {
            projectilePool = new Transform[numPlayerProjectilesInScene];
            for (int c = 0; c < numPlayerProjectilesInScene; c++)
            {
                projectilePool[c] = Instantiate(projectilePrefab);
                projectilePool[c].gameObject.SetActive(false);
            }
        }
    }

    public Transform Projectile
    {
        get
        {
            Transform returnTransform = null;
            int c = 0;
            if(projectilePool!=null)
            {
                while (c < projectilePool.Length && returnTransform == null)
                {
                    if (!projectilePool[c].gameObject.activeInHierarchy)
                    {
                        returnTransform = projectilePool[c];
                        projectilePool[c].gameObject.SetActive(true);
                    }
                    c++;
                }
            }
            return returnTransform;
        }
    }
    #endregion

    #region Ships
    public int numEnemyShipsInScene;
    public Transform enemyShipPrefab;
    protected Transform[] enemyShipPool = new Transform[0];
    public void InitializeEnemyShips()
    {   
        if (enemyShipPool != null && enemyShipPool.Length == 0)
        {
            enemyShipPool = new Transform[numEnemyShipsInScene];
            for (int c = 0; c < numEnemyShipsInScene; c++)
            {
                enemyShipPool[c] = Instantiate(enemyShipPrefab);
                enemyShipPool[c].gameObject.SetActive(false);
            }
        }
    }

    public Transform EnemyShip
    {
        get
        {
            Transform returnTransform = null;
            int c = 0;
            if(enemyShipPool != null)
            {
                while (c < enemyShipPool.Length && returnTransform == null)
                {
                    if (!enemyShipPool[c].gameObject.activeInHierarchy)
                    {
                        returnTransform = enemyShipPool[c];
                        enemyShipPool[c].gameObject.SetActive(true);
                    }
                    c++;
                }
            }
            return returnTransform;
        }
    }
    #endregion

    #region PlayerShip
    public int maxPlayerShips;
    public Transform playerShip;
    protected Transform[] playerShipPool = new Transform[0];
    private Ship target;
    public void InitializePlayerShips()
    {
        if (playerShipPool != null && playerShipPool.Length == 0)
        {
            playerShipPool = new Transform[maxPlayerShips];
            for (int c = 0; c < maxPlayerShips; c++)
            {
                playerShipPool[c] = Instantiate(playerShip);
                playerShipPool[c].gameObject.SetActive(false);
            }
        }
    }

    public Transform PlayerShip
    {
        get
        {
            Transform returnTransform = null;
            int c = 0;
            if (playerShipPool != null)
            {
                while (c < playerShipPool.Length && returnTransform == null)
                {
                    if (!playerShipPool[c].gameObject.activeInHierarchy)
                    {
                        returnTransform = playerShipPool[c];
                        playerShipPool[c].gameObject.SetActive(true);
                    }
                    c++;
                }
            }
            return returnTransform;
        }
    }

    public Ship FindEnemyShip()
    {
        if (target == null || !target.gameObject.activeInHierarchy)
        {
            GameObject[] enemyShips = GameObject.FindGameObjectsWithTag("Enemy");
            int randomShip = Random.Range(0, enemyShips.Length);
            if (enemyShips != null && enemyShips.Length > 0)
            {
                target = enemyShips[randomShip].GetComponent<Ship>();
            }
        }
        return target;
    }
    #endregion

    #region Bloom
    public int maxBloom;
    public Transform bloom;
    protected Transform[] bloomsPool = new Transform[0];
    public void InitializeBlooms()
    {
        if (bloomsPool != null && bloomsPool.Length == 0)
        {
            bloomsPool = new Transform[maxBloom];
            for (int c = 0; c < maxBloom; c++)
            {
                bloomsPool[c] = Instantiate(bloom);
                bloomsPool[c].gameObject.SetActive(false);
            }
        }
    }

    public Transform Bloom
    {
        get
        {
            Transform returnTransform = null;
            int c = 0;
            if (bloomsPool != null)
            {
                while (c < bloomsPool.Length && returnTransform == null)
                {
                    if (!bloomsPool[c].gameObject.activeInHierarchy)
                    {
                        returnTransform = bloomsPool[c];
                        bloomsPool[c].gameObject.SetActive(true);
                    }
                    c++;
                }
            }
            return returnTransform;
        }
    }
    #endregion
}
