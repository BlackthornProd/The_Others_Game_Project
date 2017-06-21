using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public Question[] questions; // an array of questions from the Question script !
	private static List<Question> unansweredQuestions;// a list of type Question.

	private Question currentQuestion;

	[SerializeField] private Text factText; // SerializeField means we can see this Text var in the inspector but we can't access it in any other script !
	[SerializeField] private Text trueAnswerText;
	[SerializeField] private Text falseAnswerText;

	[SerializeField] private float timeBtwQuestions = 2f;

	public Animator anim;

	void Start(){


		if(unansweredQuestions == null || unansweredQuestions.Count == 0){ // if there is nothing in this list... OR if the list as 0 items in it ...
				unansweredQuestions = questions.ToList<Question>(); // Here we are loading all the items in the questions array into the list !
		}

		SetCurrentQuestion(); // Calling the function !
	}

	void SetCurrentQuestion(){

		int randomQuestionIndex = Random.Range(0, unansweredQuestions.Count); // grabbing a random question from our list !
		currentQuestion = unansweredQuestions[randomQuestionIndex]; // and making the current question equal to the item in the list of index randomQuestionIndex !

		factText.text = currentQuestion.fact; // this will display the question in our UI when we play the game !

		if(currentQuestion.isTrue){// if the answer of our current question is true then ...
			trueAnswerText.text = "CORRECT !";// then the true answer if correct
			falseAnswerText.text = "WRONG !";// and the false answer is wrong
		} else { // and vice versa ...
			trueAnswerText.text = "WRONG !";
			falseAnswerText.text = "CORRECT !";
		}
	}

	IEnumerator TransitionToNextQuestion(){

		unansweredQuestions.Remove(currentQuestion); // we remove from our list the current question item so we don't have to answer it twice!

		yield return new WaitForSeconds(timeBtwQuestions); // wait some time before the next question !

		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // it will reload the current scene so we get a new Question !

	}

	public void UserSelectTrue(){ // if the user pressed the true button ...

		if(currentQuestion.isTrue){ // ... and the current fact was true ...
			Debug.Log("CORRECT !");
		} else { // if he pressed the tryue button but the current fact was false then ...
			Debug.Log("WRONG");
		}

		anim.SetTrigger("False");
		StartCoroutine(TransitionToNextQuestion()); // when we answer true we call the CoRoutine !
	}

	public void UserSelectFalse(){ // if the user pressed the false button ...

		if(!currentQuestion.isTrue){ // ... and the current fact was false ...
			Debug.Log("CORRECT !");
		} else { // if he pressed the false button but the current fact was true then ...
			Debug.Log("WRONG");
		}

		anim.SetTrigger("True");
		StartCoroutine(TransitionToNextQuestion());// when we answer false we call the CoRoutine !
	}
}
