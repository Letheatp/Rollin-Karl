using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineLineLimit : CinemachineExtension
{
    [SerializeField] Vector3 _origin;
    [SerializeField] Vector3 _direction;
    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage != CinemachineCore.Stage.Body) { return; }

        var ray = new Ray(_origin, _direction);

        var point = state.RawPosition;

        var t = (point.x - _origin.x) / _direction.x;
        var y = _origin.y + t * _direction.y;

        point = new Vector3(point.x, y, _origin.z);

        state.RawPosition = point;
    }

    #region DrawGizmos

    private const float GizmoLineLength = 1000;

    // 移動範囲をエディタ上で表示(確認用)
    private void OnDrawGizmos()
    {
        if (!isActiveAndEnabled) return;

        var ray = new Ray(_origin, _direction);

        Debug.DrawRay(
            ray.origin - ray.direction * GizmoLineLength / 2,
            ray.direction * GizmoLineLength
        );
    }

    #endregion
}
