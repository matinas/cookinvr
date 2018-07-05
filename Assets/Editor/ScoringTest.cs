using UnityEngine;
using NUnit.Framework;
using System.Collections.Generic;

public class ScoringTest {

	private List<string> burgerIngs = new List<string>();

	private GameObject go;

	private int correctScore = 10;
	private int wrongIngrPenalty = -5;

	[OneTimeSetUpAttribute]
	public void Init() // Variables to set once before starting test executions
	{
		go = new GameObject();
		go.AddComponent<ScoreManager_v2>();
		go.GetComponent<ScoreManager_v2>().correctScore = correctScore;
		go.GetComponent<ScoreManager_v2>().wrongIngrPenalty = wrongIngrPenalty;
	}

	[SetUp]
	public void Setup()	// Variables to set before starting each test
	{
		burgerIngs.Clear();
		burgerIngs.Add("Bottom Bun");
		burgerIngs.Add("Beef");
		burgerIngs.Add("Cheese");
		burgerIngs.Add("Tomatoe");
		burgerIngs.Add("Topper Bun");
	}

	[Test]
	public void CorrectRecipe()
	{
		List<string> ings = new List<string>();
		ings.Add("Bottom Bun");
		ings.Add("Beef");
		ings.Add("Cheese");
		ings.Add("Tomatoe");
		ings.Add("Topper Bun");

		int ingCount = ings.Count; // This is done here because as we are passing a list to CalculateScore it's passed as a reference an it's modified
		
		ScoreData sd = go.GetComponent<ScoreManager_v2>().CalculateScore(ings, burgerIngs);

		List<int> expectedScoring = new List<int>();
		expectedScoring.Add(correctScore);
		expectedScoring.Add(correctScore);
		expectedScoring.Add(correctScore);
		expectedScoring.Add(correctScore);
		expectedScoring.Add(correctScore);

		Assert.AreEqual(correctScore*ingCount, sd.finalScore);
		Assert.AreEqual(expectedScoring, sd.partialScorings);
	}

	[Test]
	public void MissingFirstIngredient()
	{
		List<string> ings = new List<string>();
		ings.Add("Beef");
		ings.Add("Cheese");
		ings.Add("Tomatoe");
		ings.Add("Topper Bun");
		
		int ingCount = ings.Count; // This is done here because as we are passing a list to CalculateScore it's passed as a reference an it's modified

		ScoreData sd = go.GetComponent<ScoreManager_v2>().CalculateScore(ings, burgerIngs);

		List<int> expectedScoring = new List<int>();
		expectedScoring.Add(wrongIngrPenalty);
		expectedScoring.Add(correctScore);
		expectedScoring.Add(correctScore);
		expectedScoring.Add(correctScore);
		expectedScoring.Add(correctScore);

		Assert.AreEqual(correctScore*ingCount + wrongIngrPenalty, sd.finalScore);
		Assert.AreEqual(expectedScoring, sd.partialScorings);
	}

	[Test]
	public void MissingLastIngredient()
	{
		List<string> ings = new List<string>();
		ings.Add("Bottom Bun");
		ings.Add("Beef");
		ings.Add("Cheese");
		ings.Add("Tomatoe");
		
		int ingCount = ings.Count; // This is done here because as we are passing a list to CalculateScore it's passed as a reference an it's modified

		ScoreData sd = go.GetComponent<ScoreManager_v2>().CalculateScore(ings, burgerIngs);

		List<int> expectedScoring = new List<int>();
		expectedScoring.Add(correctScore);
		expectedScoring.Add(correctScore);
		expectedScoring.Add(correctScore);
		expectedScoring.Add(correctScore);
		expectedScoring.Add(wrongIngrPenalty);

		Assert.AreEqual(correctScore*ingCount + wrongIngrPenalty, sd.finalScore);
		Assert.AreEqual(expectedScoring, sd.partialScorings);
	}

	[Test]
	public void MissingMiddleIngredient()
	{
		List<string> ings = new List<string>();
		ings.Add("Bottom Bun");
		ings.Add("Beef");
		ings.Add("Tomatoe");
		ings.Add("Topper Bun");

		int ingCount = ings.Count; // This is done here because as we are passing a list to CalculateScore it's passed as a reference an it's modifieds
		
		ScoreData sd = go.GetComponent<ScoreManager_v2>().CalculateScore(ings, burgerIngs);

		List<int> expectedScoring = new List<int>();
		expectedScoring.Add(correctScore);
		expectedScoring.Add(correctScore);
		expectedScoring.Add(wrongIngrPenalty);
		expectedScoring.Add(correctScore);
		expectedScoring.Add(correctScore);

		Assert.AreEqual(correctScore*ingCount + wrongIngrPenalty, sd.finalScore);
		Assert.AreEqual(expectedScoring, sd.partialScorings);
	}

	[Test]
	public void ExtraInvalidStartIngredient()
	{
		List<string> ings = new List<string>();
		ings.Add("Extra");
		ings.Add("Bottom Bun");
		ings.Add("Beef");
		ings.Add("Cheese");
		ings.Add("Tomatoe");
		ings.Add("Topper Bun");

		int ingCount = ings.Count; // This is done here because as we are passing a list to CalculateScore it's passed as a reference an it's modifieds
		
		ScoreData sd = go.GetComponent<ScoreManager_v2>().CalculateScore(ings, burgerIngs);

		List<int> expectedScoring = new List<int>();
		expectedScoring.Add(wrongIngrPenalty);
		expectedScoring.Add(correctScore);
		expectedScoring.Add(correctScore);
		expectedScoring.Add(correctScore);
		expectedScoring.Add(correctScore);
		expectedScoring.Add(correctScore);

		Assert.AreEqual(correctScore*(ingCount-1) + wrongIngrPenalty, sd.finalScore);
		Assert.AreEqual(expectedScoring, sd.partialScorings);
	}

	[Test]
	public void ExtraValidStartIngredient()
	{
		List<string> ings = new List<string>();
		ings.Add("Tomatoe");
		ings.Add("Bottom Bun");
		ings.Add("Beef");
		ings.Add("Cheese");
		ings.Add("Tomatoe");
		ings.Add("Topper Bun");

		int ingCount = ings.Count; // This is done here because as we are passing a list to CalculateScore it's passed as a reference an it's modified
		
		ScoreData sd = go.GetComponent<ScoreManager_v2>().CalculateScore(ings, burgerIngs);

		List<int> expectedScoring = new List<int>();
		expectedScoring.Add(wrongIngrPenalty);
		expectedScoring.Add(correctScore);
		expectedScoring.Add(correctScore);
		expectedScoring.Add(correctScore);
		expectedScoring.Add(correctScore);
		expectedScoring.Add(correctScore);

		Assert.AreEqual(correctScore*(ingCount-1) + wrongIngrPenalty, sd.finalScore);
		Assert.AreEqual(expectedScoring, sd.partialScorings);
	}

	[Test]
	public void ExtraValidMiddleIngredient()
	{
		List<string> ings = new List<string>();
		ings.Add("Bottom Bun");
		ings.Add("Beef");
		ings.Add("Cheese");
		ings.Add("Cheese");
		ings.Add("Tomatoe");
		ings.Add("Topper Bun");

		int ingCount = ings.Count; // This is done here because as we are passing a list to CalculateScore it's passed as a reference an it's modified
		
		ScoreData sd = go.GetComponent<ScoreManager_v2>().CalculateScore(ings, burgerIngs);

		List<int> expectedScoring = new List<int>();
		expectedScoring.Add(correctScore);
		expectedScoring.Add(correctScore);
		expectedScoring.Add(correctScore);
		expectedScoring.Add(wrongIngrPenalty);
		expectedScoring.Add(correctScore);
		expectedScoring.Add(correctScore);

		Assert.AreEqual(correctScore*(ingCount-1) + wrongIngrPenalty, sd.finalScore);
		Assert.AreEqual(expectedScoring, sd.partialScorings);
	}

	[Test]
	public void MultipleMissingTogetherIngredients()
	{
		List<string> ings = new List<string>();
		ings.Add("Bottom Bun");
		ings.Add("Tomatoe");
		ings.Add("Topper Bun");

		int ingCount = ings.Count; // This is done here because as we are passing a list to CalculateScore it's passed as a reference an it's modified
		
		ScoreData sd = go.GetComponent<ScoreManager_v2>().CalculateScore(ings, burgerIngs);

		List<int> expectedScoring = new List<int>();
		expectedScoring.Add(correctScore);
		expectedScoring.Add(wrongIngrPenalty);
		expectedScoring.Add(wrongIngrPenalty);
		expectedScoring.Add(correctScore);
		expectedScoring.Add(correctScore);

		Assert.AreEqual(correctScore*ingCount + wrongIngrPenalty*2, sd.finalScore);
		Assert.AreEqual(expectedScoring, sd.partialScorings);
	}

	[Test]
	public void MultipleMissingAlternateIngredients()
	{
		List<string> ings = new List<string>();
		ings.Add("Bottom Bun");
		ings.Add("Cheese");
		ings.Add("Topper Bun");

		int ingCount = ings.Count; // This is done here because as we are passing a list to CalculateScore it's passed as a reference an it's modified
		
		ScoreData sd = go.GetComponent<ScoreManager_v2>().CalculateScore(ings, burgerIngs);

		List<int> expectedScoring = new List<int>();
		expectedScoring.Add(correctScore);
		expectedScoring.Add(wrongIngrPenalty);
		expectedScoring.Add(correctScore);
		expectedScoring.Add(wrongIngrPenalty);
		expectedScoring.Add(correctScore);

		Assert.AreEqual(correctScore*ingCount + wrongIngrPenalty*2, sd.finalScore);
		Assert.AreEqual(expectedScoring, sd.partialScorings);
	}

	[Test]
	public void MultipleExtraInvalidIngredients()
	{
		List<string> ings = new List<string>();
		ings.Add("Extra");
		ings.Add("Bottom Bun");
		ings.Add("Extra");
		ings.Add("Beef");
		ings.Add("Extra");
		ings.Add("Cheese");
		ings.Add("Extra");
		ings.Add("Tomatoe");
		ings.Add("Extra");
		ings.Add("Topper Bun");
		ings.Add("Extra");

		int ingCount = ings.Count; // This is done here because as we are passing a list to CalculateScore it's passed as a reference an it's modifieds
		
		ScoreData sd = go.GetComponent<ScoreManager_v2>().CalculateScore(ings, burgerIngs);

		List<int> expectedScoring = new List<int>();
		expectedScoring.Add(wrongIngrPenalty);
		expectedScoring.Add(correctScore);
		expectedScoring.Add(wrongIngrPenalty);
		expectedScoring.Add(correctScore);
		expectedScoring.Add(wrongIngrPenalty);
		expectedScoring.Add(correctScore);
		expectedScoring.Add(wrongIngrPenalty);
		expectedScoring.Add(correctScore);
		expectedScoring.Add(wrongIngrPenalty);
		expectedScoring.Add(correctScore);
		expectedScoring.Add(wrongIngrPenalty);

		Assert.AreEqual(correctScore*(ingCount-6) + wrongIngrPenalty*6, sd.finalScore);
		Assert.AreEqual(expectedScoring, sd.partialScorings);
	}
}
