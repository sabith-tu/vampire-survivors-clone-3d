using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;
using SABI.SOA;

public class ScriptableObjectCustomWindow : OdinMenuEditorWindow
{
    public CreateNewIntValueSO createNewIntSO;
    public CreateNewFloatValueSO createNewFloatValueSO;
    public CreateNewBoolValueSO createNewBoolValueSO;
    public CreateNewStringValueSO createNewStringValueSO;

    public CreateNewActionValueSO createNewActionValueSO;
    public CreateNewStateValueSO createNewStateValueSO;


    [MenuItem("SABI/ScriptableObjectCustomWindow")]
    private static void OpenWindow()
    {
        GetWindow<ScriptableObjectCustomWindow>().Show();
    }

    protected override OdinMenuTree BuildMenuTree()
    {
        var tree = new OdinMenuTree();

        createNewIntSO = new CreateNewIntValueSO();
        tree.Add("CREATE BASE SO/INT", createNewIntSO);

        createNewFloatValueSO = new CreateNewFloatValueSO();
        tree.Add("CREATE BASE SO/FLOAT", createNewFloatValueSO);

        createNewBoolValueSO = new CreateNewBoolValueSO();
        tree.Add("CREATE BASE SO/BOOL", createNewBoolValueSO);

        createNewStringValueSO = new CreateNewStringValueSO();
        tree.Add("CREATE BASE SO/STRING", createNewStringValueSO);

        createNewActionValueSO = new CreateNewActionValueSO();
        tree.Add("CREATE EVENT SO/ACTION", createNewActionValueSO);

        createNewStateValueSO = new CreateNewStateValueSO();
        tree.Add("CREATE STATE SO/SIMPLE STATE", createNewStateValueSO);


        tree.AddAllAssetsAtPath("_View/ALL", "Assets/_Project/Data");
        tree.AddAllAssetsAtPath("_View/Base/ Int", "Assets/_Project/Data", typeof(IntValueSO));
        tree.AddAllAssetsAtPath("_View/Base/ Float", "Assets/_Project/Data", typeof(FloatValueSO));
        tree.AddAllAssetsAtPath("_View/Base/ Bool", "Assets/_Project/Data", typeof(BoolValueSO));
        tree.AddAllAssetsAtPath("_View/Base/ String", "Assets/_Project/Data", typeof(StringValueSO));
        tree.AddAllAssetsAtPath("_View/Events/ Action", "Assets/_Project/Data", typeof(ActionSO));
        tree.AddAllAssetsAtPath("_View/SimpleState/ States", "Assets/_Project/Data", typeof(SimpleStateSO));
        return tree;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        if (createNewIntSO != null) DestroyImmediate(createNewIntSO.valueSO);
        if (createNewFloatValueSO != null) DestroyImmediate(createNewFloatValueSO.valueSO);
        if (createNewBoolValueSO != null) DestroyImmediate(createNewBoolValueSO.valueSO);
        if (createNewStringValueSO != null) DestroyImmediate(createNewStringValueSO.valueSO);
        if (createNewActionValueSO != null) DestroyImmediate(createNewActionValueSO.valueSO);
        if (createNewStateValueSO != null) DestroyImmediate(createNewStateValueSO.valueSO);
    }


    public class CreateNewIntValueSO
    {
        [Space(25)] public string IntName;

        [InlineEditor(ObjectFieldMode = InlineEditorObjectFieldModes.Hidden)] [HideInInspector]
        public IntValueSO valueSO;

        public CreateNewIntValueSO()
        {
            valueSO = ScriptableObject.CreateInstance<IntValueSO>();
        }

        [PropertySpace(SpaceBefore = 50)]
        [Button]
        public void CreateNewData()
        {
            AssetDatabase.CreateAsset(valueSO, "Assets/_Project/Data/" + "SO_INT_" + IntName + ".asset");
            AssetDatabase.SaveAssets();

            valueSO = ScriptableObject.CreateInstance<IntValueSO>();
        }
    }

    public class CreateNewFloatValueSO
    {
        [Space(25)] public string FloatName;

        [InlineEditor(ObjectFieldMode = InlineEditorObjectFieldModes.Hidden)] [HideInInspector]
        public FloatValueSO valueSO;

        public CreateNewFloatValueSO()
        {
            valueSO = ScriptableObject.CreateInstance<FloatValueSO>();
        }

        [PropertySpace(SpaceBefore = 50)]
        [Button]
        public void CreateNewData()
        {
            AssetDatabase.CreateAsset(valueSO, "Assets/_Project/Data/" + "SO_FLOAT_" + FloatName + ".asset");
            AssetDatabase.SaveAssets();

            valueSO = ScriptableObject.CreateInstance<FloatValueSO>();
        }
    }

    public class CreateNewBoolValueSO
    {
        [Space(25)] public string BoolName;

        [InlineEditor(ObjectFieldMode = InlineEditorObjectFieldModes.Hidden)] [HideInInspector]
        public BoolValueSO valueSO;

        public CreateNewBoolValueSO()
        {
            valueSO = ScriptableObject.CreateInstance<BoolValueSO>();
        }

        [PropertySpace(SpaceBefore = 50)]
        [Button]
        public void CreateNewData()
        {
            AssetDatabase.CreateAsset(valueSO, "Assets/_Project/Data/" + "SO_BOOL_" + BoolName + ".asset");
            AssetDatabase.SaveAssets();

            valueSO = ScriptableObject.CreateInstance<BoolValueSO>();
        }
    }

    public class CreateNewStringValueSO
    {
        [Space(25)] public string StringName;

        [InlineEditor(ObjectFieldMode = InlineEditorObjectFieldModes.Hidden)] [HideInInspector]
        public StringValueSO valueSO;

        public CreateNewStringValueSO()
        {
            valueSO = ScriptableObject.CreateInstance<StringValueSO>();
        }

        [PropertySpace(SpaceBefore = 50)]
        [Button]
        public void CreateNewData()
        {
            AssetDatabase.CreateAsset(valueSO, "Assets/_Project/Data/" + "SO_STRING_" + StringName + ".asset");
            AssetDatabase.SaveAssets();

            valueSO = ScriptableObject.CreateInstance<StringValueSO>();
        }
    }

    public class CreateNewActionValueSO
    {
        [Space(25)] public string ActionName;

        [InlineEditor(ObjectFieldMode = InlineEditorObjectFieldModes.Hidden)] [HideInInspector]
        public ActionSO valueSO;

        public CreateNewActionValueSO()
        {
            valueSO = ScriptableObject.CreateInstance<ActionSO>();
        }

        [PropertySpace(SpaceBefore = 50)]
        [Button]
        public void CreateNewData()
        {
            AssetDatabase.CreateAsset(valueSO, "Assets/_Project/Data/" + "SO_ACTION_" + ActionName + ".asset");
            AssetDatabase.SaveAssets();

            valueSO = ScriptableObject.CreateInstance<ActionSO>();
        }
    }

    public class CreateNewStateValueSO
    {
        [Space(25)] public string SimpleStateName;

        [InlineEditor(ObjectFieldMode = InlineEditorObjectFieldModes.Hidden)] [HideInInspector]
        public SimpleStateSO valueSO;

        public CreateNewStateValueSO()
        {
            valueSO = ScriptableObject.CreateInstance<SimpleStateSO>();
        }

        [PropertySpace(SpaceBefore = 50)]
        [Button]
        public void CreateNewData()
        {
            AssetDatabase.CreateAsset(valueSO, "Assets/_Project/Data/" + "SO_STATE_" + SimpleStateName + ".asset");
            AssetDatabase.SaveAssets();

            valueSO = ScriptableObject.CreateInstance<SimpleStateSO>();
        }
    }
}