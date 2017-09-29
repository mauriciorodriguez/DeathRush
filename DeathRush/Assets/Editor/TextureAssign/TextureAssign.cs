using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;


public class TextureAssign : EditorWindow
{
	//Supported file formats.
	//Add more of this if you want to support more texture file formats, and also add the GetFiles code below.
	string pngExtension = "*.png";
	string jpgExtension = "*.jpg";

	//This is to set the textures of the material using material.SetTexture()
	//The source of this is in UnityStandardInput.cginc which is in the Unity build in shaders (separate download).
	string materialAlbedo = "_MainTex";
	string materialMetallic = "_MetallicGlossMap";
	string materialSpecular = "_SpecGlossMap";
	string materialNormal = "_BumpMap";
	string materialHeight = "_ParallaxMap";
	string materialOcclusion = "_OcclusionMap";
	string materialEmission = "_EmissionMap";

	//These are the names added to the textures when exported by Substance Painter.
	const string textureAlbedo = "_AlbedoTransparency";
	const string textureMetallic = "_MetallicSmoothness";
	const string textureSpecular = "_SpecularSmoothness";
	const string textureNormal = "_Normal";
	const string textureHeight = "_Height"; //This texture has to be manually added to the export preset in SP, so make sure the name is correct.
	const string textureOcclusion = "_AO"; //This texture has to be manually added to the export preset in SP, so make sure the name is correct.
	const string textureEmission = "_Emission";


	[MenuItem("Window/TextureAssign")]
	static void Init()
	{
		// Get existing open window or if none, make a new one:
		TextureAssign textureAssignWindow = (TextureAssign)EditorWindow.GetWindow(typeof(TextureAssign));
		textureAssignWindow.position = new Rect(100, 200, 300, 100);
	}

	void OnGUI()
	{
		List<string> texturePathsList = new List<string>();
		string[] materialPaths;

		GUILayout.Space(20);
		GUILayout.BeginHorizontal();

		if (GUILayout.Button("Assign Textures"))
		{
			//Get all the materials in the project.
			materialPaths = Directory.GetFiles("Assets\\", "*.mat", SearchOption.AllDirectories);

			//Create array which stores the loaded materials.
			Material[] materials = new Material[materialPaths.Length];

			//Loop through all the found materials.
			for (int i = 0; i < materialPaths.Length; i++)
			{
				//Load all materials into memory
				materials[i] = (Material)(AssetDatabase.LoadAssetAtPath(materialPaths[i], typeof(Material)));
			}

			//Get all the png textures in the project.
			string[] pngPaths = Directory.GetFiles("Assets\\", pngExtension, SearchOption.AllDirectories);

			//Add the png files to the list.
			texturePathsList.AddRange(pngPaths);

			/////////////////////////////////////////////////////
			//////////Add more of this if you want to support more texture file formats.
			//Get all the jpg textures in the project and add it to the list.
			string[] jpgPaths = Directory.GetFiles("Assets\\", jpgExtension, SearchOption.AllDirectories);
			texturePathsList.AddRange(jpgPaths);
			/////////////////////////////////////////////////////


			//Loop through all the textures.
			for( int i = 0; i < texturePathsList.Count; i++)
			{
				//Load the texture into memory.
				Texture2D texture = (Texture2D)(AssetDatabase.LoadAssetAtPath(texturePathsList[i], typeof(Texture2D)));

				//Get the texture name.
				string textureName = texture.name;

				//Find the last underscore character.
                int index = FindLastChar(textureName, '_');

				//Found.
				if(index != -1)
				{
					//Get the name without the designation.
					string texturePart = textureName.Substring(0, index);

					//Get the designation
					string designation = textureName.Substring(index);

					//Loop through all the materials.
					for (int e = 0; e < materials.Length; e++)
					{
						//Is the material name the same as the texture name?
						if(materials[e].name == texturePart)
						{
							//Assign the texture to the material.
							AssignTexture(materials[e], texture, texturePathsList[i], designation);

							//Bail out.
							e = materials.Length;
                        }
					}
				}
            }
		}

		GUILayout.EndHorizontal();
	}

	static int FindLastChar(string arr, char value)
	{
		for (int i = (arr.Length-1); i >= 0; i--)
		{
			if (arr[i] == value)
			{
				return i;
			}
		}
		return -1;
	}

	void AssignTexture(Material material, Texture2D texture, string texturePath, string designation)
	{
		switch (designation)
		{
			case textureAlbedo:
				material.SetTexture(materialAlbedo, texture);

				//This is important, otherwise the texture will look wrong.
				material.SetColor("_Color", Color.white);
				break;

			case textureMetallic:
				material.EnableKeyword("_METALLICGLOSSMAP");
				material.SetTexture(materialMetallic, texture);
				break;

			case textureSpecular:
				material.EnableKeyword("_SPECGLOSSMAP");
				material.SetTexture(materialSpecular, texture);
				break;

			case textureNormal:
				
				material.EnableKeyword("_NORMALMAP");

				//A normal map should be marked as a normal map.
				TextureImporter importer = AssetImporter.GetAtPath(texturePath) as TextureImporter;
				importer.textureType = TextureImporterType.NormalMap;

			//	UnityEditor.EditorUtility.SetDirty(importer);

				AssetDatabase.WriteImportSettingsIfDirty(texturePath);
				//AssetDatabase.ImportAsset(texturePath, ImportAssetOptions.ForceUpdate);

				material.SetTexture(materialNormal, texture);

				UnityEditor.AssetDatabase.SaveAssets();
				UnityEditor.AssetDatabase.Refresh();
				break;

			case textureHeight:
				material.EnableKeyword("_PARALLAXMAP");
				material.SetTexture(materialHeight, texture);
				break;

			case textureOcclusion:
				material.SetTexture(materialOcclusion, texture);
				break;

			case textureEmission:
				material.EnableKeyword("_EMISSION");
				material.SetTexture(materialEmission, texture);
				material.SetColor("_EmissionColor", Color.white);
				break;

			default:
			break;
		}
	}
}