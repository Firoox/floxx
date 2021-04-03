namespace Floxx.Core.DTO.Identity
{
    public class ToggleUserStatusRequest
    {
        public bool ActivateUser { get; set; }
        public string UserId { get; set; }
    }
}