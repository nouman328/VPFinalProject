//  
//        RTP Uni & Multicast Voice Conferencing Sample Using RTP_VOIP_Library 
//             Copyright (C) 2008 FADI Abdelqader www.SocketCoder.Com
//        Visit www.SocketCoder.Com For More Free Tutorials and Examples
//                     R.0.2 (Free Open Source Tutorial)
//
//   Note: You have to install Microsoft DirectX 9.0 Please Check Microsoft.Com
//
//   References:
//   RTP - http://www.ietf.org/rfc/rfc1889.txt
//   G.711 - http://www.itu.int/rec/T-REC-G.711-198811-I/en
//   DirectSound: http://en.wikipedia.org/wiki/DirectSound
//
//   Using:
//   RTP: http://www.codeproject.com/KB/IP/Using_RTP_in_Multicasting.aspx
//   RTP Library: http://research.microsoft.com/conferencexp/
//   G.711 Library: http://www.codeproject.com/KB/security/g711audio.aspx
//   Directsound Library: http://msdn.microsoft.com/en-us/library/bb219822(VS.85).aspx

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

// RTP VOIP Library Quick Guide:
// - - - - - - - - - - - To Make P2P Call - - - - - - - - - - - - - - -
//  (1) JoinRTPSession(this,YourName,IP Address Uni/Multi,Port Number,false,0)
//  (2) StartTalking()
// - - - - - - - - - - - To Make Client/Server Call - - - - - - - - - - 
//      - Server Part:
//  (1) JoinRTPSession(this,ReflectorServiceName,Multicast IP,Port Number,true,Server_Listening_Port)
//      - Client Part: 
//      For Talker:
//  (1) TalkTo_RTP_ReflectorService(Reflector IP Address Unicast IP,Reflector Server Port, UserName)
//      For Listener: 
//  (1) JoinRTPSession(this,YourName,IP Address Uni/Multi,Port Number,false,0)
// - - - - - - - - - - - Other Methods - - - - - -  - - - - - - - - - - 
//  (1) LeaveRTPSession()     


namespace RTP_VOIP_Library_Sample
{
    public partial class RTP_VOIP_Library_Sample : Form
    {
        public RTP_VOIP_Library_Sample()
        {
            InitializeComponent();
        }

        string get_time()
        {
            return " at " + DateTime.Now.ToString();
        }
        void RefClientAddMSG(string MSG)
        {
            listBox_RefClientEvent.Items.Add(MSG);
            listBox_RefClientEvent.SelectedIndex = listBox_RefClientEvent.Items.Count - 1;
        }
        void AddP2PEventMSG(string MSG)
        {
            Talkers_listBox.Items.Add(MSG);
            Talkers_listBox.SelectedIndex = Talkers_listBox.Items.Count - 1;
        }
        SocketCoder_RTP_Voice_Library.RTP_VOIP_Library rtp = new SocketCoder_RTP_Voice_Library.RTP_VOIP_Library();
        SocketCoder_RTP_Voice_Library.RTP_VOIP_Library rtp_server = new SocketCoder_RTP_Voice_Library.RTP_VOIP_Library();

        private void Join_button_Click_1(object sender, EventArgs e)
        {
            Join_button.Enabled = !Join_button.Enabled;
            button_Leave.Enabled = !button_Leave.Enabled;
            button_RTPTalk.Enabled = !button_RTPTalk.Enabled;

            // Join an RTP Session and start receiving
            rtp.JoinRTPSession(this, TalkerName_textbox.Text, IP_comboBox.Text,int.Parse(Direct_RTP_Port.Text),false,0);

            AddP2PEventMSG(rtp.LogRtpMessage + get_time());
            

        }

        private void button_Leave_Click_1(object sender, EventArgs e)
        {
            Join_button.Enabled = !Join_button.Enabled;
            button_Leave.Enabled = !button_Leave.Enabled;
            button_RTPTalk.Enabled = false;

            // Leave RTP Session and unhook all RTP Events
            rtp.LeaveRTPSession();
            AddP2PEventMSG("you have left the voice session" + get_time());
        }

        private void button_Start_Ref_Click(object sender, EventArgs e)
        {
           
            button_Start_Ref.Enabled = !button_Start_Ref.Enabled;
            button_Stop_Ref.Enabled = !button_Stop_Ref.Enabled;

            // Join an RTP Session as Reflector Server
            rtp_server.JoinRTPSession(this,Environment.MachineName +  " Reflector Server", RefServ_TxtIP.Text, int.Parse(RefServ_ListenPort.Text), true,int.Parse(RefServ_SendingPort.Text));

        }

        private void button_Stop_Ref_Click(object sender, EventArgs e)
        {
            button_Start_Ref.Enabled = !button_Start_Ref.Enabled;
            button_Stop_Ref.Enabled = !button_Stop_Ref.Enabled;

            // Leave RTP Session and unhook all RTP Events
            rtp_server.LeaveRTPSession();
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = !button2.Enabled;
            button_CloseSession.Enabled = true;

            // Join an RTP Session and start receiving
            rtp.JoinRTPSession(this, textBox_RefUserName.Text, Multicast_Group.Text, int.Parse(textBox_RefClientListenPort.Text), false, 0);

            RefClientAddMSG(rtp.LogRtpMessage);                     
            
        }

        private void button_Conncet_Click(object sender, EventArgs e)
        {
            rtp.SetVoiceDevices(this); // Set SoundCard Device to DirectSound Using the recommended settings 
            rtp.RtpStartTalking(); // Start Talking to other Peer Directly Unicast or Multicast
        }


        private void button_Conncet_Click_1(object sender, EventArgs e)
        {
            button_Conncet.Enabled = !button_Conncet.Enabled;
            button_CloseSession.Enabled = true;

            // Talking to Reflector Server By Sending The Voice to Unicast IP 
            // then the reflector server  will reflect it to specific multicast group
            rtp.TalkTo_RTP_ReflectorService(this,textBox_RefIP.Text,int.Parse(textBox_RefPort.Text), textBox_RefUserName.Text);

            RefClientAddMSG("Recording & Sending..." + get_time());
        }

        private void button_CloseSession_Click(object sender, EventArgs e)
        {
            button2.Enabled = true;
            button_Conncet.Enabled = true;
            button_CloseSession.Enabled = button_Conncet.Enabled & button_Start_Ref.Enabled ? false : true;

            
            rtp.LeaveRTPSession();// Leave RTP Session and unhook all RTP Events

            RefClientAddMSG("You have left the voice session" + get_time());
        }

        private void RTPVoiceConferencingSample_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Leave RTP Session and unhook all RTP Events
            rtp.LeaveRTPSession();
        }

        // UDP
         SocketCoder_RTP_Voice_Library.UDP_VOIP_Library.UDP_ReflectorServer RefServer = null;
         SocketCoder_RTP_Voice_Library.UDP_VOIP_Library.UDP_ClientAndP2P UniUDPCleintTalker = null;
         SocketCoder_RTP_Voice_Library.UDP_VOIP_Library.UDP_ClientAndP2P UniUDPCleintJoiner = null;
         SocketCoder_RTP_Voice_Library.UDP_VOIP_Library.UDP_ClientAndP2P UniUDPCleintHearer = null;

        
          private void button_TalkUDP_Click(object sender, EventArgs e)
           {
              button_TalkUDP.Enabled = !button_TalkUDP.Enabled;
              button_UDPDropCall.Enabled = true;

              UniUDPCleintTalker = new SocketCoder_RTP_Voice_Library.UDP_VOIP_Library.UDP_ClientAndP2P(this, Text_UDPPeerIP.Text, int.Parse(textBox_UDPPort.Text), int.Parse(textBox_UDPPort.Text), true);

              UniUDPCleintTalker.StartUdpVoiceCallJustTalking();
           }


           private void button_StartUDPRefServer_Click(object sender, EventArgs e)
           {
              if (!button_UDPDropCall.Enabled)
              {
                  button_TalkUDP.Enabled = false;
                  button_Hear.Enabled = false;
                  button_JoinUDPMultiGroup.Enabled = false;


                  button_StartUDPRefServer.Enabled = !button_StartUDPRefServer.Enabled;
                  button_StopUDPRefServer.Enabled = !button_StopUDPRefServer.Enabled;
                  RefServer = new SocketCoder_RTP_Voice_Library.UDP_VOIP_Library.UDP_ReflectorServer(textBox_UDPRefSendtoMCGroup.Text, int.Parse(textBox_UDPServerListeningPort.Text), int.Parse(textBox_UDPServerListeningPort.Text));
                  RefServer.StartUdpServer(); // Start Reflector Service - Client(Uni)->Server(Multi)->Clients
              }
              else  MessageBox.Show("Reflector should be on dedicated server", "Reflector should be dedicated!", MessageBoxButtons.OK, MessageBoxIcon.Error);
           }

           private void button_StopUDPRefServer_Click(object sender, EventArgs e)
           {
               button_StartUDPRefServer.Enabled = !button_StartUDPRefServer.Enabled;
               button_StopUDPRefServer.Enabled = !button_StopUDPRefServer.Enabled;
               RefServer.StopUdpServer();// Stop Reflector Service

               button_TalkUDP.Enabled = true;
               button_Hear.Enabled = true;
               button_JoinUDPMultiGroup.Enabled = true;
           }

           private void button_Hear_Click(object sender, EventArgs e)
           {
               button_Hear.Enabled = !button_Hear.Enabled;
               button_JoinUDPMultiGroup.Enabled = !button_JoinUDPMultiGroup.Enabled;
               button_UDPDropCall.Enabled = true;

               UniUDPCleintHearer = new SocketCoder_RTP_Voice_Library.UDP_VOIP_Library.UDP_ClientAndP2P(this, Text_UDPPeerIP.Text, int.Parse(textBox_UDPPort.Text), int.Parse(textBox_UDPPort.Text), false);

              UniUDPCleintHearer.StartUDPVoiceReceiver();

                    
           }

           private void button_JoinUDPMultiGroup_Click(object sender, EventArgs e)
           {
               button_Hear.Enabled = !button_Hear.Enabled;
               button_JoinUDPMultiGroup.Enabled = !button_JoinUDPMultiGroup.Enabled;
               button_UDPDropCall.Enabled =true;


               UniUDPCleintJoiner= new SocketCoder_RTP_Voice_Library.UDP_VOIP_Library.UDP_ClientAndP2P(this, Text_UDPJoinToMCGroup.Text, int.Parse(textBox_UDPPort.Text), int.Parse(textBox_UDPPort.Text), true);
               UniUDPCleintJoiner.StartUDPVoiceReceiver();


           }

           private void button_UDPDropCall_Click(object sender, EventArgs e)
           {
               button_Hear.Enabled = true;
               button_JoinUDPMultiGroup.Enabled = true;
               button_UDPDropCall.Enabled = false;
               button_TalkUDP.Enabled = true;

               if (UniUDPCleintTalker !=null) UniUDPCleintTalker.EndUdpCall();
               if (UniUDPCleintHearer != null) UniUDPCleintHearer.EndUdpCall();
               if (UniUDPCleintJoiner != null) UniUDPCleintJoiner.EndUdpCall();
     

           }

           private void button_RTPTalk_Click(object sender, EventArgs e)
           {
               button_RTPTalk.Enabled = !button_RTPTalk.Enabled;

               // Start Talking and Sends Directly to the same joined group or to other peer.
               rtp.RtpStartTalking();
               AddP2PEventMSG("Recording & Sending..." + get_time());
           }

           private void RTP_VOIP_Library_Sample_Load(object sender, EventArgs e)
           {
               TalkerName_textbox.Text = Environment.UserName;
               textBox_RefUserName.Text = Environment.UserName;


           }

    }
}
