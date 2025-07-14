/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using Sirenix.OdinInspector;

public class FieldOfView : MonoBehaviour {

    [SerializeField] private LayerMask layerMask;
    private Mesh mesh;
    public float fov;
    public float viewDistance;
    public Vector3 origin;
    public float startingAngle;
    private Action<Collider2D> onHitObject;
    [SerializeField] float innerOffset = 0.3f;
    public void SetOnHitObject(Action<Collider2D> onHitObjectChange) {
        this.onHitObject = onHitObjectChange;
    }
    
    [Button]
    private void Start() {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        //fov = 90f;
        //viewDistance = 50f;
        origin = Vector3.zero;
    }
    [Button]
    private void LateUpdate()
    {
        int rayCount = 100;
        float angle = startingAngle;
        float angleIncrease = fov / rayCount;

       // Khoảng cách từ origin đến đường đáy phía trước

        Vector3[] vertices = new Vector3[(rayCount + 1) * 2]; // Mỗi ray có 2 đỉnh: gần và xa
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 6]; // Mỗi ô vuông cần 2 tam giác = 6 điểm

        Vector3 centerDir = UtilsClass.GetVectorFromAngle(startingAngle - fov / 2f + fov / 2f); // hướng chính giữa
        Vector3 baseCenter = origin + centerDir * innerOffset;

        for (int i = 0; i <= rayCount; i++)
        {
            float currentAngle = angle - i * angleIncrease;
            Vector3 dir = UtilsClass.GetVectorFromAngle(currentAngle);

            // Điểm gần (trên đoạn đáy trước)
            Vector3 nearPoint = baseCenter + dir * (innerOffset);
            // Điểm xa (từ điểm gần raycast ra)
            Vector3 farPoint;
            RaycastHit2D raycastHit = Physics2D.Raycast(nearPoint, dir, viewDistance, layerMask);
            if (raycastHit.collider == null)
            {
                farPoint = nearPoint + dir * viewDistance;
            }
            else
            {
                farPoint = raycastHit.point;
                onHitObject?.Invoke(raycastHit.collider);
            }

            vertices[i * 2] = nearPoint;
            vertices[i * 2 + 1] = farPoint;
        }

        int triangleIndex = 0;
        for (int i = 0; i < rayCount; i++)
        {
            int index = i * 2;

            // Tam giác 1
            triangles[triangleIndex++] = index;
            triangles[triangleIndex++] = index + 1;
            triangles[triangleIndex++] = index + 3;

            // Tam giác 2
            triangles[triangleIndex++] = index;
            triangles[triangleIndex++] = index + 3;
            triangles[triangleIndex++] = index + 2;
        }

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.RecalculateBounds();
    }

    public void SetOrigin(Vector3 origin) {
        this.origin = origin;
    }

    public void SetAimDirection(Vector3 aimDirection) {
        startingAngle = UtilsClass.GetAngleFromVectorFloat(aimDirection) + fov / 2f;
    }

    public void SetFoV(float fov) {
        this.fov = fov;
    }

    public void SetViewDistance(float viewDistance) {
        this.viewDistance = viewDistance;
    }

}
