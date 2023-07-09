#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using Unity.EditorCoroutines.Editor;
using UnityEditor.VersionControl;
using Object = UnityEngine.Object;

namespace AquaKitty.EditorTools
{
	public class AssetReferenceUtility : EditorWindow
	{
		public class AssetTypeSettings
		{
			public string Name;
			public string ExtensionPattern;
			public bool Enabled = true;
			
			public AssetTypeSettings(string name, string extensionPattern)
			{
				Name = name;
				ExtensionPattern = extensionPattern;
			}
		}

		private static List<AssetTypeSettings> _assetTypeSettings = new List<AssetTypeSettings>()
		{
			new AssetTypeSettings("Prefab", "*.prefab"),
			new AssetTypeSettings("Material", "*.mat"),
			new AssetTypeSettings("Scene", "*.unity"),
			new AssetTypeSettings("Shader", "*.shader"),
			new AssetTypeSettings("Shader Graph", "*.shadergraph"),
			new AssetTypeSettings("Scriptable Object Asset", "*.asset")
		};
		
		public class ReferenceDetails
		{
			public Object Target;
			public int RefCount = 0;
			public Object ReplacementAssetOverride;
			public bool OverrideReplacementAsset = false;
			public bool ReplaceReference = true;
			
			public ReferenceDetails(Object target)
			{
				Target = target;
			}
		}
		
		private Object _selectedAsset;
		private Object _previousAsset;
		private string _selectedAssetGUID;
		private string _selectedAssetPath;
		private Object _replacementAsset;
		private Object _previousReplacementAsset;
		private string _replacementAssetGUID;
		private string _replacementAssetPath;
		private bool _isSearchInProgress = false;
		private float _searchProgress = 0f;
		private string _searchMessage = "Searching for References...";
		private bool _isReplacingReferences = false;
		private bool _isAssetTypeFoldoutOpen = false;
		private List<ReferenceDetails> _references = new List<ReferenceDetails>();
		private Vector2 _scrollPos = Vector2.zero;

		[MenuItem("Tools/AquaKitty/Reference Utility")]
		public static void Open()
		{
			GetWindow(typeof(AssetReferenceUtility), false, "Asset Reference Utility");
		}

		private void OnGUI()
		{
			if (EditorSettings.serializationMode != SerializationMode.ForceText)
			{
				GUI.color = Color.red;
				GUILayout.Label("Reference Utility requires Asset Serialization Mode set to Force Text");
				GUI.color = Color.white;
				return;
			}
			
			DrawHeader();

			if (_isSearchInProgress)
			{
				DrawSearchProgressBar();
				return;
			}
			
			if (_references == null || _references.Count == 0)
			{
				return;
			}
			DrawReferences();
		}

		private void DrawHeader()
		{
			GUILayout.Space(16f);
			GUILayout.Label("Asset Reference Utility:", UnityEditor.EditorStyles.largeLabel, GUILayout.Height(32f));
			GUILayout.Space(16f);
			
			DrawAssetSelection("Original Asset", ref _selectedAsset, ref _previousAsset, ref _selectedAssetPath, ref _selectedAssetGUID);
			DrawAssetTypeSettings();

			if (_selectedAsset == null)
			{
				return;
			}
			
			GUILayout.Space(16f);
			
			if (_isSearchInProgress)
			{
				GUILayout.Box("Searching...");
			}
			else if (GUILayout.Button("SEARCH FOR REFERENCES"))
			{
				EditorCoroutineUtility.StartCoroutineOwnerless(SearchForReferences());
			}
		}

		private void DrawReferences()
		{
			GUILayout.Space(16f);
			
			DrawAssetSelection("Replacement Asset", ref _replacementAsset, ref _previousReplacementAsset, ref _replacementAssetPath, ref _replacementAssetGUID);
			bool hasReplacementAsset = _replacementAsset != null;
			if (!hasReplacementAsset)
			{
				for (int i = 0; i < _references.Count; ++i)
				{
					ReferenceDetails reference = _references[i];
					if (reference.ReplaceReference && reference.OverrideReplacementAsset && reference.ReplacementAssetOverride != null)
					{
						hasReplacementAsset = true;
					}
				}
			}

			GUILayout.Space(16f);
			
			if (_isReplacingReferences)
			{
				GUILayout.Box("Replacing references...");
			}
			else if (hasReplacementAsset && GUILayout.Button("Replace References"))
			{
				EditorCoroutineUtility.StartCoroutineOwnerless(ReplaceReferences());
			}
			GUILayout.Space(32f);
			GUILayout.Box(string.Format("Referenced by {0} assets", _references.Count));
			_scrollPos = GUILayout.BeginScrollView(_scrollPos);
			{
				for (int i = 0; i < _references.Count; ++i)
				{
					GUILayout.Space(24f);
					ReferenceDetails reference = _references[i];

					GUILayout.BeginHorizontal();
					{
						GUI.enabled = false;
						EditorGUILayout.ObjectField(string.Empty, reference.Target, typeof(Object), false);
						GUI.enabled = true;
						GUILayout.Label("Count: " + reference.RefCount);
					}
					GUILayout.EndHorizontal();
					
					reference.ReplaceReference = EditorGUILayout.Toggle("Replace", reference.ReplaceReference);
					if (!reference.ReplaceReference)
					{
						continue;
					}

					GUILayout.BeginHorizontal();
					{
						reference.OverrideReplacementAsset = EditorGUILayout.Toggle("Override Replacement", reference.OverrideReplacementAsset);
					
						bool overrideReplacement = reference.OverrideReplacementAsset;
						if (!overrideReplacement)
						{
							GUILayout.EndHorizontal();
							continue;
						}
					
						Object replacement = overrideReplacement ? reference.ReplacementAssetOverride : _replacementAsset;
						reference.ReplacementAssetOverride = EditorGUILayout.ObjectField(string.Empty, replacement, typeof(Object), false);
					}
					GUILayout.EndHorizontal();
				}

			}
			GUILayout.EndScrollView();
		}

		private void DrawSearchProgressBar()
		{
			EditorUtility.DisplayProgressBar("Searching for References...", _searchMessage, _searchProgress);
		}

		private void DrawAssetSelection(string message, ref Object asset, ref Object previousAsset, ref string assetPath, ref string assetGUID)
		{
			asset = EditorGUILayout.ObjectField(message, asset, typeof(Object), false);
			if (asset == null)
			{
				assetPath = null;
				assetGUID = null;
				previousAsset = null;
				return;
			}
			
			if (previousAsset == null || previousAsset != asset)
			{
				assetPath = AssetDatabase.GetAssetPath(asset);
				assetGUID = AssetDatabase.AssetPathToGUID(assetPath);
				previousAsset = asset;
			}
		}

		private IEnumerator ReplaceReferences()
		{
			_isReplacingReferences = true;
			for (int i = 0; i < _references.Count; ++i)
			{
				ReferenceDetails reference = _references[i];
				if (!reference.ReplaceReference)
				{
					continue;
				}

				string newGUID = _replacementAssetGUID;
				if (reference.OverrideReplacementAsset)
				{
					if (reference.ReplacementAssetOverride == null)
					{
						continue;
					}
					
					string overridePath = AssetDatabase.GetAssetPath(reference.ReplacementAssetOverride);
					newGUID = AssetDatabase.AssetPathToGUID(overridePath);
				}
				
				Task checkout = Provider.Checkout(reference.Target, CheckoutMode.Both);
				checkout.Wait();
				
				string assetPath = AssetDatabase.GetAssetPath(reference.Target);
				string file = File.ReadAllText(assetPath);
				string replacedFile = file.Replace(_selectedAssetGUID, newGUID);
				File.WriteAllText(assetPath, replacedFile);
			}
			
			AssetDatabase.Refresh(ImportAssetOptions.Default);
			_isReplacingReferences = false;

			RefreshReferences();
			yield return null;
		}
		
		private void DrawAssetTypeSettings()
		{
			_isAssetTypeFoldoutOpen = EditorGUILayout.Foldout(_isAssetTypeFoldoutOpen, "Asset Type Filters");
			if (!_isAssetTypeFoldoutOpen)
			{
				return;
			}
			
			GUILayout.BeginHorizontal();
			GUILayout.Space(24f);
			GUILayout.BeginVertical();
			
			for (int i = 0; i < _assetTypeSettings.Count; ++i)
			{
				AssetTypeSettings assetSettings = _assetTypeSettings[i];
				assetSettings.Enabled = EditorGUILayout.Toggle(assetSettings.Name, assetSettings.Enabled);
			}

			GUILayout.EndVertical();
			GUILayout.EndHorizontal();
		}

		private IEnumerator SearchForReferences()
		{
			_isSearchInProgress = true;
			_searchProgress = 0f;
			_references.Clear();
			
			List<string> allAssetsPaths = new List<string>();
			for (int i = 0; i < _assetTypeSettings.Count; i++)
			{
				AssetTypeSettings settings = _assetTypeSettings[i];
				if (!settings.Enabled)
				{
					continue;
				}
				
				_searchProgress = (i+1) / _assetTypeSettings.Count;
				_searchMessage = "Searching: " + settings.Name + "s";
				
				string[] assetPaths = Directory.GetFiles(Application.dataPath, settings.ExtensionPattern, SearchOption.AllDirectories);
				allAssetsPaths.AddRange(assetPaths);
			}
			
			for (int i = 0; i < allAssetsPaths.Count; ++i)
			{
				string assetPath = allAssetsPaths[i];
				string file = File.ReadAllText(assetPath);
				string[] lines = file.Split('\n');
				
				for (int j = 0; j < lines.Length; ++j)
				{
					string line = lines[j];
					
					if (!line.Contains("guid:") || !line.Contains(_selectedAssetGUID))
					{
						continue;
					}
					
					string pathToReferenceAsset = assetPath.Replace(Application.dataPath, string.Empty);
					pathToReferenceAsset = pathToReferenceAsset.Replace(".meta", string.Empty);
					string fixedPath = "Assets" + pathToReferenceAsset;

					// Convert file path to correct platform
					if (Application.platform == RuntimePlatform.WindowsEditor)
					{
						fixedPath = fixedPath.Replace("/", @"\");
					}
					else if (Application.platform == RuntimePlatform.OSXEditor)
					{
						fixedPath = fixedPath.Replace(@"\", "/");
					}
					
					Object asset = AssetDatabase.LoadAssetAtPath<Object>(fixedPath);
					if (asset == null)
					{
						Debug.LogError("Failed to load asset at path: " + fixedPath);
						continue;
					}
					
					ReferenceDetails details = _references.Find(x => x.Target == asset);
					if (details == null)
					{
						details = new ReferenceDetails(asset);
						_references.Add(details);
					}
					details.RefCount++;
				}
			}
			_isSearchInProgress = false;
			yield return null;
		}
		
		private void RefreshReferences()
		{
			if (_references == null)
			{
				return;
			}
			
			for (int i = _references.Count - 1; i >= 0; --i)
			{
				ReferenceDetails referenceDetails = _references[i];
				string assetPath = AssetDatabase.GetAssetPath(referenceDetails.Target);
				string file = File.ReadAllText(assetPath);
				string[] lines = file.Split('\n');
				
				bool foundGUI = false;
				for (int j = 0; j < lines.Length; ++j)
				{
					string line = lines[j];
					if (!line.Contains("guid:"))
					{
						continue;
					}

					if (line.Contains(_selectedAssetGUID))
					{
						foundGUI = true;
						break;
					}
				}

				if (!foundGUI)
				{
					_references.RemoveAt(i);
				}
			}
		}
	}
}
#endif







