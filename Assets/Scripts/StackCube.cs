using System.Collections;
using System.Collections.Generic;
using EzySlice;
using UnityEngine;

public class StackCube : MonoBehaviour
{
    [SerializeField] private float stackSpeed = 1f;

    private Vector3 targetPosition;
    private Material stackMaterial;

    public void Initialize(Material targetMaterial, Mesh targetMesh)
    {
        if (targetMesh != null)
        {
            GetComponent<MeshFilter>().mesh = targetMesh;
        }

        stackMaterial = targetMaterial;
        GetComponent<MeshRenderer>().materials = new Material[] { stackMaterial };

        var meshCollider = gameObject.AddComponent<MeshCollider>();
        meshCollider.convex = true;

        gameObject.layer = LayerMask.NameToLayer("Stack");
    }

    private void Awake()
    {
        targetPosition = new Vector3(transform.position.x * -1f, transform.position.y, transform.position.z);
    }

    void Update()
    {
        if (!GameController.GameState)
            return;

        if (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, stackSpeed * Time.deltaTime);
        }
        else
        {
            targetPosition = new Vector3(transform.position.x * -1f, transform.position.y, transform.position.z);
        }
    }

    private const float comboSensitivity = 0.1f;
    public static float comboPositionX = 0f;
    public static int comboCount = 0;
    public Mesh CutLeftoverParts(Transform leftCutPosition, Transform rightCutPosition)
    {
        if (gameObject.Slice(leftCutPosition.position, Vector3.right) == null && gameObject.Slice(rightCutPosition.position, Vector3.left) == null && Mathf.Abs(transform.position.x - comboPositionX) > comboSensitivity)
        {
            enabled = false;
            var sliceRigid = gameObject.AddComponent<Rigidbody>();

            comboCount = 0;
            return GetComponent<MeshFilter>().mesh;
        }

        if (transform.position.x >= comboPositionX + comboSensitivity)
        {
            var slices = gameObject.SliceInstantiate(rightCutPosition.position, Vector3.right, stackMaterial);

            var sliceRigid = slices[0].AddComponent<Rigidbody>();

            var sliceMesh = slices[1].GetComponent<MeshFilter>().mesh;
            var sliceMeshCollider = slices[1].AddComponent<MeshCollider>();
            sliceMeshCollider.convex = true;

            slices[1].layer = LayerMask.NameToLayer("Stack");

            leftCutPosition.position += (transform.position.x - comboPositionX) * Vector3.right;
            comboPositionX = transform.position.x;

            gameObject.SetActive(false);

            comboCount = 0;
            return sliceMesh;
        }
        else if (transform.position.x <= comboPositionX - comboSensitivity)
        {
            var slices = gameObject.SliceInstantiate(leftCutPosition.position, Vector3.right, stackMaterial);

            var sliceRigid = slices[1].AddComponent<Rigidbody>();

            var sliceMesh = slices[0].GetComponent<MeshFilter>().mesh;
            var sliceMeshCollider = slices[0].AddComponent<MeshCollider>();
            sliceMeshCollider.convex = true;

            slices[0].layer = LayerMask.NameToLayer("Stack");

            rightCutPosition.position += (transform.position.x - comboPositionX) * Vector3.right;
            comboPositionX = transform.position.x;

            gameObject.SetActive(false);

            comboCount = 0;
            return sliceMesh;
        }
        else
        {
            comboCount++;

            AudioController.Instance.PlayComboSound(comboCount);
            Debug.Log("Combo " + comboCount);

            transform.position = new Vector3(comboPositionX, transform.position.y, transform.position.z);

            enabled = false;
            return GetComponent<MeshFilter>().mesh;
        }
    }
}
