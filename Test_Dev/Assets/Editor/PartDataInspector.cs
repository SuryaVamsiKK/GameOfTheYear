using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Part_Data))]
public class PartDataInspector : Editor
{

	Part_Data _PartData;

	public Mesh mPreviewMesh;
	public Material mMat;
	public Material[] mMata;
	public PreviewRenderUtility mPrevRender;
	int selected = 0;

	string[] options = new string[]
	{
		"Left Hand", "Right Hand", "Left Leg", "Right Leg",
	};

	void OnEnable()
	{
		_PartData = (Part_Data)target;
		if (_PartData.Asthetic != null)
		{
			mPreviewMesh = _PartData.Asthetic.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().sharedMesh;
			mMat = _PartData.Asthetic.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Renderer>().sharedMaterial;
			mMata = _PartData.Asthetic.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().sharedMaterials;
		}
	}

	public override void OnInspectorGUI()
	{
		//if (_PartData.Asthetic == null)
		//{
		//}

		EditorGUIUtility.labelWidth = 100;

		if (_PartData.Asthetic != null)
		{
			var boundries = new Rect(5, 50, 200, 200);
			DrawRenderPreview(boundries);
		}

			GUILayout.Space(20);
			GUILayout.BeginHorizontal();
			GUILayout.Space(200);
			_PartData.Part_Name = EditorGUILayout.TextField("Part Name : ", _PartData.Part_Name, GUILayout.ExpandWidth(true));
			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal();
			GUILayout.Space(200);
			_PartData.ID = EditorGUILayout.TextField("Part ID : ", _PartData.ID, GUILayout.ExpandWidth(true));
			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal();
			GUILayout.Space(200);
			_PartData.Asthetic = EditorGUILayout.ObjectField("Asthetic: ", _PartData.Asthetic, typeof(GameObject), true) as GameObject;
			GUILayout.EndHorizontal();

			if (_PartData.PartType == "Left Hand")
			{
				selected = 0;
			}

			if (_PartData.PartType == "Right Hand")
			{
				selected = 1;
			}

			if (_PartData.PartType == "Left Leg")
			{
				selected = 2;
			}

			if (_PartData.PartType == "Right Leg")
			{
				selected = 3;
			}

			GUILayout.BeginHorizontal();
			GUILayout.Space(200);
			selected = EditorGUILayout.Popup("Part Type : ", selected, options, GUILayout.ExpandWidth(true));
			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal();
			GUILayout.Space(200);
			_PartData.Weight = float.Parse(EditorGUILayout.TextField("Weight : ", _PartData.Weight.ToString(), GUILayout.ExpandWidth(true)));
			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal();
			GUILayout.Space(200);
			//GUILayout.Label("Energy : ");
			_PartData.Enegry = float.Parse(EditorGUILayout.TextField("Energy : ", _PartData.Enegry.ToString(), GUILayout.ExpandWidth(true)));
			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal();
			GUILayout.Space(200);
			_PartData.Damage = float.Parse(EditorGUILayout.TextField("Damage : ", _PartData.Damage.ToString(), GUILayout.ExpandWidth(true)));
			GUILayout.EndHorizontal();

			if (selected == 0)
			{
				_PartData.PartType = "Left Hand";
			}

			if (selected == 1)
			{
				_PartData.PartType = "Right Hand";
			}

			if (selected == 2)
			{
				_PartData.PartType = "Left Leg";
			}

			if (selected == 3)
			{
				_PartData.PartType = "Right Leg";
			}

		GUILayout.Space(20);
		GUILayout.BeginHorizontal();
		GUILayout.Space(200);
		if (GUILayout.Button("Save Changes"))
		{
			AssetDatabase.Refresh();
			EditorUtility.SetDirty(_PartData);
			AssetDatabase.SaveAssets();
		}
		GUILayout.EndHorizontal();
		GUILayout.Space(200);

		
		//base.OnInspectorGUI();
	}

	public void DrawRenderPreview(Rect r)
	{

		if (_PartData.Asthetic != null)
		{

			Debug.Log(mPreviewMesh.subMeshCount);

			if (mPrevRender == null)
				mPrevRender = new PreviewRenderUtility();

			if (mPreviewMesh == null)
			{
				mPreviewMesh = Resources.Load("SquareTest", typeof(Mesh)) as Mesh;
				mMat = Resources.Load("Materials/Box01Mat", typeof(Material)) as Material;
			}

			if (_PartData.PartType == "Left Hand" || _PartData.PartType == "Right Hand")
			{
				mPrevRender.camera.transform.position = new Vector3(_PartData.Asthetic.transform.GetChild(_PartData.Asthetic.transform.childCount - 1).position.x, _PartData.Asthetic.transform.GetChild(_PartData.Asthetic.transform.childCount - 1).position.y + 20.0f, _PartData.Asthetic.transform.GetChild(_PartData.Asthetic.transform.childCount - 1).position.z);
				mPrevRender.camera.transform.rotation = Quaternion.Euler(90.0f, 180.0f, 0.0f);
			}

			if (_PartData.PartType == "Right Leg")
			{
				mPrevRender.camera.transform.position = new Vector3(_PartData.Asthetic.transform.GetChild(_PartData.Asthetic.transform.childCount - 1).position.x + 20.0f, _PartData.Asthetic.transform.GetChild(_PartData.Asthetic.transform.childCount - 1).position.y, _PartData.Asthetic.transform.GetChild(_PartData.Asthetic.transform.childCount - 1).position.z);
				mPrevRender.camera.transform.rotation = Quaternion.Euler(0.0f, -90.0f, 0.0f);
			}

			if (_PartData.PartType == "Left Leg")
			{
				mPrevRender.camera.transform.position = new Vector3(_PartData.Asthetic.transform.GetChild(_PartData.Asthetic.transform.childCount - 1).position.x - 20.0f, _PartData.Asthetic.transform.GetChild(_PartData.Asthetic.transform.childCount - 1).position.y, _PartData.Asthetic.transform.GetChild(_PartData.Asthetic.transform.childCount - 1).position.z + 1.0f);
				mPrevRender.camera.transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
			}
			//mPrevRender.camera.transform.RotateAround(_PartData.Asthetic.transform.GetChild(_PartData.Asthetic.transform.childCount - 1).position, Vector3.up, 80.0f * Time.deltaTime);
			mPrevRender.camera.farClipPlane = 1000;

			mPrevRender.lights[0].intensity = 0.75f;
			mPrevRender.lights[0].transform.rotation = Quaternion.Euler(90f, 140.0f, 0f);
			mPrevRender.lights[1].intensity = 1f;

			mPrevRender.BeginPreview(r, GUIStyle.none);

			for (int i = 0; i < mPreviewMesh.subMeshCount; i++)
			{
				mPrevRender.DrawMesh(mPreviewMesh, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity, mMata[i], i);
			}
			//mPrevRender.DrawMesh(mPreviewMesh, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity, mMat, 1);
			bool fog = RenderSettings.fog;
			Unsupported.SetRenderSettingsUseFogNoDirty(false);
			mPrevRender.camera.Render();
			Unsupported.SetRenderSettingsUseFogNoDirty(fog);
			Texture texture = mPrevRender.EndPreview();
			//GUI.DrawTexture(r, texture);
			EditorGUI.DrawTextureTransparent(r, texture);
		}
	}
}
