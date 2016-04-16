//  
//               RTP Uni & Multicast VOIP Library 
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
using System.Text;
using System.Collections.Generic;

// Managed Networking Namespaces
using System.Net;
using System.IO;
using System.Threading;
using System.Net.Sockets;

// RTP Namespaces 
using MSR.LST;
using MSR.LST.Net.Rtp;

// G.711 Namespace 
using G711Audio;

// DirectSound Namespace
using Microsoft.DirectX.DirectSound;

namespace SocketCoder_RTP_Voice_Library
{
    class RTP_VOIP_Library
    {
        // Declarations
        #region Local Variables
        public string LogRtpMessage = null; // Set Received Log Messages
        private bool bStop;//Flag to end the Start and Receive threads.
        private bool Reflector_Service_On = false; // indicate to reflect or play the received voice 
        private bool IsReflectorClient = false; // indicate to send to reflector server or to P2P 
        private bool IsTheFirstTime = true;
        private int bufferSize; // the size of the voice buffer
        private Thread th_senderThread; // Taking & Sending Thread
        private Thread th_ref; // Talking to Reflector Server Thread
        #endregion Local Variables
        #region RTP Classes
        private static IPEndPoint ep = RtpSession.DefaultEndPoint; // RTP EndPoint
        private static IPEndPoint ep_client_send; // RTP Reflector Client Sending to Server
        private RtpSession rtpSession; //Manages the connection to a multicast address and all the objects related to Rtp
        private RtpSession rtpReflectorServerSenderSession; //Manages the connection to a multicast address and all the objects related to Rtp
        private RtpSession rtpReflectorClientSenderSession; //Manages the connection to a unicast address
        private RtpSender rtpSender;//Sends the data across the network
        #endregion RTP Classes
        #region DirectSound Classes
        private CaptureBufferDescription captureBufferDescription;
        private AutoResetEvent autoResetEvent;
        private Notify notify;
        private WaveFormat waveFormat;
        private Capture capture;
        private CaptureBuffer captureBuffer;
        private Device device;
        private SecondaryBuffer playbackBuffer;
        private BufferDescription playbackBufferDescription;
        #endregion DirectSound Classes
        // Members
        #region RTP Members

        // Hook Rtp events

            // RTPEvents:

            //(A)Exceptions Events:
            //  1- DuplicateCNameDetected
            //  2- FrameOutOfSequence
            //  3- HiddenSocketException
            //  4- InvalidPacket
            //  5- InvalidPacketInFrame
            //  6- NetworkTimeout
            //  7- PacketOutOfSequence
            //  8- RtpParticipantTimeout
            //  9- RtpStreamTimeout

            //(B)Reporting Events:
            //  1-ReceiverReport

            //(C)RTP Participant & Stream Add/Remove Events  
            //  1- RtpParticipantAdded
            //  2- RtpParticipantDataChanged
            //  3- RtpParticipantRemoved
            //  4- RtpStreamRemoved
            //  5- RtpStreamAdded;

        private void HookRtpParticipantEvents()
        {
            // Add Remove Participant Events
            RtpEvents.RtpParticipantAdded += new RtpEvents.RtpParticipantAddedEventHandler(RtpParticipantAdded);
            RtpEvents.RtpParticipantRemoved += new RtpEvents.RtpParticipantRemovedEventHandler(RtpParticipantRemoved);
        }
        private void HookRtpStreamEvents()
        {
            // Add Remove Stream Events
            RtpEvents.RtpStreamAdded += new RtpEvents.RtpStreamAddedEventHandler(RtpStreamAdded);
            RtpEvents.RtpStreamRemoved += new RtpEvents.RtpStreamRemovedEventHandler(RtpStreamRemoved);
        }
                
        // Receive data from network
        private void RtpParticipantAdded(object sender, RtpEvents.RtpParticipantEventArgs ea)
        {   
            LogRtpMessage = ea.RtpParticipant.Name + " has joined the voice session.";
        }

        private void RtpParticipantRemoved(object sender, RtpEvents.RtpParticipantEventArgs ea)
        {
            LogRtpMessage = ea.RtpParticipant.Name + " has left the voice session";
        }

        private void RtpStreamAdded(object sender, RtpEvents.RtpStreamEventArgs ea)
        {
            ea.RtpStream.FrameReceived += new RtpStream.FrameReceivedEventHandler(FrameReceived);
        }

        private void RtpStreamRemoved(object sender, RtpEvents.RtpStreamEventArgs ea)
        {
            ea.RtpStream.FrameReceived -= new RtpStream.FrameReceivedEventHandler(FrameReceived);
        }

        private void FrameReceived(object sender, RtpStream.FrameReceivedEventArgs ea)
        {
            if (!Reflector_Service_On) // indicate whither playing received voice or reflect it to multicast group 
                PlayReceivedVoice(ea.Frame.Buffer);
            else
                rtpSender.Send(ea.Frame.Buffer);

        }

        private void UnhookRtpEvents()
        {
            RtpEvents.RtpParticipantAdded -= new RtpEvents.RtpParticipantAddedEventHandler(RtpParticipantAdded);
            RtpEvents.RtpParticipantRemoved -= new RtpEvents.RtpParticipantRemovedEventHandler(RtpParticipantRemoved);
            RtpEvents.RtpStreamAdded -= new RtpEvents.RtpStreamAddedEventHandler(RtpStreamAdded);
            RtpEvents.RtpStreamRemoved -= new RtpEvents.RtpStreamRemovedEventHandler(RtpStreamRemoved);
        }

        private void DisposeRtpSession()
        {
            if (rtpSession != null)
            {
                // Clean up all outstanding objects owned by the RtpSession
                rtpSession.Dispose();
                rtpSession = null;
                rtpSender = null;
            }

            if (rtpReflectorClientSenderSession != null && IsReflectorClient)
            {
                // Clean up all outstanding objects owned by the RtpSession
                rtpReflectorClientSenderSession.Dispose();
                rtpReflectorClientSenderSession = null;
                rtpSender = null;
            }

        }

        // RTP Public Members 

        // hook events & Start RtpSession
        public void JoinRTPSession(System.Windows.Forms.Control AppForm_TypeThis, string ParticipatorName, string Peer_IP, int Join_port, bool IsReflectorServer, int ReflectorServerSendingPort)
        {
            Reflector_Service_On = IsReflectorServer; // True(If Server) else False (if Client or P2P Join)
            
            // Hook RTP Sesstion
            HookRtpParticipantEvents();
            HookRtpStreamEvents();
            

            if (IsReflectorServer)
            {
                ep = new IPEndPoint(IPAddress.Parse(Peer_IP), Join_port);
                rtpSession = new RtpSession(ep, new RtpParticipant(ParticipatorName + " Server Listener", "Listener " + ParticipatorName), true, true);
                // Sending Session
                ep_client_send = new IPEndPoint(IPAddress.Parse(Peer_IP), ReflectorServerSendingPort);
                rtpReflectorServerSenderSession = new RtpSession(ep_client_send, new RtpParticipant(ParticipatorName + " VOIP Talker ","Talker " + ParticipatorName), true, true);
                rtpSender = rtpReflectorServerSenderSession.CreateRtpSender(ParticipatorName + " VOIP Reflector ", PayloadType.dynamicAudio, null);
                // Just Clear & Reset The RTP Session At the first time
                if (IsTheFirstTime)
                { IsTheFirstTime = false; LeaveRTPSession(); JoinRTPSession(AppForm_TypeThis, ParticipatorName, Peer_IP, Join_port, IsReflectorServer, ReflectorServerSendingPort); }
            }
            else
            {               
                ep = new IPEndPoint(IPAddress.Parse(Peer_IP), Join_port);
                rtpSession = new RtpSession(ep, new RtpParticipant(ParticipatorName + " VOIP Listener", ParticipatorName + "(Listener)"), true, true);
                SetVoiceDevices(AppForm_TypeThis);
                rtpSender = rtpSession.CreateRtpSender("VOIP Listener", PayloadType.dynamicAudio, null);
            }

            
        }

        // Unhook events, dispose RtpSession
        public void LeaveRTPSession()
        {
            StopTalking();
            UnhookRtpEvents();
            DisposeRtpSession();
        }

        #endregion RTP Members     
        #region Recording & Playing Using DirectSound
        private void StartRecordAndSend() // Send Recorded Voice
        {
            try
            {
                captureBuffer = new CaptureBuffer(captureBufferDescription, capture); // Set Buffer Size,Voice Recording Format & Input Voice Device 
                SetBufferEvents(); // Set the events Positions to Send While Recording
                int halfBuffer = bufferSize / 2; // Take the half buffer size
                captureBuffer.Start(true); // start capturing
                bool readFirstBufferPart = true; // to know which part has been filled (the buufer has been divided into tow parts)  
                int offset = 0; // at point 0
                MemoryStream memStream = new MemoryStream(halfBuffer); // set the half buffer size to the memory stream
                bStop = false;

                while (!bStop) // Looping until bStop=true Set by the talker 
                {

                    //WaitOne() Blocks the current thread until the current WaitHandle receives a signal
                    //WaitHandle("Encapsulates operating system–specific objects that wait for exclusive access to shared resources")
                    autoResetEvent.WaitOne(); 

                    memStream.Seek(0, SeekOrigin.Begin); //Sets the position within the current stream to 0 
                    captureBuffer.Read(offset, memStream, halfBuffer, LockFlag.None); // capturing and set to MemoryStream
                    readFirstBufferPart = !readFirstBufferPart; // reflecting the boolean value to set the new comming buffer to the other part
                    offset = readFirstBufferPart ? 0 : halfBuffer; // if readFirstBufferPart set to true then set the offset to 0 else set the offset to the half buffer 

                    byte[] dataToWrite = ALawEncoder.ALawEncode(memStream.GetBuffer()); // G.711 Encoding, Compress to 50%

                    rtpSender.Send(dataToWrite); // Sending the compressed voice across RTP 

                }
            }
            catch (Exception) { }

            finally
            {
                captureBuffer.Stop();
            }


        }

        // Start G.711 decoding and Palying Received Voice
        private void PlayReceivedVoice(byte[] VoiceBuffer)
        {

            try
            {
                     //Receive data.
                    byte[] byteData = VoiceBuffer;

                    //G711 compresses the data by 50%, so we allocate a buffer of double
                    //the size to store the decompressed data.
                    byte[] byteDecodedData = new byte[byteData.Length * 2];

                    //Decompress data using G.711
                    ALawDecoder.ALawDecode(byteData, out byteDecodedData);
  
                   //Play the data received to the user.
                    playbackBuffer = new SecondaryBuffer(playbackBufferDescription, device);
                    playbackBuffer.Write(0, byteDecodedData, LockFlag.None); // 0= Starting Point offset  
                    playbackBuffer.Play(0, BufferPlayFlags.Default); // 0 = The Priority of Sound for hardware that mixing the voice resources  
            }
            catch (Exception){}
            finally
            {
            }


        }

        protected void SetBufferEvents()
        {
            // Goal: To Send While Recording
            // To Set The Buffer Size to get 200 milliseconds and divide it in half, 
            // so that when the first half is filled the data can be used to send, 
            // while the second half of the buffer is being filled with PCM Data

            try
            {
                autoResetEvent = new AutoResetEvent(false); // To wait for notifications
                notify = new Notify(captureBuffer); // The number of bytes that can trigger the notification event 

                // the first half
                BufferPositionNotify bufferPositionNotify1 = new BufferPositionNotify(); // to describe the notification position
                bufferPositionNotify1.Offset = bufferSize / 2 - 1; // (= At the Half of The Buffer) to know where the notify event will trigger
                bufferPositionNotify1.EventNotifyHandle = autoResetEvent.SafeWaitHandle.DangerousGetHandle(); // Set The Event that will trigger after the offset reached

                // the last half
                BufferPositionNotify bufferPositionNotify2 = new BufferPositionNotify();
                bufferPositionNotify2.Offset = bufferSize - 1; // (= At The Last Buffer)
                bufferPositionNotify2.EventNotifyHandle = autoResetEvent.SafeWaitHandle.DangerousGetHandle();

                notify.SetNotificationPositions(new BufferPositionNotify[] { bufferPositionNotify1, bufferPositionNotify2 }); // The Tow Positions (First & Last) 
            }
            catch (Exception) { }
        }

        // DirectSound Public Members

        public void SetVoiceDevices(int deviceID, short channels, System.Windows.Forms.Control AppForm_TypeThis, short bitsPerSample, int samplesPerSecond)
        {
            // Installization Voice Devices
            device = new Device(); // Sound Input Device 
            device.SetCooperativeLevel(AppForm_TypeThis, CooperativeLevel.Normal); // Set The Application Form and Priority
            CaptureDevicesCollection captureDeviceCollection = new CaptureDevicesCollection(); // To Get Available Devices (Input Sound Card) 
            DeviceInformation deviceInfo = captureDeviceCollection[deviceID]; // Set Device Number
            capture = new Capture(deviceInfo.DriverGuid); // Get The Selected Device Driver Information

            //Set up the wave format to be captured.
            waveFormat = new WaveFormat(); // Wave Format declaration 
            waveFormat.Channels = channels; // Channels  (2 if Stereo)
            waveFormat.FormatTag = WaveFormatTag.Pcm; // PCM - Pulse Code Modulation
            waveFormat.SamplesPerSecond = samplesPerSecond; // The Number of Samples Peer One Second
            waveFormat.BitsPerSample = bitsPerSample; // The Number of bits for each sample
            waveFormat.BlockAlign = (short)(channels * (bitsPerSample / (short)8)); // Minimum atomic unit of data in one byte, Ex: 1 * (16/8) = 2 bits 
            waveFormat.AverageBytesPerSecond = waveFormat.BlockAlign * samplesPerSecond; // required Bytes-Peer-Second Ex. 22050*2= 44100
            captureBufferDescription = new CaptureBufferDescription();
            captureBufferDescription.BufferBytes = waveFormat.AverageBytesPerSecond / 5; //Ex. 200 milliseconds of PCM data = 8820 Bytes (In Record)
            captureBufferDescription.Format = waveFormat; // Using Wave Format

            // Playback
            playbackBufferDescription = new BufferDescription();
            playbackBufferDescription.BufferBytes = waveFormat.AverageBytesPerSecond / 5;  //Ex. 200 milliseconds of PCM data = 8820 Bytes (In Playback)
            playbackBufferDescription.Format = waveFormat;
            playbackBuffer = new SecondaryBuffer(playbackBufferDescription, device);
            bufferSize = captureBufferDescription.BufferBytes;
        }

        public void SetVoiceDevices(System.Windows.Forms.Control AppForm_TypeThis)
        {
            // Use The Recommended settings For Sound Devices
            SetVoiceDevices(
                0, // Device Number (First Device) 
                1, // Channels      (2 if Stereo)
                AppForm_TypeThis, // Application Form Pointer 
                16, // BitsPerSample
                22050); // SamplesPerSecond  
        }

        // Start Recording , G.711 encoding and then Return the voice as byte array
        public void RtpStartTalking()
        {
            try
            {
                // Recording & Sending Thread
                th_senderThread = new Thread(new ThreadStart(StartRecordAndSend));
                th_senderThread.IsBackground = true;
                th_senderThread.Start();

            }
            catch (Exception) { }
        }

        private void StopTalking()
        {
            bStop = true;
            try { th_senderThread.Abort(); } catch(Exception){}
            try {if(IsReflectorClient) th_ref.Abort(); }catch (Exception) { }
            
        }

        #endregion Recording & Playing Using DirectSound
        #region Reflector Client
        private void ClientTo_ReflectorService()
        {
            try
            {
                // CaptureBufferDescription: Set the Buffer Bytes Number & The Capture Format
                // capture: Set The Sound Card Driver Guide From DirectSound Library 
                captureBuffer = new CaptureBuffer(captureBufferDescription, capture); 
                

                // To Set The Buffer Size to 200 milliseconds and divide it in half, 
                // so that when the first half is filled the data can be used to send, 
                // while the second half of the buffer is being filled with PCM Data
                SetBufferEvents(); 


                int halfBuffer = bufferSize / 2;
                captureBuffer.Start(true);
                bool readFirstBufferPart = true;
                int offset = 0;
                MemoryStream memStream = new MemoryStream(halfBuffer);
                bStop = false;
                while (!bStop)
                {
                    autoResetEvent.WaitOne();
                    memStream.Seek(0, SeekOrigin.Begin);
                    captureBuffer.Read(offset, memStream, halfBuffer, LockFlag.None);
                    readFirstBufferPart = !readFirstBufferPart;
                    offset = readFirstBufferPart ? 0 : halfBuffer;

                    byte[] dataToWrite = ALawEncoder.ALawEncode(memStream.GetBuffer());
                    rtpSender.Send(dataToWrite);
                }
            }
            catch (Exception) { }
            finally
            {
                captureBuffer.Stop();
            }


        }

        // Reflector Service Public Members
        public void TalkTo_RTP_ReflectorService(System.Windows.Forms.Control AppForm_TypeThis,string ServerIP, int ServerPort,string YourName)
        {
            IsReflectorClient = true; //set to true to send to reflector server
            SetVoiceDevices(AppForm_TypeThis);
            // Special IPEndPoint to Send to Reflector Server Session Only
            ep_client_send = new IPEndPoint(IPAddress.Parse(ServerIP), ServerPort);

            HookRtpParticipantEvents();

            // Start New Unicast RTP Session between the client and the reflector server
            rtpReflectorClientSenderSession = new RtpSession(ep_client_send, new RtpParticipant(YourName + " VOIP Talker ", YourName), true, true);

            // CreateRtpSenderFec() to Assign FEC(Forward Error Correction) to the RtpSender
            rtpSender = rtpReflectorClientSenderSession.CreateRtpSender(YourName + " VOIP Talker ", PayloadType.dynamicAudio, null);

            th_ref = new Thread(new ThreadStart(ClientTo_ReflectorService));
            th_ref.IsBackground = true;
            th_ref.Start();

        }

        
        #endregion Reflector Client
        
    }

    #region UDP VOIP Library

    namespace UDP_VOIP_Library
    {
        class UDP_ReflectorServer
        {
            private UdpClient udpRecClient;
            private Socket Sender_socket;
            private bool loopStop;
            private byte[] byteData = new byte[1024];

            public string UdpMulicastIP = "";
            public int UdpClientPort = 0;
            public int UdpServerPort = 0;

            public UDP_ReflectorServer(string ToMulticastIP,int ToClientPort,int MyServerPort)
            {
                UdpMulicastIP = ToMulticastIP;
                UdpClientPort = ToClientPort;
                UdpServerPort = MyServerPort;
            }

            public void StartUdpServer()
            {
                try
                {
                    udpRecClient = new UdpClient(UdpServerPort);

                    Thread senderThread = new Thread(new ThreadStart(UDPReflectorServer));
                    senderThread.IsBackground = true;
                    senderThread.Start();
                }
                catch (Exception )
                {
                }
            }

            public void StopUdpServer()
            {
                try
                {
                    loopStop = true;

                    Sender_socket.Close();

                    udpRecClient.Close();
                }
                catch (Exception) { }

            }

            private void UDPReflectorServer()
            {
                try
                {

                    Sender_socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                    IPEndPoint ipend = new IPEndPoint(IPAddress.Parse(UdpMulicastIP), UdpClientPort); //snd
                    IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0); // rcv

                    loopStop = false;
                    while (!loopStop)
                    {

                        byte[] byteData = udpRecClient.Receive(ref remoteEP);
                        Sender_socket.SendTo(byteData, ipend);

                    }
                }
                catch (Exception)
                {
                }
            }

        }
         class UDP_ClientAndP2P
        {
            #region Sender VOIP Methods
            #region Shared Local Variables
            protected static CaptureBufferDescription captureBufferDescription;
            protected static AutoResetEvent autoResetEvent = null;
            protected static Notify notify = null;
            protected static WaveFormat waveFormat = new WaveFormat();
            protected static Capture capture = null;
            protected static Device device = null;
            protected static CaptureBuffer captureBuffer;
            protected static SecondaryBuffer playbackBuffer;
            protected static BufferDescription playbackBufferDescription = null;
            protected static int bufferSize = 0;
            protected static UdpClient udpClient; // For Voice Receiving 
            protected static Thread receiverThread;// Voice In Thread
            protected static bool Stoploop;
            protected static byte[] byteData = new byte[1024];
            #endregion Shared Local Variables

            public  string UdpToIPAddress = "";
            public  int  UdpToPortAddress = 0;
            public  int  UdpListeningPort = 0;
            public  bool UdpEnabledMulticasting = false;
            public string MyMulticastIP="";
             public UDP_ClientAndP2P(System.Windows.Forms.Control AppForm_TypeThis, string ToUnicastIP_Or_ToMulticastIP, int ToPort, int MyPort, bool UseMulticastingIP)
            {
                UdpToIPAddress = ToUnicastIP_Or_ToMulticastIP;
                UdpToPortAddress = ToPort;
                UdpListeningPort = MyPort;
                UdpEnabledMulticasting = UseMulticastingIP;
                MyMulticastIP = ToUnicastIP_Or_ToMulticastIP;
                SetVoiceDevices(
                0, // Device Number (First Device) 
                1, // Channels      (2 if Stereo)
                AppForm_TypeThis, // Application Form Pointer 
                16, // BitsPerSample
                22050); // SamplesPerSecond  

            }

             public UDP_ClientAndP2P(System.Windows.Forms.Control AppForm_TypeThis, string ToServerIP,string MyMulticastGroup_IP, int ToServerPort, int MyPort)
             {
                 UdpToIPAddress = ToServerIP;
                 UdpToPortAddress = ToServerPort;
                 UdpListeningPort = MyPort;
                 UdpEnabledMulticasting = true;
                 MyMulticastIP = MyMulticastGroup_IP;
                 SetVoiceDevices(
                 0, // Device Number (First Device) 
                 1, // Channels      (2 if Stereo)
                 AppForm_TypeThis, // Application Form Pointer 
                 16, // BitsPerSample
                 22050); // SamplesPerSecond  

             }
            public void SetVoiceDevices(int deviceID, short channels, System.Windows.Forms.Control AppForm_TypeThis, short bitsPerSample, int samplesPerSecond)
            {
                // Installization Voice Devices
                device = new Device(); // Sound Input Device 
                device.SetCooperativeLevel(AppForm_TypeThis, CooperativeLevel.Normal); // Set The Application Form and Priority
                CaptureDevicesCollection captureDeviceCollection = new CaptureDevicesCollection(); // To Get Available Devices (Input Sound Card) 
                DeviceInformation deviceInfo = captureDeviceCollection[deviceID]; // Set Device Number
                capture = new Capture(deviceInfo.DriverGuid); // Get The Selected Device Driver Information

                //Set up the wave format to be captured.
                waveFormat = new WaveFormat(); // Wave Format declaration 
                waveFormat.Channels = channels; // Channels  (2 if Stereo)
                waveFormat.FormatTag = WaveFormatTag.Pcm; // PCM - Pulse Code Modulation
                waveFormat.SamplesPerSecond = samplesPerSecond; // The Number of Samples Peer One Second
                waveFormat.BitsPerSample = bitsPerSample; // The Number of bits for each sample
                waveFormat.BlockAlign = (short)(channels * (bitsPerSample / (short)8)); // Minimum atomic unit of data in one byte, Ex: 1 * (16/8) = 2 bits 
                waveFormat.AverageBytesPerSecond = waveFormat.BlockAlign * samplesPerSecond; // required Bytes-Peer-Second Ex. 22050*2= 44100
                captureBufferDescription = new CaptureBufferDescription();
                captureBufferDescription.BufferBytes = waveFormat.AverageBytesPerSecond / 5; //Ex. 200 milliseconds of PCM data = 8820 Bytes (In Record)
                captureBufferDescription.Format = waveFormat; // Using Wave Format

                // Playback
                playbackBufferDescription = new BufferDescription();
                playbackBufferDescription.BufferBytes = waveFormat.AverageBytesPerSecond / 5;  //Ex. 200 milliseconds of PCM data = 8820 Bytes (In Playback)
                playbackBufferDescription.Format = waveFormat;
                playbackBuffer = new SecondaryBuffer(playbackBufferDescription, device);
                bufferSize = captureBufferDescription.BufferBytes;
            }

             protected void SetBufferEvents()
            {
                // Goal: To Send While Recording
                // To Set The Buffer Size to get 200 milliseconds and divide it in half, 
                // so that when the first half is filled the data can be used to send, 
                // while the second half of the buffer is being filled with PCM Data

                try
                {
                    autoResetEvent = new AutoResetEvent(false); // To wait for notifications
                    notify = new Notify(captureBuffer); // The number of bytes that can trigger the notification event 

                    // the first half
                    BufferPositionNotify bufferPositionNotify1 = new BufferPositionNotify(); // to describe the notification position
                    bufferPositionNotify1.Offset = bufferSize / 2 - 1; // (= At the Half of The Buffer) to know where the notify event will trigger
                    bufferPositionNotify1.EventNotifyHandle = autoResetEvent.SafeWaitHandle.DangerousGetHandle(); // Set The Event that will trigger after the offset reached

                    // the last half
                    BufferPositionNotify bufferPositionNotify2 = new BufferPositionNotify();
                    bufferPositionNotify2.Offset = bufferSize - 1; // (= At The Last Buffer)
                    bufferPositionNotify2.EventNotifyHandle = autoResetEvent.SafeWaitHandle.DangerousGetHandle();

                    notify.SetNotificationPositions(new BufferPositionNotify[] { bufferPositionNotify1, bufferPositionNotify2 }); // The Tow Positions (First & Last) 
                }
                catch (Exception) { }
            }


            private void CapturingAndSending()
            {
                try
                {

                    Socket Sender_socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                    IPEndPoint ipend = new IPEndPoint(IPAddress.Parse(UdpToIPAddress), UdpToPortAddress);

                    captureBuffer = new CaptureBuffer(captureBufferDescription, capture);// Set Buffer Size,Voice Recording Format & Input Voice Device 

                    SetBufferEvents();  // Set the events Positions to Send While Recording

                    int halfBuffer = bufferSize / 2;// Take the half buffer size
                    
                    captureBuffer.Start(true);// start capturing

                    bool readFirstBufferPart = true; // to know which part has been filled (the buufer has been divided into tow parts)  
                    int offset = 0;// at point 0

                    MemoryStream memStream = new MemoryStream(halfBuffer); // set the half buffer size to the memory stream
                    Stoploop = false;

                    while (!Stoploop) // Looping until Stoploop=true Set by the talker 
                    {
                        //WaitOne() Blocks the current thread until the current WaitHandle receives a signal
                        //WaitHandle("Encapsulates operating system–specific objects that wait for exclusive access to shared resources")
                        autoResetEvent.WaitOne();

                        memStream.Seek(0, SeekOrigin.Begin);//Sets the position within the current stream to 0 
                        captureBuffer.Read(offset, memStream, halfBuffer, LockFlag.None); // capturing and set to MemoryStream
                        readFirstBufferPart = !readFirstBufferPart; // reflecting the boolean value to set the new comming buffer to the other part
                        offset = readFirstBufferPart ? 0 : halfBuffer;// if readFirstBufferPart set to true then set the offset to 0 else set the offset to the half buffer 
                        byte[] dataToWrite = ALawEncoder.ALawEncode(memStream.GetBuffer());// G.711 Encoding, Compress to less then 50%
                        Sender_socket.SendTo(dataToWrite, ipend);// Sending the compressed voice across network 

                    }
                }
                catch (Exception)
                {}
                
            }

            public void StartUdpVoiceCallJustTalking()
            {

                Thread senderThread = new Thread(new ThreadStart(CapturingAndSending));
                senderThread.IsBackground = true;
                senderThread.Start();
            }

            public void EndUdpCall()
            {
                Stoploop = true;

                try
                {
                    udpClient.Close();
                    receiverThread.Abort();
                }
                catch (Exception) { }
            }
            #endregion Sender VOIP Methods
            #region  Receiver VOIP Methods

            public void StartUDPVoiceReceiver()
            {
                udpClient = new UdpClient(UdpListeningPort);

                receiverThread = new Thread(new ThreadStart(VOIPReceiver));
                receiverThread.IsBackground = true;
                receiverThread.Start();
            }
            void VOIPReceiver()
            {
                try
                {
                    Stoploop = false;
                    IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, UdpListeningPort);
                    if (UdpEnabledMulticasting) udpClient.JoinMulticastGroup(IPAddress.Parse(MyMulticastIP));
                    while (!Stoploop)
                    {
                        byte[] byteData = udpClient.Receive(ref remoteEP);//Receive data.

                        //G711 compresses the data by 50%, so we allocate a buffer of double
                        //the size to store the decompressed data.
                        byte[] byteDecodedData = new byte[byteData.Length * 2];

                        ALawDecoder.ALawDecode(byteData, out byteDecodedData); // G.711 decoding
                        playbackBuffer = new SecondaryBuffer(playbackBufferDescription, device);
                        playbackBuffer.Write(0, byteDecodedData, LockFlag.None);// 0= Starting Point offset  
                        playbackBuffer.Play(0, BufferPlayFlags.Default);// 0 = The Priority of Sound for hardware that mixing the voice resources  
                    }
                }
                catch (Exception) { }
            }
            #endregion Receiver VOIP Methods
        }
    }
    #endregion UDP VOIP Library

}

       // G.711 (A-Law) Library 
       #region G.711 Encoder Classes

namespace G711Audio
{
    /// <summary>
    /// Turns 16-bit linear PCM values into 8-bit A-law bytes.
    /// </summary>
    public class ALawEncoder
    {
        public const int MAX = 0x7fff; //maximum that can be held in 15 bits

        /// <summary>
        /// An array where the index is the 16-bit PCM input, and the value is
        /// the a-law result.
        /// </summary>
        private static byte[] pcmToALawMap;

        static ALawEncoder()
        {
            pcmToALawMap = new byte[65536];
            for (int i = short.MinValue; i <= short.MaxValue; i++)
                pcmToALawMap[(i & 0xffff)] = encode(i);
        }

        /// <summary>
        /// Encode one a-law byte from a 16-bit signed integer. protected use only.
        /// </summary>
        /// <param name="pcm">A 16-bit signed pcm value</param>
        /// <returns>A a-law encoded byte</returns>
        private static byte encode(int pcm)
        {
            //Get the sign bit.  Shift it for later use without further modification
            int sign = (pcm & 0x8000) >> 8;
            //If the number is negative, make it positive (now it's a magnitude)
            if (sign != 0)
                pcm = -pcm;
            //The magnitude must fit in 15 bits to avoid overflow
            if (pcm > MAX) pcm = MAX;

            /* Finding the "exponent"
             * Bits:
             * 1 2 3 4 5 6 7 8 9 A B C D E F G
             * S 7 6 5 4 3 2 1 0 0 0 0 0 0 0 0
             * We want to find where the first 1 after the sign bit is.
             * We take the corresponding value from the second row as the exponent value.
             * (i.e. if first 1 at position 7 -> exponent = 2)
             * The exponent is 0 if the 1 is not found in bits 2 through 8.
             * This means the exponent is 0 even if the "first 1" doesn't exist.
             */
            int exponent = 7;
            //Move to the right and decrement exponent until we hit the 1 or the exponent hits 0
            for (int expMask = 0x4000; (pcm & expMask) == 0 && exponent > 0; exponent--, expMask >>= 1) { }

            /* The last part - the "mantissa"
             * We need to take the four bits after the 1 we just found.
             * To get it, we shift 0x0f :
             * 1 2 3 4 5 6 7 8 9 A B C D E F G
             * S 0 0 0 0 0 1 . . . . . . . . . (say that exponent is 2)
             * . . . . . . . . . . . . 1 1 1 1
             * We shift it 5 times for an exponent of two, meaning
             * we will shift our four bits (exponent + 3) bits.
             * For convenience, we will actually just shift the number, then AND with 0x0f. 
             * 
             * NOTE: If the exponent is 0:
             * 1 2 3 4 5 6 7 8 9 A B C D E F G
             * S 0 0 0 0 0 0 0 Z Y X W V U T S (we know nothing about bit 9)
             * . . . . . . . . . . . . 1 1 1 1
             * We want to get ZYXW, which means a shift of 4 instead of 3
             */
            int mantissa = (pcm >> ((exponent == 0) ? 4 : (exponent + 3))) & 0x0f;

            //The a-law byte bit arrangement is SEEEMMMM (Sign, Exponent, and Mantissa.)
            byte alaw = (byte)(sign | exponent << 4 | mantissa);

            //Last is to flip every other bit, and the sign bit (0xD5 = 1101 0101)
            return (byte)(alaw ^ 0xD5);
        }

        /// <summary>
        /// Encode a pcm value into a a-law byte
        /// </summary>
        /// <param name="pcm">A 16-bit pcm value</param>
        /// <returns>A a-law encoded byte</returns>
        public static byte ALawEncode(int pcm)
        {
            return pcmToALawMap[pcm & 0xffff];
        }

        /// <summary>
        /// Encode a pcm value into a a-law byte
        /// </summary>
        /// <param name="pcm">A 16-bit pcm value</param>
        /// <returns>A a-law encoded byte</returns>
        public static byte ALawEncode(short pcm)
        {
            return pcmToALawMap[pcm & 0xffff];
        }

        /// <summary>
        /// Encode an array of pcm values
        /// </summary>
        /// <param name="data">An array of 16-bit pcm values</param>
        /// <returns>An array of a-law bytes containing the results</returns>
        public static byte[] ALawEncode(int[] data)
        {
            int size = data.Length;
            byte[] encoded = new byte[size];
            for (int i = 0; i < size; i++)
                encoded[i] = ALawEncode(data[i]);
            return encoded;
        }

        /// <summary>
        /// Encode an array of pcm values
        /// </summary>
        /// <param name="data">An array of 16-bit pcm values</param>
        /// <returns>An array of a-law bytes containing the results</returns>
        public static byte[] ALawEncode(short[] data)
        {
            int size = data.Length;
            byte[] encoded = new byte[size];
            for (int i = 0; i < size; i++)
                encoded[i] = ALawEncode(data[i]);
            return encoded;
        }

        /// <summary>
        /// Encode an array of pcm values
        /// </summary>
        /// <param name="data">An array of bytes in Little-Endian format</param>
        /// <returns>An array of a-law bytes containing the results</returns>
        public static byte[] ALawEncode(byte[] data)
        {
            int size = data.Length / 2;
            byte[] encoded = new byte[size];
            for (int i = 0; i < size; i++)
                encoded[i] = ALawEncode((data[2 * i + 1] << 8) | data[2 * i]);
            return encoded;
        }

        /// <summary>
        /// Encode an array of pcm values into a pre-allocated target array
        /// </summary>
        /// <param name="data">An array of bytes in Little-Endian format</param>
        /// <param name="target">A pre-allocated array to receive the A-law bytes.  This array must be at least half the size of the source.</param>
        public static void ALawEncode(byte[] data, byte[] target)
        {
            int size = data.Length / 2;
            for (int i = 0; i < size; i++)
                target[i] = ALawEncode((data[2 * i + 1] << 8) | data[2 * i]);
        }
    }


    /// <summary>
    /// Turns 8-bit A-law bytes back into 16-bit PCM values.
    /// </summary>
    public static class ALawDecoder
    {
        /// <summary>
        /// An array where the index is the a-law input, and the value is
        /// the 16-bit PCM result.
        /// </summary>
        private static short[] aLawToPcmMap;

        static ALawDecoder()
        {
            aLawToPcmMap = new short[256];
            for (byte i = 0; i < byte.MaxValue; i++)
                aLawToPcmMap[i] = decode(i);
        }

        /// <summary>
        /// Decode one a-law byte. For protected use only.
        /// </summary>
        /// <param name="alaw">The encoded a-law byte</param>
        /// <returns>A short containing the 16-bit result</returns>
        private static short decode(byte alaw)
        {
            //Invert every other bit, and the sign bit (0xD5 = 1101 0101)
            alaw ^= 0xD5;

            //Pull out the value of the sign bit
            int sign = alaw & 0x80;
            //Pull out and shift over the value of the exponent
            int exponent = (alaw & 0x70) >> 4;
            //Pull out the four bits of data
            int data = alaw & 0x0f;

            //Shift the data four bits to the left
            data <<= 4;
            //Add 8 to put the result in the middle of the range (like adding a half)
            data += 8;

            //If the exponent is not 0, then we know the four bits followed a 1,
            //and can thus add this implicit 1 with 0x100.
            if (exponent != 0)
                data += 0x100;
            /* Shift the bits to where they need to be: left (exponent - 1) places
             * Why (exponent - 1) ?
             * 1 2 3 4 5 6 7 8 9 A B C D E F G
             * . 7 6 5 4 3 2 1 . . . . . . . . <-- starting bit (based on exponent)
             * . . . . . . . Z x x x x 1 0 0 0 <-- our data (Z is 0 only when exponent is 0)
             * We need to move the one under the value of the exponent,
             * which means it must move (exponent - 1) times
             * It also means shifting is unnecessary if exponent is 0 or 1.
             */
            if (exponent > 1)
                data <<= (exponent - 1);

            return (short)(sign == 0 ? data : -data);
        }

        /// <summary>
        /// Decode one a-law byte
        /// </summary>
        /// <param name="alaw">The encoded a-law byte</param>
        /// <returns>A short containing the 16-bit result</returns>
        public static short ALawDecode(byte alaw)
        {
            return aLawToPcmMap[alaw];
        }

        /// <summary>
        /// Decode an array of a-law encoded bytes
        /// </summary>
        /// <param name="data">An array of a-law encoded bytes</param>
        /// <returns>An array of shorts containing the results</returns>
        public static short[] ALawDecode(byte[] data)
        {
            int size = data.Length;
            short[] decoded = new short[size];
            for (int i = 0; i < size; i++)
                decoded[i] = aLawToPcmMap[data[i]];
            return decoded;
        }

        /// <summary>
        /// Decode an array of a-law encoded bytes
        /// </summary>
        /// <param name="data">An array of a-law encoded bytes</param>
        /// <param name="decoded">An array of shorts containing the results</param>
        /// <remarks>Same as the other method that returns an array of shorts</remarks>
        public static void ALawDecode(byte[] data, out short[] decoded)
        {
            int size = data.Length;
            decoded = new short[size];
            for (int i = 0; i < size; i++)
                decoded[i] = aLawToPcmMap[data[i]];
        }

        /// <summary>
        /// Decode an array of a-law encoded bytes
        /// </summary>
        /// <param name="data">An array of a-law encoded bytes</param>
        /// <param name="decoded">An array of bytes in Little-Endian format containing the results</param>
        public static void ALawDecode(byte[] data, out byte[] decoded)
        {
            int size = data.Length;
            decoded = new byte[size * 2];
            for (int i = 0; i < size; i++)
            {
                //First byte is the less significant byte
                decoded[2 * i] = (byte)(aLawToPcmMap[data[i]] & 0xff);
                //Second byte is the more significant byte
                decoded[2 * i + 1] = (byte)(aLawToPcmMap[data[i]] >> 8);
            }
        }
    }
}
#endregion G.711 Encoding


//Updating Table:

//R0.2: (Updated By Fadi Abdelqader)
//- Add Reflector Server For UDP
//- Fixed Infinity sending bug in Reflector Server For RTP
//- Fixed JoinRTPSession CNAME Conflict

//R0.1: (Created By Fadi Abdelqader)
//- VOIP Uni-To-Uni-Or-Multi Using RTP
//- VOIP Uni-To-Uni-Or-Multi Using UDP
//- VOIP Uni-To-Server & Server-To-Multi Using RTP



// (C)Authors:
// RTP_VOIP_Library: Fadi Abdelqader 2008 - (C) www.SocketCoder.Com
// RTP Library & DirectSound Library: (C) Microsoft Corporation
// G.711 Library: http://www.codeproject.com/KB/security/g711audio.aspx