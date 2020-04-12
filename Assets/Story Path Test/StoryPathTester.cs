using System.Linq;
using Ink.Runtime;
using UnityEngine;

public class StoryPathTester : MonoBehaviour {
	[SerializeField] private TextAsset m_StoryJSON;
	
	private void Start() {
		Story story = new Story (m_StoryJSON.text);
		while (story.canContinue) {
			story.Continue();
			string storyPath = story.state.currentPathString;
			string knotName = storyPath != null ? storyPath.Substring(0, storyPath.IndexOf(".")) : "";
			string firstTag = story.currentTags.Any() ? int.Parse(story.currentTags.First()).ToString("00") : "--no tag--";
			string lineID = knotName + "." + firstTag;
			string info = $"LineID {lineID} (path {storyPath}): {story.currentText.Trim()}";

			if (storyPath == null) {
				Debug.LogError($"<color=red>MISSING PATH</color>: {info}", m_StoryJSON);
			} else {
				Debug.Log(info, m_StoryJSON);
			}

			if (story.currentChoices.Any()) {
				story.ChooseChoiceIndex(0);
			}
		}
	}
}