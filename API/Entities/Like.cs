namespace API.Entities
{
    public class Like
    {
        public User LikingUser { get; set; }//onaj koji lajkuje
        public int LikingUserId { get; set; }

        public User LikedUser { get; set; }//onaj koji je lajkovan
        public int LikedUserId { get; set; }
    }
}