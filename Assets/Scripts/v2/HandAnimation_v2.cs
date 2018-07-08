using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class HandAnimation_v2 : MonoBehaviour {

	private Animator anim;

	private Hand hand;

	private int Idle = Animator.StringToHash("Idle");
    private int Point = Animator.StringToHash("Point");
    private int GrabLarge = Animator.StringToHash("GrabLarge");
    private int GrabSmall = Animator.StringToHash("GrabSmall");
    private int ThumbUp = Animator.StringToHash("ThumbUp");
	private int Fist = Animator.StringToHash("Fist");
    private int MiddleFinger = Animator.StringToHash("MiddleFinger");
  	private int Phone = Animator.StringToHash("Phone");
	private int Rock = Animator.StringToHash("Rock");
	private int Natural = Animator.StringToHash("Natural");

	void Awake()
	{
		anim = GetComponent<Animator>();
		hand = GetComponentInParent<Hand>();

		anim.SetTrigger(Natural);
	}

	void Update()
	{
		if (hand.controller.GetPressDown(EVRButtonId.k_EButton_Grip))
			RockPose();
		else
			if (hand.controller.GetPressUp(EVRButtonId.k_EButton_Grip))
				NaturalPose();
	}

	public void IdlePose()
	{
		anim.SetTrigger(Idle);
	}

	public void GrabSmallPose()
	{
		anim.SetTrigger(GrabSmall);
	}

	public void GrabLargePose()
	{
		anim.SetTrigger(GrabLarge);
	}

	public void PointPose()
	{
		anim.SetTrigger(Point);
	}

	public void FistPose()
	{
		anim.SetTrigger(Fist);
	}

	public void NaturalPose()
	{
		anim.SetTrigger(Natural);
	}

	public void RockPose()
	{
		anim.SetTrigger(Rock);
	}
}
