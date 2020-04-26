using UnityEngine;
using System;
using System.Collections;
using TreeSharpPlus;
using RootMotion.FinalIK;

public class BehaviorTree : MonoBehaviour
{	    public GameObject Wanderer;     
		public GameObject Friend; 
		public GameObject Police;
		private BehaviorAgent behaviorAgent;
		public Transform second;
		public Transform final;
		public Transform ph;
		public Transform wonder;
		public Transform jaildoor;
		public Transform one;
		public Transform two;
		public Transform three;
		public Transform four;
		public Transform door2;
		public Transform getout;
		public InteractionObject phone;
		public InteractionObject nothing ;
	public FullBodyBipedEffector eff1;
	public FullBodyBipedEffector left;
	
	
public FullBodyBipedEffector eff2;
    public InteractionObject obj;
	public InteractionObject obj2;
	void Start ()

	{
		
		behaviorAgent = new BehaviorAgent (this.ConversationTree ());

		BehaviorManager.Instance.Register (behaviorAgent);

		behaviorAgent.StartBehavior ();

	}

    protected Node Converse()

    {

        return

            new DecoratorPrintResult(

                new Sequence( 

                Wanderer.GetComponent<BehaviorMecanim>().ST_PlayGesture("SURPRISED", AnimationLayer.Hand, 3000),

                Friend.GetComponent<BehaviorMecanim>().ST_PlayGesture("WAVE", AnimationLayer.Hand, 3000),

                Wanderer.GetComponent<BehaviorMecanim>().ST_PlayGesture("CLAP", AnimationLayer.Hand, 3000),

                Friend.GetComponent<BehaviorMecanim>().ST_PlayGesture("POINTING", AnimationLayer.Hand, 3000))
				)
				;

    }
		

public Node EyeContact(Val<Vector3> WanderPos, Val<Vector3> FriendPos)
    {

        Vector3 height = new Vector3(0.0f, 1.85f, 0.0f);

        Val<Vector3> WanderHead = Val.V(() => WanderPos.Value + height);

        Val<Vector3> Friendhead = Val.V(() => FriendPos.Value + height);



        return new SequenceParallel(

            Friend.GetComponent<BehaviorMecanim>().Node_HeadLook(Friendhead),

            Wanderer.GetComponent<BehaviorMecanim>().Node_HeadLook(WanderHead));

    }
		
protected Node EyeContactAndConverse( Val<Vector3> WandererPos, Val<Vector3> FriendPos)     {     
			return new Race(             
			this.EyeContact(WandererPos, FriendPos));
			   
			}
	protected Node ApproachAndWait(GameObject x, Transform target)
		{

		Val<Vector3> position = Val.V (() => target.position);

		return new Sequence(

             x.GetComponent<BehaviorMecanim>().Node_GoTo(position)
			 );



	}
protected Node PoliceWalk(Transform one, Transform two, Transform three, Transform four)
{
	Val<Vector3> o = Val.V (() => one.position);
	Val<Vector3> tw = Val.V (() => two.position);
	Val<Vector3> th = Val.V (() => three.position);
	Val<Vector3> f = Val.V (() => four.position);
	return new DecoratorLoop (
	new Sequence(
			Police.GetComponent<BehaviorMecanim>().Node_GoTo(o),
			Police.GetComponent<BehaviorMecanim>().Node_GoTo(tw),
			Police.GetComponent<BehaviorMecanim>().Node_GoTo(th),
			Police.GetComponent<BehaviorMecanim>().Node_GoTo(f)));
				
	
}
			
	protected Node Approach( Val<Vector3> WandererPos, Val<Vector3> FriendPos, Transform target)    
		{        
			Val <Vector3> p1pos = Val.V(() => target.position);
			return new Sequence(          
			// Approach at distance 1.0f          
			Friend.GetComponent<BehaviorMecanim>().Node_GoTo(p1pos),
			Wanderer.GetComponent<BehaviorMecanim>().Node_GoTo(p1pos)
			);    
		}
		
	protected Node Orient(Val<Vector3> WandererPos, Val<Vector3> FriendPos, Transform target)
	{
		return new SequenceParallel(            
			Friend.GetComponent<BehaviorMecanim>().Node_OrientTowards(WandererPos), 	
			Wanderer.GetComponent<BehaviorMecanim>().Node_OrientTowards(FriendPos));
	}
	protected Node Phones(){
		return new Sequence(
			Friend.GetComponent<BehaviorMecanim>().Node_StartInteraction(left, phone),
			//Friend.GetComponent<BehaviorMecanim>().Node_StartInteraction(left, obj2),
			new LeafWait(1000),
			      Friend.GetComponent<BehaviorMecanim>().ST_PlayBodyGesture("TALKING ON PHONE", 3000),

                          Friend.GetComponent<BehaviorMecanim>().Node_StopInteraction(left),
			Friend.GetComponent<BehaviorMecanim>().ST_PlayGesture("POINTING", AnimationLayer.Hand, 3000)
			
			);
		
	}
				
				
	protected Node ST_ShakeHands( Val<Vector3> WandererPos, Val<Vector3> FriendPos, Val<InteractionObject> obj, Val<FullBodyBipedEffector> effector1, Val<FullBodyBipedEffector> effector2)

    {

        return new Sequence(
			
            Friend.GetComponent<BehaviorMecanim>().Node_OrientTowards(WandererPos),                
			Wanderer.GetComponent<BehaviorMecanim>().Node_OrientTowards(FriendPos),

            Friend.GetComponent<BehaviorMecanim>().Node_StartInteraction(effector1, obj),
			
			new LeafWait(2000),

            Wanderer.GetComponent<BehaviorMecanim>().Node_StartInteraction(effector2, obj),
			
			new LeafWait(2000),
			
			Wanderer.GetComponent<BehaviorMecanim>().Node_StopInteraction(effector2),
			
			Friend.GetComponent<BehaviorMecanim>().Node_StopInteraction(effector1)

            //new LeafWait(1000)
			);

    }
	
	protected Node dooropen()
	{

		return new Sequence(Friend.GetComponent<BehaviorMecanim>().ST_PlayGesture("POINTING", AnimationLayer.Hand, 3000));

		
	}
	
	protected Node ST_EndTree(){
		return new DecoratorLoop (
			new Sequence( new LeafWait(2000))
			);
	}

	
	protected Node Wonder(){
		return new Sequence (Friend.GetComponent<BehaviorMecanim>().ST_PlayGesture("THINK", AnimationLayer.Hand, 3000));
	}
		
	public Node ConversationTree()     { 
					Val<Vector3> WandererPos = Val.V(() => Wanderer.transform.position);
					Val<Vector3> FriendPos = Val.V(() => Friend.transform.position); 
 
        return new SequenceParallel(            
		this.PoliceWalk(one,two,three,four),
		new Sequence(
		this.ApproachAndWait(Friend,second),
		this.ApproachAndWait(Friend,wonder),
		this.Wonder(),
		this.dooropen(),
		this.ApproachAndWait(Friend,ph),
		this.Phones(),
		this.ApproachAndWait(Friend,door2),
		this.ApproachAndWait(Friend,jaildoor),
		this.dooropen(),
		this.Approach(WandererPos, FriendPos,final),
		this.Orient(WandererPos, FriendPos, final),
		this.Converse(),
		new SequenceParallel(
							this.ApproachAndWait(Friend,getout),
							this.ApproachAndWait(Wanderer,getout)
							),
		this.ST_ShakeHands(WandererPos,FriendPos,obj, eff1, eff2),
		this.ST_EndTree())
		);    
		}
    // Update is called once per frame
    void Update()
    {

    }
}
