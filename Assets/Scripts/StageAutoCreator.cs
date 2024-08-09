using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class StageAutoCreator : MonoBehaviour
{
    private const int StageDeltaX = 21;
    private const int StageDeltaY = 21;

    private const int CreateThreshold = 50;

    private List<AbstractStage> stages = new();

    [SerializeField] private int currentStageIndex = 2;
    [SerializeField] private GameObject _target;
    [SerializeField] private AssetReference _reference;
    // Start is called before the first frame update
    void Start()
    {
        this.UpdateAsObservable()
            .Where(_ => _target.transform.position.y < -StageDeltaY * currentStageIndex + CreateThreshold)
            .Subscribe(_ => CreateStage());
    }

    private void CreateStage()
    {
        var handle = Addressables.InstantiateAsync(_reference, this.transform);

        handle.Completed += op =>
        {
            if (op.Status == AsyncOperationStatus.Succeeded)
            {
                var instance = op.Result;

                Vector2 position = new Vector3(-StageDeltaX * (currentStageIndex + 1), -StageDeltaY * (currentStageIndex + 1), 0);
                instance.transform.position = position;

                currentStageIndex += 1;
                stages.Add(instance.GetComponent<AbstractStage>());
            }
            else
            {
                Debug.Log("Stageの生成に失敗しました");
            }
        };
    }
}
