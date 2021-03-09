using System;
using RestSharp;
using NLog;

namespace HandyAPI
{
    public partial class TheHandy
    {
        private string connectionKey;
        private string rooturi;
        private string _URL = null;

        private static readonly NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Constructs the URL used to communicate with the Handy API
        /// </summary>
        public string HandyURL { get
            {
                if (_URL == null)
                {
                    if (rooturi.EndsWith("/"))
                    {
                        _URL = $"{rooturi}{connectionKey}/";
                    }
                    else
                    {
                        _URL = $"{rooturi}/{connectionKey}/";
                    }
                    log.Debug($"Calculated HandyURL as {_URL}");
                }
                return _URL;
            }
        }

        #region Constructors
        public static TheHandy Connect(string SecretKey)
        {
            log.Trace("Connecting to root endpoint using provided connection key");
            return new TheHandy() { rooturi = ENDPOINT_DEFAULT, connectionKey = SecretKey };
        }

        public static TheHandy ConnectToTest()
        {
            log.Trace("Connecting to testing endpoint using example connection key");
            return new TheHandy() { rooturi = ENDPOINT_VIRTUAL, connectionKey = TEST_CONNECTION_KEY };
        }

        public static TheHandy ConnectStaging(string SecretKey)
        {
            log.Trace("Connecting to the staging endpoint using the provided connection key");
            return new TheHandy() { rooturi = ENDPOINT_STAGING, connectionKey = SecretKey };
        }

        public static TheHandy ConnectLocal(string SecretKey, string localaddress, int port)
        {
            log.Trace($"Connecting to a custom endpoint (https://{localaddress}:{port}/api/v1) using the provided connection key");
            return new TheHandy() { rooturi = $"https://{localaddress}:{port}/api/v1", connectionKey = SecretKey };
        }
        #endregion

        #region Helpers
        private RestClient _client = null;
        public RestClient Client { get
            {
                if (_client == null)
                {
                    _client = new RestClient(HandyURL);
                }

                return _client;
            }
        }

        public static RestRequest GetRequest(string method)
        {
            var req = new RestRequest(method, Method.GET);
            req.Timeout = -1;
            req.AddParameter(PARAM_TIMEOUT, DEFAULT_TIMEOUT);

            return req;
        }
        #endregion

        #region ServerTimeSync
        public long getServerTime()
        {
            var resp = Client.Execute(GetRequest(CMD_GETSERVERTIME));
            var rv = SimpleJson.DeserializeObject<ResponseServerTime>(resp.Content);
            return rv.serverTime;
        }
        #endregion

        #region GetData
        public ResponseVersion getVersion()
        {
            var resp = Client.Execute(GetRequest(CMD_GETVERSION));
            var rv = SimpleJson.DeserializeObject<ResponseVersion>(resp.Content);
            return rv;
        }

        public ResponseSettings getSettings()
        {
            var resp = Client.Execute(GetRequest(CMD_GETSETTINGS));
            var rv = SimpleJson.DeserializeObject<ResponseSettings>(resp.Content);
            return rv;
        }

        public ResponseStatus getStatus()
        {
            var resp = Client.Execute(GetRequest(CMD_GETSTATUS));
            var rv = SimpleJson.DeserializeObject<ResponseStatus>(resp.Content);
            return rv;
        }
        #endregion

        #region SetAction
        public bool setMode(Mode mode)
        {
            var req = GetRequest(CMD_SETMODE);
            req.AddParameter(PARAM_MODE, (int)mode);

            var resp = Client.Execute(req);

            var rv = SimpleJson.DeserializeObject<ResponseSetMode>(resp.Content);
            return rv.success;
        }

        public bool toggleMode(Mode mode)
        {
            var req = GetRequest(CMD_TOGGLEMODE);
            req.AddParameter(PARAM_MODE, (int)mode);

            var resp = Client.Execute(req);

            var rv = SimpleJson.DeserializeObject<ResponseSetMode>(resp.Content);
            return rv.success;
        }

        public ResponseSetSpeed setSpeed(int newSpeed, bool asPercent = false)
        {
            var req = GetRequest(CMD_SETSPEED);
            req.AddParameter(PARAM_SPEED, (int)newSpeed);
            if (asPercent == true)
            {
                req.AddParameter(PARAM_TYPE, PARAMTYPE_PERCENT);
            }

            var resp = Client.Execute(req);

            var rv = SimpleJson.DeserializeObject<ResponseSetSpeed>(resp.Content);
            return rv;
        }

        public ResponseSetStroke setStroke(int newStroke, bool asPercent = false)
        {
            var req = GetRequest(CMD_SETSTROKE);
            req.AddParameter(PARAM_STROKE, newStroke);
            if (asPercent == true)
            {
                req.AddParameter(PARAM_TYPE, PARAMTYPE_PERCENT);
            }

            var resp = Client.Execute(req);
            var rv = SimpleJson.DeserializeObject<ResponseSetStroke>(resp.Content);
            return rv;
        }

        public ResponseStepSpeed stepSpeed(bool speedup)
        {
            var req = GetRequest(CMD_STEPSPEED);
            req.AddParameter(PARAM_STEP, speedup);

            var resp = Client.Execute(req);
            var rv = SimpleJson.DeserializeObject<ResponseStepSpeed>(resp.Content);
            return rv;
        }

        public ResponseStepStroke stepStroke(bool increase)
        {
            var req = GetRequest(CMD_STEPSTROKE);
            req.AddParameter(PARAM_STEP, increase);

            var resp = Client.Execute(req);
            var rv = SimpleJson.DeserializeObject<ResponseStepStroke>(resp.Content);
            return rv;
        }
        #endregion

        #region Sync
        public ResponseSyncPrepare syncPrepare(string URI, string name, int size)
        {
            var req = GetRequest(CMD_SYNCPREPARE);

            req.AddParameter(PARAM_URL, URI);
            req.AddParameter(PARAM_NAME, name);
            req.AddParameter(PARAM_SIZE, size);

            var resp = Client.Execute(req);
            var rv = SimpleJson.DeserializeObject<ResponseSyncPrepare>(resp.Content);
            return rv;
        }

        public ResponseSyncPlay syncPlay(bool start, int time)
        {
            var req = GetRequest(CMD_SYNCPLAY);
            req.AddParameter(PARAM_PLAY, start);
            req.AddParameter(PARAM_SERVERTIME, getServerTime());
            req.AddParameter(PARAM_TIME, time);

            var resp = Client.Execute(req);
            var rv = SimpleJson.DeserializeObject<ResponseSyncPlay>(resp.Content);
            return rv;
        }

        public ResponseSyncOffset syncOffset(int offset)
        {
            var req = GetRequest(CMD_SYNCOFFSET);
            req.AddParameter(PARAM_OFFSET, offset);

            var resp = Client.Execute(req);
            var rv = SimpleJson.DeserializeObject<ResponseSyncOffset>(resp.Content);
            return rv;
        }
        #endregion
    }
}
