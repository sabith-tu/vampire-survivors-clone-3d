﻿#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MTAssets.SkinnedMeshCombiner
{
    /*
     *  This class is responsible for the functioning of the "Combined Meshes Manager" component, and all its functions.
     */
    /*
     * The Skinned Mesh Combiner was developed by Marcos Tomaz in 2019.
     * Need help? Contact me (mtassets@windsoft.xyz)
     */

    [ExecuteInEditMode]
    [AddComponentMenu("")] //Hide this script in component menu.
    public class CombinedMeshesManager : MonoBehaviour
    {
        //Private constants
        private const int MAX_VERTICES_FOR_16BITS_MESH = 50000; //NOT change this

        //Enums of script
        public enum MergeMethod
        {
            OneMeshPerMaterial,
            AllInOne,
            JustMaterialColors,
            OnlyAnima2dMeshes
        }

        //Variables of this management
        ///<summary>[WARNING] Do not change the value of this variable. This is a variable used for internal tool operations.</summary> 
        [HideInInspector]
        public GameObject rootGameObject;
        ///<summary>[WARNING] Do not change the value of this variable. This is a variable used for internal tool operations.</summary> 
        [HideInInspector]
        public MergeMethod mergeMethodUsed;
        ///<summary>[WARNING] Do not change the value of this variable. This is a variable used for internal tool operations.</summary> 
        [HideInInspector]
        public GameObject resultOfConversionToStaticMesh;
        ///<summary>[WARNING] Do not change the value of this variable. This is a variable used for internal tool operations.</summary> 
        [HideInInspector]
        public SkinnedMeshRenderer[] sourceMeshes;

#if UNITY_EDITOR
        //Public variables of Interface
        private bool gizmosOfThisComponentIsDisabled = false;

        //The UI of this component
        #region INTERFACE_CODE
        [UnityEditor.CustomEditor(typeof(CombinedMeshesManager))]
        public class CustomInspector : UnityEditor.Editor
        {
            public override void OnInspectorGUI()
            {
                //Start the undo event support, draw default inspector and monitor of changes
                CombinedMeshesManager script = (CombinedMeshesManager)target;
                script.gizmosOfThisComponentIsDisabled = MTAssetsEditorUi.DisableGizmosInSceneView("CombinedMeshesManager", script.gizmosOfThisComponentIsDisabled);

                //Description
                GUILayout.Space(10);
                EditorGUILayout.HelpBox("This component manages the combined mesh, generated by the Skinned Mesh Combiner. If you want to undo the merge, go to the Parent Object of this merge.", MessageType.None);
                GUILayout.Space(10);

                //Anima2D management
                #region ANIMA2D_MANAGEMENT_INTERFACE
#if MTAssets_Anima2D_Available
                if (script.mergeMethodUsed == MergeMethod.OnlyAnima2dMeshes)
                {
                    //Settings for "Anima2D"
                    EditorGUILayout.LabelField("Anima2D Merge", EditorStyles.boldLabel);
                    GUILayout.Space(10);

                    //Show atlas of sprite
                    EditorGUILayout.ObjectField(new GUIContent("Atlas Of This Model",
                        "The sprite to render in this model Anima2D."),
                        script.atlasForRenderInChar, typeof(Texture2D), true, GUILayout.Height(16));

                    GUILayout.Space(10);
                }
#endif
                #endregion

                //Management
                EditorGUILayout.LabelField("General Management", EditorStyles.boldLabel);
                GUILayout.Space(10);

                if (GUILayout.Button("Go To Responsible Combiner GameObject", GUILayout.Height(25)))
                {
                    Selection.objects = new Object[] { script.rootGameObject };
                }

                if (GUILayout.Button("Recalculate Bounds Of This Mesh", GUILayout.Height(25)))
                {
                    script.GetComponent<SkinnedMeshRenderer>().sharedMesh.RecalculateBounds();
                }

                if (GUILayout.Button("Recalculate Normals Of This Mesh", GUILayout.Height(25)))
                {
                    script.GetComponent<SkinnedMeshRenderer>().sharedMesh.RecalculateNormals();
                }

                if (GUILayout.Button("Recalculate Tangents Of This Mesh", GUILayout.Height(25)))
                {
                    script.GetComponent<SkinnedMeshRenderer>().sharedMesh.RecalculateTangents();
                }

                if (GUILayout.Button("Optimize This Mesh", GUILayout.Height(25)))
                {
                    script.GetComponent<SkinnedMeshRenderer>().sharedMesh.Optimize();
                }

                if (script.GetComponent<SkinnedMeshRenderer>().sharedMaterials.Length > 1)
                    if (GUILayout.Button("Split This Mesh Into Unique Meshes By Materials", GUILayout.Height(25)))
                        if (EditorUtility.DisplayDialog("Split meshes?", "The division of meshes will cause the Blendshapes of your mesh to stop working on the meshes resulting from the division. This is if your mesh has Blendshapes. Note that the mesh resulting from the merge will remain unchanged. Do you wish to continue?", "Yes", "No") == true)
                            script.SplitCombinedMeshByMaterials();
            }
        }
        #endregion
#endif

        //Anima2D merge data and rederization

        #region ANIMA2D_MERGE_DATA_AND_RENDERIZATION
#if MTAssets_Anima2D_Available 
        //Renderization of combined Anima2D meshes
        [HideInInspector]
        public Texture2D atlasForRenderInChar;
        [HideInInspector]
        public SkinnedMeshRenderer cachedSkinnedRenderer;
        [HideInInspector]
        public MaterialPropertyBlock materialPropertyBlock;

        void OnWillRenderObject()
        {
            //If the merge method not is Only Anima2d Meshes, ignore this render
            if (mergeMethodUsed != MergeMethod.OnlyAnima2dMeshes)
                return;

            if (materialPropertyBlock != null)
            {
                if (atlasForRenderInChar != null)
                {
                    materialPropertyBlock.SetTexture("_MainTex", atlasForRenderInChar);
                }
                cachedSkinnedRenderer.SetPropertyBlock(materialPropertyBlock);
            }
            if (materialPropertyBlock == null)
            {
                materialPropertyBlock = new MaterialPropertyBlock();
            }
        }
#endif
        #endregion

        //Public API

        public void SplitCombinedMeshByMaterials()
        {
            //Show progress bar
#if UNITY_EDITOR
            EditorUtility.DisplayProgressBar("A moment", "Splitting meshes...", 1.0f);
#endif

            //Get this renderer
            SkinnedMeshRenderer thisMeshRenderer = this.gameObject.GetComponent<SkinnedMeshRenderer>();

            //Split each submesh into a new unique submesh into a unique GameObject
            for (int i = 0; i < thisMeshRenderer.sharedMesh.subMeshCount; i++)
            {
                //Create data for this mesh
                CombineInstance[] combine = new CombineInstance[1];
                List<Transform> bones = new List<Transform>();
                List<Matrix4x4> bindPoses = new List<Matrix4x4>();

                //Add bone to list of bones to merge and set bones bindposes
                Transform[] currentMeshBones = thisMeshRenderer.bones;
                for (int x = 0; x < currentMeshBones.Length; x++)
                {
                    bones.Add(currentMeshBones[x]);
                    bool compatibilityMode = rootGameObject.GetComponent<SkinnedMeshCombiner>().compatibilityMode;
                    if (compatibilityMode == true)
                        bindPoses.Add(thisMeshRenderer.sharedMesh.bindposes[x] * thisMeshRenderer.transform.worldToLocalMatrix);
                    if (compatibilityMode == false)
                        bindPoses.Add(currentMeshBones[x].worldToLocalMatrix * thisMeshRenderer.transform.worldToLocalMatrix);
                }

                //Configure the Combine Instance for this submesh
                combine[0].mesh = thisMeshRenderer.sharedMesh;
                combine[0].subMeshIndex = i;
                combine[0].transform = thisMeshRenderer.transform.localToWorldMatrix;

                //Create the new mesh splitted in a unique mesh
                Mesh splitedMesh = new Mesh();
                if (thisMeshRenderer.sharedMesh.vertexCount <= MAX_VERTICES_FOR_16BITS_MESH)
                    splitedMesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt16;
                if (thisMeshRenderer.sharedMesh.vertexCount > MAX_VERTICES_FOR_16BITS_MESH)
                    splitedMesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
                splitedMesh.name = "Combined Mesh Splitted (Material " + i.ToString() + ")";
                splitedMesh.CombineMeshes(combine, true, true);

                //Do recalculations where is desired
                splitedMesh.RecalculateBounds();

                //Create the holder GameObject for this unique new mesh splitted
                GameObject holderGameObject = new GameObject("Combined Mesh Splitted (Material " + i.ToString() + ")");
                holderGameObject.transform.SetParent(this.gameObject.transform);
                SkinnedMeshRenderer render = holderGameObject.AddComponent<SkinnedMeshRenderer>();
                render.sharedMesh = splitedMesh;
                render.bones = bones.ToArray();
                render.sharedMesh.bindposes = bindPoses.ToArray();
                render.rootBone = thisMeshRenderer.rootBone;
                render.sharedMaterials = new Material[] { thisMeshRenderer.sharedMaterials[i] };

                //If save in assets option is enabled, save this splited mesh into asset files
#if UNITY_EDITOR
                SkinnedMeshCombiner rootCombiner = rootGameObject.GetComponent<SkinnedMeshCombiner>();
                if (rootCombiner != null && Application.isPlaying == false)
                    if (rootCombiner.saveDataInAssets == true)
                    {
                        //Create the "Splited" folder
                        if (!AssetDatabase.IsValidFolder("Assets/MT Assets"))
                            AssetDatabase.CreateFolder("Assets", "MT Assets");
                        if (!AssetDatabase.IsValidFolder("Assets/MT Assets/_AssetsData"))
                            AssetDatabase.CreateFolder("Assets/MT Assets", "_AssetsData");
                        if (!AssetDatabase.IsValidFolder("Assets/MT Assets/_AssetsData/Meshes"))
                            AssetDatabase.CreateFolder("Assets/MT Assets/_AssetsData", "Meshes");
                        if (!AssetDatabase.IsValidFolder("Assets/MT Assets/_AssetsData/Meshes/Splited"))
                            AssetDatabase.CreateFolder("Assets/MT Assets/_AssetsData/Meshes", "Splited");

                        //Save the mesh into asset files
                        string meshDir = "Assets/MT Assets/_AssetsData/Meshes/Splited/" + thisMeshRenderer.sharedMesh.name + " Split " + i.ToString() + ".asset";
                        AssetDatabase.CreateAsset(splitedMesh, meshDir);

                        //Add this mesh dir saved, into list of created meshes on root skinned mesh combiner
                        rootCombiner.resultMergeAssetsSaved.Add(meshDir);
                    }
#endif
            }

            //Disable this original render
            thisMeshRenderer.enabled = false;

            //Clear progress bar
#if UNITY_EDITOR
            EditorUtility.ClearProgressBar();
            Debug.Log("The mesh was divided according to the materials. The Skinned Mesh Renderer of the original combined mesh has just been disabled.");
#endif
        }
    }
}