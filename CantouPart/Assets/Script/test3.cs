using UnityEngine;
using System.Collections;
//Vector3.right (1,0,0) x axis
//Vector3.forward(0,0,1) z axis

public class test3 : MonoBehaviour {
	private Rigidbody myrigidbody;
	private Transform mytransform;

	private int count_wall = 0;
	private int count_bounce = 0;//与地面反弹次数

	private Vector3 velocity;
	private float velocity_magnitude = 0;

	//private Vector3 sp_vec = new Vector3 (0f,0f,1f);//水平方向向量
	private Vector3 sp_vec = new Vector3 (0.2f,0f,0.8f);//水平方向向量
	private Vector3 sz_vec = new Vector3 (0f,-1f,0f);//竖直向下方向向量

	private float force = 0f;
	private float force1=100f;//初始水平方向力大小
	private float force2=20f;//水平反弹一次后水平方向力大小
	private float sz_force=10f;//重力竖直方向力大小

	public AudioSource audio_wallcrack;//撞墙音效
	public AudioSource audio_rollground1;
	private int count_audioground=0;
	private bool isrolling=false;


	// Use this for initialization
	void Start () {
		audio_rollground1.loop= true;
		myrigidbody = this.GetComponent<Rigidbody> ();
		force = force1;
	}

	// Update is called once per frame
	void Update () {
		velocity_magnitude= myrigidbody.velocity.magnitude;
		Debug.Log (velocity_magnitude+":"+count_audioground);
		myrigidbody.AddForce (sp_vec * force);
		myrigidbody.AddForce (sz_vec * sz_force);

		if (isrolling&&myrigidbody.velocity.magnitude>0.5) {
			Debug.Log ("play!" + myrigidbody.velocity.magnitude);
			audio_rollground1.Play ();
			isrolling = false;

		}
		if (myrigidbody.velocity.magnitude < 0.5) {
			audio_rollground1.loop= false;
		}
	}

	void FixedUpdate(){
	}
		
	void OnCollisionEnter(Collision e){
		if (e.gameObject.tag=="wall") {
			Debug.Log ("wall!!!");
			sp_vec = -sp_vec;//如果撞上墙，水平方向的力反向
			force = force2;
			count_wall++;
			if (count_wall <= 1) {
				Debug.Log ("wall play");
				audio_wallcrack.Play ();
			}
		}
		if (e.transform.tag.CompareTo ("ground") == 0) {
			Debug.Log ("ground!!!");
			sz_vec=-sp_vec;//如果是第一次落地，反向弹起
			count_bounce++;
			if (count_bounce == 1) {
				isrolling = true;
			}

			if (count_bounce > 1) {//第二次及以后不再施加竖直方向的力，自由滚动
				sz_vec = new Vector3 (0f, 0f, 0f);
				sp_vec = new Vector3 (0f, 0f, 0f);

				/*if (count_audioground == 2) {
					audio_rollground1.Play ();
				}*/

			}
		}
	}
}
