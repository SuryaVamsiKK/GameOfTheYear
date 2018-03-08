using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Player_Body))]
public class Build_Inspect : Editor {

	public override void OnInspectorGUI()
	{
	

		var boldtext = new GUIStyle(GUI.skin.label);
		boldtext.fontStyle = FontStyle.Bold;
		boldtext.fontSize = 20;

		var attributeText = new GUIStyle(GUI.skin.label);
		attributeText.fontStyle = FontStyle.BoldAndItalic;
		//boldtext.fontSize = 20;

		Player_Body PlayerBody = (Player_Body)target;

		GUILayout.Space(20);
		GUILayout.BeginVertical("Box");
		GUILayout.Space(5);

		//Heading
		#region
		GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
		GUILayout.Label("Character Build", boldtext);
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();
		GUILayout.Space(5);
		#endregion

		//Left Hand
		#region
		GUILayout.BeginHorizontal();
		GUILayout.Space(5);

		GUILayout.Label("Left Hand", attributeText);
		GUILayout.FlexibleSpace();
		PlayerBody.Left_Hand = EditorGUILayout.TextField(PlayerBody.Left_Hand.ToString(), GUILayout.ExpandWidth(true));
		GUILayout.Space(5);

		GUILayout.EndHorizontal();
		GUILayout.Space(5);
		#endregion
		
		//Right Hand
		#region
		GUILayout.BeginHorizontal();
		GUILayout.Space(5);

		GUILayout.Label("Right Hand", attributeText);
		GUILayout.FlexibleSpace();
		PlayerBody.Right_Hand = EditorGUILayout.TextField(PlayerBody.Right_Hand.ToString(), GUILayout.ExpandWidth(true));
		GUILayout.Space(5);

		GUILayout.EndHorizontal();
		GUILayout.Space(5);
		#endregion

		//Left Leg
		#region
		GUILayout.BeginHorizontal();
		GUILayout.Space(5);

		GUILayout.Label("Left Leg", attributeText);
		GUILayout.FlexibleSpace();
		PlayerBody.Left_Leg = EditorGUILayout.TextField(PlayerBody.Left_Leg.ToString(), GUILayout.ExpandWidth(true));
		GUILayout.Space(5);

		GUILayout.EndHorizontal();
		GUILayout.Space(5);
		#endregion

		//Right Leg
		#region
		GUILayout.BeginHorizontal();
		GUILayout.Space(5);

		GUILayout.Label("Right Leg", attributeText);
		GUILayout.FlexibleSpace();
		PlayerBody.Right_Leg = EditorGUILayout.TextField(PlayerBody.Right_Leg.ToString(), GUILayout.ExpandWidth(true));
		GUILayout.Space(5);

		GUILayout.EndHorizontal();
		GUILayout.Space(5);
		#endregion


		//Buttons
		#region
		GUILayout.BeginHorizontal();
		GUILayout.Space(5);
		
		if (GUILayout.Button("Apply"))
		{
			PlayerBody.Assignment();
			PlayerBody.WeightCalcualtion();
			PlayerBody.VisualUpdate();
		}

		if (GUILayout.Button("Reset"))
		{
			//PlayerBody.Left_Hand = 1;
			//PlayerBody.Right_Hand = 1;
			PlayerBody.Reset();
			PlayerBody.Left_Hand = EditorGUILayout.TextField(PlayerBody.Left_Hand.ToString(), GUILayout.ExpandWidth(true));
			PlayerBody.Right_Hand = EditorGUILayout.TextField(PlayerBody.Right_Hand.ToString(), GUILayout.ExpandWidth(true));
			PlayerBody.Left_Leg = EditorGUILayout.TextField(PlayerBody.Left_Leg.ToString(), GUILayout.ExpandWidth(true));
			PlayerBody.Right_Leg = EditorGUILayout.TextField(PlayerBody.Right_Leg.ToString(), GUILayout.ExpandWidth(true));

		}

		GUILayout.Space(5);
		GUILayout.EndHorizontal();
		GUILayout.Space(5);
		#endregion

		GUILayout.EndVertical();
		GUILayout.Space(20);

		base.OnInspectorGUI();
	}
}
