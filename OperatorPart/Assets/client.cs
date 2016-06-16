using UnityEngine;  
using System.Collections;  

public class client : MonoBehaviour {  
	//要连接的服务器地址  
	string IP = "127.0.0.1";  
	//string IP = "192.168.0.105"; 
	//string IP = "192.168.31.179";
	//string IP = "";
	//string Intro="1.点击";
	//要连接的端口  
	int Port = 10000;  

	string Message="";
	public NetworkView nView;
	//Vector2 Sc;



	// Use this for initialization  
	void Start () {  

		nView = GetComponent<NetworkView> ();

	}  
	
	void OnGUI(){  
		



		//端类型的状态  
		switch(Network.peerType){  
			//禁止客户端连接运行, 服务器未初始化  
		case NetworkPeerType.Disconnected:  
			StartConnect();  
			break;  
			//运行于服务器端  
		case NetworkPeerType.Server:  
			break;  
			//运行于客户端  
		case NetworkPeerType.Client:
			OnClient();
			break;  
			//正在尝试连接到服务器  
		case NetworkPeerType.Connecting:  
			break;  
		}  
	}  
	
	
	void StartConnect(){
		float screenwidth = Screen.width;
		float screenheight = Screen.height;
		float recwidth = screenwidth * (2/3f);
		float recborder = screenwidth *(1/6f);
		float buttonheight = screenheight * (1/6f);
		float text_height = screenheight * (1/10f);
		float boxheight = screenheight * (1/6f);
		float textarea_height = screenheight * (1/6f);
		float vertical_border = screenheight * (1 / 9f);



		//GUIStyle fontstyle = new GUIStyle ();
		//fontstyle.fontSize = 10;
		GUI.skin.button.fontSize = 100;
		GUI.skin.label.fontSize = 60;
		GUI.skin.textArea.fontSize = 100;

		Debug.Log(screenwidth + ":" + screenheight);
		GUILayout.BeginArea (new Rect(recborder,vertical_border,recwidth,screenheight));
		GUILayout.BeginVertical ("",GUILayout.Width(recwidth));
		GUILayout.Label("1.两台手机连接到同一稳定  wifi\r\n2.查看放入头盔的手机的IP地址，将IP地址输入工作人员所持手机，并点击“连接”按钮",GUILayout.Height(3*text_height));
		//GUILayout.Label("2.查看放入头盔的手机的IP地址，将IP地址输入工作人员所持手机，并点击“连接”按钮",GUILayout.Height(text_height));

		//GUILayout.Box(IP,GUILayout.Height(boxheight));  
		//文本框  
		IP = GUILayout.TextArea(IP,GUILayout.Height(textarea_height)); 
		if (GUILayout.Button("连  接",GUILayout.Height(buttonheight))){  
			//Application.LoadLevel("scene_1");
			NetworkConnectionError error = Network.Connect(IP,Port);  
			Debug.Log("连接状态"+error);  
		}  
		GUILayout.EndVertical ();
		GUILayout.EndArea ();
	}  

	void OnClient(){  
		float screenwidth = Screen.width;
		float screenheight = Screen.height;
		float recwidth = screenwidth * (2/3f);
		float recborder = screenwidth *(1/6f);
		float buttonheight = screenheight * (1/6f);
		float text_height = screenheight * (1/10f);
		float boxheight = screenheight * (1/6f);
		float textarea_height = screenheight * (1/6f);

		float vertical_border = screenheight * (1 / 9f);

		GUI.skin.button.fontSize = 100;
		GUI.skin.label.fontSize = 60;
		//GUI.skin.textArea.fontSize = 50;

		GUILayout.BeginArea (new Rect(recborder,vertical_border,recwidth,screenheight));
		GUILayout.BeginVertical ("",GUILayout.Width(recwidth));
		GUILayout.Label("连接成功，开始操作!\r\n\r\n重新开始“——回到初始场景\r\n切换场景“——播放砍头场景",GUILayout.Height(3*text_height));
		if (GUILayout.Button ("重新开始",GUILayout.Height(buttonheight))) {
			Message="1";
			nView.RPC("ReciveMessage", RPCMode.All, Message); 
		}
		if (GUILayout.Button("切换场景",GUILayout.Height(buttonheight))){  
			//发送给接收的函数, 模式为全部, 参数为信息  
			Message="2";
			nView.RPC("ReciveMessage", RPCMode.All, Message);  
		}  
		GUILayout.EndVertical ();
		GUILayout.EndArea ();
		 
	}
	
	[RPC]
	void ReciveMessage(string msg){
		Message = msg;
	}
	



	
	// Update is called once per frame  
	void Update () {  
		
	}  


}  
