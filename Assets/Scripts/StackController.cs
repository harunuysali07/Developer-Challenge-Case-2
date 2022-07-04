using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class StackController : MonoBehaviour
{
    [SerializeField] private StackCube stackCube;
    [SerializeField] private GameObject finishCube;
    [SerializeField] private List<Material> stackCubeMaterials;
    [SerializeField] private float horizontalSpawnOffset = 3f;
    [SerializeField] private float verticalSpawnOffset = 5f;

    [SerializeField] private Transform leftCutPosition, rightCutPosition;

    private Vector3 nextSpawnPosition = new Vector3(0, -.5f, 0);
    private int stackCount = 0;

    private Vector3 leftCutStartPosition, rightCutStartPosition;

    private void Start()
    {
        leftCutStartPosition = leftCutPosition.position;
        rightCutStartPosition = rightCutPosition.position;

        GameController.Instance.OnLevelStart += OnLevelStart;
    }

    private void OnLevelStart()
    {
        nextSpawnPosition.z += horizontalSpawnOffset;
        stackCount = 0;
        lastMesh = stackCube.GetComponent<MeshFilter>().sharedMesh;
        enabled = true;

        StackCube.comboCount = 0;
        StackCube.comboPositionX = 0;

        leftCutPosition.position = leftCutStartPosition;
        rightCutPosition.position = rightCutStartPosition;

        Instantiate(finishCube, new Vector3(0, -.5f, nextSpawnPosition.z + horizontalSpawnOffset * GameController.Instance.DifficultyData.CurrentLevelLength), Quaternion.identity);
        
        SpawnCube();
    }

    private Mesh lastMesh;
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && GameController.GameState)
        {
            PlayerController.Instance.targetStackPosition = nextSpawnPosition + horizontalSpawnOffset * Vector3.back + Vector3.up * .5f;

            if (lastCube != null)
            {
                lastMesh = lastCube.CutLeftoverParts(leftCutPosition, rightCutPosition);
            }

            if (lastMesh != null && stackCount < GameController.Instance.DifficultyData.CurrentLevelLength)
            {
                SpawnCube();
            }
            else
            {
                PlayerController.Instance.targetStackPosition += horizontalSpawnOffset * Vector3.forward;
                enabled = false;
            }
        }
    }

    private StackCube lastCube;
    private void SpawnCube()
    {
        var verticalOffset = Random.Range(0, 2) == 0 ? -1 : 1;

        lastCube = Instantiate(stackCube, nextSpawnPosition + Vector3.right * verticalSpawnOffset * verticalOffset, Quaternion.identity);
        lastCube.Initialize(stackCubeMaterials[stackCount % stackCubeMaterials.Count], lastMesh);

        stackCount++;
        nextSpawnPosition.z += horizontalSpawnOffset;
    }
}
