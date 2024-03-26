using System.Linq;
using UnityEditor;
using UnityEngine;
using MyBox;

[RequireComponent(typeof(LineRenderer))]
public class LineRendererSmoother : MonoBehaviour
{
    public LineRenderer Line;
    public Vector3[] InitialState = new Vector3[1];
    public float SmoothingLength = 2f;
    public int SmoothingSections = 10;
    public float SimplifyValue = 0.1f;


    [ButtonMethod(ButtonMethodDrawOrder.BeforeInspector)]
    private void DoUpdateInitialState()
    {
        InitialState = new Vector3[Line.positionCount];
        Line.GetPositions(InitialState);
    }

    [ButtonMethod(ButtonMethodDrawOrder.BeforeInspector)]
    private void DoResetToInitialState()
    {
        Line.positionCount = InitialState.Length;
        Line.SetPositions(InitialState);
    }


    [ButtonMethod(ButtonMethodDrawOrder.BeforeInspector)]
    private void DoSmoothLine()
    {
        Smooth();
    }

    [ButtonMethod(ButtonMethodDrawOrder.BeforeInspector)]
    private void DoGenerate2DCollider()
    {
        Generate2DLineCollider();
    }

    [ButtonMethod(ButtonMethodDrawOrder.BeforeInspector)]
    private void DoGenerateMeshCollider()
    {
        GenerateMeshCollider();
    }

    [ButtonMethod(ButtonMethodDrawOrder.BeforeInspector)]
    private void DoSimplify()
    {
        Line.Simplify(SimplifyValue);
    }

    public void Generate2DLineCollider()
    {
        PolygonCollider2D collider = GetComponent<PolygonCollider2D>();

        if (collider == null)
        {
            collider = gameObject.AddComponent<PolygonCollider2D>();
        }

        float halfWidth = Line.startWidth / 2f;

        Vector3[] position3 = new Vector3[Line.positionCount];
        Line.GetPositions(position3);
        Vector2[] position2 = new Vector2[Line.positionCount];
        for (int i = 0; i < Line.positionCount; i++)
        {
            position2[i] = new Vector2(position3[i].x + halfWidth, position3[i].y - halfWidth);
        }

        collider.SetPath(0, position2);
    }

    public void GenerateMeshCollider()
    {
        MeshCollider collider = GetComponent<MeshCollider>();

        if (collider == null)
        {
            collider = gameObject.AddComponent<MeshCollider>();
        }


        Mesh mesh = new Mesh();
        Line.BakeMesh(mesh, true);

        mesh.SetIndices(mesh.GetIndices(0).Concat(mesh.GetIndices(0).Reverse()).ToArray(), MeshTopology.Triangles, 0);

      /*  // if you need collisions on both sides of the line, simply duplicate & flip facing the other direction!
        // This can be optimized to improve performance ;)
        int[] meshIndices = mesh.GetIndices(0);
        int[] newIndices = new int[meshIndices.Length * 2];

        int j = meshIndices.Length - 1;
        for (int i = 0; i < meshIndices.Length; i++)
        {
            newIndices[i] = meshIndices[i];
            newIndices[meshIndices.Length + i] = meshIndices[j];
        }
        mesh.SetIndices(newIndices, MeshTopology.Triangles, 0);*/

        collider.sharedMesh = mesh;
    }

    public void Smooth()
    {
        BezierCurve[] curves = new BezierCurve[Line.positionCount - 1];
        for (int i = 0; i < curves.Length; i++)
        {
            curves[i] = new BezierCurve();
        }

        for (int i = 0; i < curves.Length; i++)
        {
            Vector3 position = Line.GetPosition(i);
            Vector3 lastPosition = i == 0 ? Line.GetPosition(0) : Line.GetPosition(i - 1);
            Vector3 nextPosition = Line.GetPosition(i + 1);

            Vector3 lastDirection = (position - lastPosition).normalized;
            Vector3 nextDirection = (nextPosition - position).normalized;

            Vector3 startTangent = (lastDirection + nextDirection) * SmoothingLength;
            Vector3 endTangent = (nextDirection + lastDirection) * -1 * SmoothingLength;


            curves[i].Points[0] = position; // Start Position (P0)
            curves[i].Points[1] = position + startTangent; // Start Tangent (P1)
            curves[i].Points[2] = nextPosition + endTangent; // End Tangent (P2)
            curves[i].Points[3] = nextPosition; // End Position (P3)
        }

        // Apply look-ahead for first curve and retroactively apply the end tangent
        {
            Vector3 nextDirection = (curves[1].EndPosition - curves[1].StartPosition).normalized;
            Vector3 lastDirection = (curves[0].EndPosition - curves[0].StartPosition).normalized;

            curves[0].Points[2] = curves[0].Points[3] +
                (nextDirection + lastDirection) * -1 * SmoothingLength;
        }

        Line.positionCount = curves.Length * SmoothingSections;
        int index = 0;
        for (int i = 0; i < curves.Length; i++)
        {
            Vector3[] segments = curves[i].GetSegments(SmoothingSections);
            for (int j = 0; j < segments.Length; j++)
            {
                Line.SetPosition(index, segments[j]);
                index++;
            }
        }
    }
}