﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperNAT.AsyncSocket
{
    public class RequestInfo : IRequestInfo
    {
        public byte[] Raw { get; set; }
        public byte Head { get; set; }
        public long TotalLength { get; set; }
        public long BodyLength { get; set; }
        public JsonData Body { get; set; }
        public byte End { get; set; }

        public bool Success { get; set; }
        public string Message { get; set; }
    }

    public class JsonData
    {
        /// <summary>
        /// 01:nat 02:http 03:websocket 04:tcp 05:udp
        /// </summary>
        public byte Type { get; set; }

        /// <summary>
        /// 指令类型
        /// </summary>
        public byte Action { get; set; }

        /// <summary>
        /// 数据实体
        /// </summary>
        public string Data { get; set; }
    }

    public enum JsonType : byte
    {
        NAT = 0x01,
        HTTP = 0x02,
        WEBSOCKET = 0x03,
        TCP = 0x04,
        UDP = 0x05
    }

    public enum NatAction : byte
    {
        Connect = 0x01,
        Heart = 0x02,
        MapChange = 0x03,
        ServerMessage = 0x04
    }

    public enum TcpAction : byte
    {
        Connect = 0x01,
        TransferData = 0x02,
        Close = 0x03
    }

    public enum HttpAction : byte
    {
        Request = 0x01,
        Response = 0x02
    }

    public class TcpModel
    {
        public string ServerId { get; set; }
        public string Host { get; set; }
        public string Local { get; set; }
        public string SessionId { get; set; }
        public byte[] Content { get; set; }
    }

    public class HttpModel : TcpModel
    {
        public string Method { get; set; }
        public string Route { get; set; }
        public NameValueCollection Headers { get; set; }
        public string ResponseInfo { get; set; }
    }

    public class ServerMessage
    {
        public byte Type { get; set; } = (byte)ServerMessageType.Error;
        public string Message { get; set; }
        /// <summary>
        /// 异常消息比如主机密钥被占用或者无效则通知客户端不重连
        /// </summary>
        public bool ReConnect { get; set; } = false;
    }

    public enum ServerMessageType : byte
    {
        Info = 0,
        Error = 1
    }
}