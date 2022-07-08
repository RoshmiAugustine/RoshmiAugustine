// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Newtonsoft.Json;

namespace b2c_ms_graph
{
    public class UsersModel
    {
        public UserModel[] Users { get; set; }

        public static UsersModel Parse(string JSON)
        {
            return JsonConvert.DeserializeObject(JSON, typeof(UsersModel)) as UsersModel;
        }
    }
    public class UserToUpdateURL
    {
        public int UserID { get; set; }
        public int HelperID { get; set; }
        public long AgencyID { get; set; }
        [JsonProperty("ObjectID")]
        public string AspNetUserID { get; set; }
    }
}