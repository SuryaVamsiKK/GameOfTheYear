using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PartsDatabaseHolder))]
public class CustomDatabase : Editor {

	PartsDatabaseHolder PDHolder;
	Part_Data obj;
	string path;

	void OnEnable()
	{
		PDHolder = (PartsDatabaseHolder)target;
	}

	public override void OnInspectorGUI()
	{
		GUILayout.Label("Total Parts : " + PDHolder.Part.Count); 
		GUILayout.Space(20);

		#region Entry Details Display
		GUILayout.BeginHorizontal();
		{
			if (obj != null)
			{
				obj.Part_Name = EditorGUILayout.TextField(obj.Part_Name, GUILayout.Width(100));
			}

			obj = (Part_Data)EditorGUILayout.ObjectField("", obj, typeof(Part_Data), true);
			path = AssetDatabase.GetAssetPath(obj);

			if (obj != null)
			{
				GUILayout.Label(obj.PartType);
			}
		}

		GUILayout.EndHorizontal();

		if (obj != null)
		{
			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			GUILayout.Label(path);
			GUILayout.FlexibleSpace();
			GUILayout.Label("Part ID : " + obj.ID);
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
		}

		GUILayout.Space(7);
		#endregion

		#region Data Entry Buttons
		GUILayout.BeginHorizontal();
		if (GUILayout.Button("Add Part"))
		{
			AddPart(obj);
			AssetDatabase.Refresh();
			EditorUtility.SetDirty(PDHolder);
			AssetDatabase.SaveAssets();
		}
		if (GUILayout.Button("Change"))
		{
			obj = null;
			path = null;
			AssetDatabase.Refresh();
			EditorUtility.SetDirty(PDHolder);
			AssetDatabase.SaveAssets();
		}
		GUILayout.EndHorizontal();
		GUILayout.Space(20);
		#endregion

		#region base GUI
		/*
		GUILayout.Space(20);
		base.OnInspectorGUI();
		GUILayout.Space(20);
		*/
		#endregion
		
		#region All the 4 different tabs

		//The Tab Buttons
		{
			PDHolder.currentTab = GUILayout.Toolbar(PDHolder.currentTab, new string[] { "Left Hand", "Right Hand", "Left Leg", "Right Leg" });
			GUILayout.Space(20);
		}

		//Left Hand
		if (PDHolder.currentTab == 0)
		{
			for (int cnt = 0; cnt < PDHolder.Part.Count; cnt++)
			{
				if (PDHolder.Part[cnt].PartType == "Left Hand")
				{
					GUILayout.BeginHorizontal();
					PDHolder.Part[cnt].PartData.Part_Name = EditorGUILayout.TextField(PDHolder.Part[cnt].PartData.Part_Name, GUILayout.Width(100));
					EditorGUILayout.ObjectField("", PDHolder.Part[cnt].PartData, typeof(Part_Data), true);
					GUILayout.Label(PDHolder.Part[cnt].PartType);
					if (GUILayout.Button("X"))
					{
						RemovePart(cnt);
						return;
					}
					GUILayout.EndHorizontal();
					GUILayout.BeginHorizontal();
					GUILayout.FlexibleSpace();
					GUILayout.Label(PDHolder.Part[cnt].Path);
					GUILayout.FlexibleSpace();
					GUILayout.Label("Part ID : " + PDHolder.Part[cnt].PartID);
					GUILayout.FlexibleSpace();
					GUILayout.EndHorizontal();
				}
			}

		}
		//Right Hand
		if (PDHolder.currentTab == 1)
		{
			for (int cnt = 0; cnt < PDHolder.Part.Count; cnt++)
			{
				if (PDHolder.Part[cnt].PartType == "Right Hand")
				{
					GUILayout.BeginHorizontal();
					PDHolder.Part[cnt].PartData.Part_Name = EditorGUILayout.TextField(PDHolder.Part[cnt].PartData.Part_Name, GUILayout.Width(100));
					EditorGUILayout.ObjectField("", PDHolder.Part[cnt].PartData, typeof(Part_Data), true);
					GUILayout.Label(PDHolder.Part[cnt].PartType);
					if (GUILayout.Button("X"))
					{
						RemovePart(cnt);
						return;
					}
					GUILayout.EndHorizontal();
					GUILayout.BeginHorizontal();
					GUILayout.FlexibleSpace();
					GUILayout.Label(PDHolder.Part[cnt].Path);
					GUILayout.FlexibleSpace();
					GUILayout.Label("Part ID : " + PDHolder.Part[cnt].PartID);
					GUILayout.FlexibleSpace();
					GUILayout.EndHorizontal();
				}
			}

		}

		//left Leg
		if (PDHolder.currentTab == 2)
		{
			for (int cnt = 0; cnt < PDHolder.Part.Count; cnt++)
			{
				if (PDHolder.Part[cnt].PartType == "Left Leg")
				{
					GUILayout.BeginHorizontal();
					PDHolder.Part[cnt].PartData.Part_Name = EditorGUILayout.TextField(PDHolder.Part[cnt].PartData.Part_Name, GUILayout.Width(100));
					EditorGUILayout.ObjectField("", PDHolder.Part[cnt].PartData, typeof(Part_Data), true);
					GUILayout.Label(PDHolder.Part[cnt].PartType);
					if (GUILayout.Button("X"))
					{
						RemovePart(cnt);
						return;
					}
					GUILayout.EndHorizontal();
					GUILayout.BeginHorizontal();
					GUILayout.FlexibleSpace();
					GUILayout.Label(PDHolder.Part[cnt].Path);
					GUILayout.FlexibleSpace();
					GUILayout.Label("Part ID : " + PDHolder.Part[cnt].PartID);
					GUILayout.FlexibleSpace();
					GUILayout.EndHorizontal();
				}
			}

		}
		//Right Leg
		if (PDHolder.currentTab == 3)
		{
			for (int cnt = 0; cnt < PDHolder.Part.Count; cnt++)
			{
				if (PDHolder.Part[cnt].PartType == "Right Leg")
				{
					GUILayout.BeginHorizontal();
					PDHolder.Part[cnt].PartData.Part_Name = EditorGUILayout.TextField(PDHolder.Part[cnt].PartData.Part_Name, GUILayout.Width(100));
					EditorGUILayout.ObjectField("", PDHolder.Part[cnt].PartData, typeof(Part_Data), true);
					GUILayout.Label(PDHolder.Part[cnt].PartType);
					if (GUILayout.Button("X"))
					{
						RemovePart(cnt);
						return;
					}
					GUILayout.EndHorizontal();
					GUILayout.BeginHorizontal();
					GUILayout.FlexibleSpace();
					GUILayout.Label(PDHolder.Part[cnt].Path);
					GUILayout.FlexibleSpace();
					GUILayout.Label("Part ID : " + PDHolder.Part[cnt].PartID);
					GUILayout.FlexibleSpace();
					GUILayout.EndHorizontal();
				}
			}

		}
		#endregion

		if (GUILayout.Button("Update Database"))
		{
			UpdateDatabase();
			AssetDatabase.Refresh();
			EditorUtility.SetDirty(PDHolder);
			AssetDatabase.SaveAssets();
		}
	}

	void OnValidate()
	{
		UpdateDatabase();
	}

	void AddPart(Part_Data part)
	{
		bool existing = false;
		for (int cnt = 0; cnt < PDHolder.Part.Count; cnt++)
		{
			if (part.ID == PDHolder.Part[cnt].PartID)
			{
				existing = true;
			}
		}

		if (existing == false)
		{
			PDHolder.Part.Add(new Parts());
			PDHolder.Part[PDHolder.Part.Count - 1].PartData = part;
			PDHolder.Part[PDHolder.Part.Count - 1].PartID = part.ID;
			PDHolder.Part[PDHolder.Part.Count - 1].PartName = part.Part_Name;
			PDHolder.Part[PDHolder.Part.Count - 1].PartType = part.PartType;
			PDHolder.Part[PDHolder.Part.Count - 1].Path = path;
			obj = null;
			path = null;
		}		
	}

	void RemovePart(int index)
	{
		PDHolder.Part.RemoveAt(index);
	}

	public void UpdateDatabase()
	{
		if (PDHolder.Part.Count > 0)
		{
			for (int i = 0; i < PDHolder.Part.Count; i++)
			{
				PDHolder.Part[i].PartData = AssetDatabase.LoadAssetAtPath(PDHolder.Part[i].Path, typeof(Part_Data)) as Part_Data;
				PDHolder.Part[i].PartID = PDHolder.Part[i].PartData.ID;
				PDHolder.Part[i].PartName = PDHolder.Part[i].PartData.Part_Name;
				PDHolder.Part[i].PartType = PDHolder.Part[i].PartData.PartType;
			}
		}
	}

}
