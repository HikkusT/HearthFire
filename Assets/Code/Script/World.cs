using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    [SerializeField] public Chunk terrain;
    [SerializeField] EnvironmentManager _envirommentManager;
    [SerializeField] GameObject player;
    [SerializeField] GameObject campfire;
    
    public GameObject torch;
    public GameObject canvas;
    public GameObject fireplaceText;
    public GameObject fireplaceTextBackimage;
    
    GameObject fireplace;

    // Start is called before the first frame update
    void Start()
    {
        _envirommentManager.PopulateTrees(terrain);
        PathManager.Instance.terrain = terrain;
        SetupPlayer();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SetupPlayer()
    {
        PreparePlayerSpawn();
        terrain.chunk[terrain.size / 2][terrain.size / 2].SnapTo(player);
        terrain.chunk[terrain.size / 2 + 1][terrain.size / 2].SpawnAt(campfire);
        player.GetComponent<CharacterController>().currentVoxel = terrain.GetVoxelAt(terrain.size / 2, terrain.size / 2);
    }

    void PreparePlayerSpawn()
    {
        terrain.chunk[terrain.size / 2][terrain.size / 2].DestroyScenarioProp();
        terrain.chunk[terrain.size / 2 + 1][terrain.size / 2].DestroyScenarioProp();
    }
}
