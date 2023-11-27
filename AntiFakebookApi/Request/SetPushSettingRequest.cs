namespace AntiFakebookApi.Request
{
    public class SetPushSettingRequest
    {
        public string LikeComment { get; set; }
        public string FromFriends { get; set; }
        public string RequestedFriend { get; set; }
        public string SuggestedFriend { get; set; }
        public string BirthDay { get; set; }
        public string Video { get; set; }
        public string Report { get; set; }
        public string SoundOn { get; set; }
        public string NotificationOn { get; set; }
        public string VibrandOn { get; set; }
        public string LedOn { get; set; }
    }
}
