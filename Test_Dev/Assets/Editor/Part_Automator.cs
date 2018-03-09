using UnityEditor;
using UnityEngine;

public class Part_Automator : EditorWindow {

	

	[MenuItem("Window/Part Automator")]
	public static void ShowWindow()
	{
		GetWindow<Part_Automator>("Part Automator");
	}

	private Mesh mPreviewMesh;
	private Material mMat;
	private PreviewRenderUtility mPrevRender;

	private void OnGUI()
	{
		

		GUILayout.Label("Automate the selected objects", EditorStyles.boldLabel);


		if (GUILayout.Button("Automate"))
		{
			foreach (GameObject obj in Selection.gameObjects)
			{
				//obj.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

				if (obj.GetComponent<Animator>() != null)
				{
					obj.GetComponent<Animator>().runtimeAnimatorController = AssetDatabase.LoadAssetAtPath("Assets/AnimationsV2/CharacterV2.controller", typeof(RuntimeAnimatorController)) as RuntimeAnimatorController;
				}

				if (obj.GetComponent<Parts_Animations>() == null)
				{
					obj.AddComponent<Parts_Animations>();
				}
			}
		}
		/*
		foreach (GameObject obj in Selection.gameObjects)
		{
			var boundries = new Rect(0, 50, this.position.width, this.position.width);
			Debug.Log(mPreviewMesh);
			DrawRenderPreview(boundries); 
			//mPreviewMesh = obj.GetComponent<MeshFilter>().sharedMesh;
			mPreviewMesh = obj.GetComponent<SkinnedMeshRenderer>().sharedMesh;
			mMat = obj.GetComponent<Renderer>().material;
		}
		*/
	}

	public void DrawRenderPreview(Rect r)
	{
		if (mPrevRender == null)
			mPrevRender = new PreviewRenderUtility();

		if (mPreviewMesh == null)
		{
			mPreviewMesh = Resources.Load("SquareTest", typeof(Mesh)) as Mesh;
			mMat = Resources.Load("Materials/Box01Mat", typeof(Material)) as Material;
		}

		mPrevRender.camera.transform.position = new Vector3(7.0f, 14.36f, 20f);
		Debug.Log(mPrevRender.camera.transform.position);
		mPrevRender.camera.transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
		mPrevRender.camera.farClipPlane = 1000;

		mPrevRender.lights[0].intensity = -1f;
		mPrevRender.lights[0].transform.rotation = Quaternion.Euler(30f, 140.0f, 0f);
		mPrevRender.lights[1].intensity = 1f;

		mPrevRender.BeginPreview(r, GUIStyle.none); 
		mPrevRender.DrawMesh(mPreviewMesh, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity, mMat, 0);
		bool fog = RenderSettings.fog;
		Unsupported.SetRenderSettingsUseFogNoDirty(false);
		mPrevRender.camera.Render();
		Unsupported.SetRenderSettingsUseFogNoDirty(fog);
		Texture texture = mPrevRender.EndPreview();

		GUI.DrawTexture(r, texture);
	}
}
