using UnityEngine;

public struct DimensionSwitched { public DimensionType NewDimension; }
public enum DimensionType
{
    [InspectorName("3D")] ThreeD,
    [InspectorName("2D")] TwoD
}
